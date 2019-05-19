using System;
using System.IO;
using System.Threading.Tasks;
using ImageGenerator;
using ImageGenerator.Contracts;
using ImageGenerator.Services;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using SwissAddinKnife.Utils;
using Xwt;
using ImageOutputPropertiesFactory = SwissAddinKnife.Features.AssetsGenerator.Utils.ImageOutputPropertiesFactory;

namespace SwissAddinKnife.Features.AssetsGenerator.Views
{
    public class ImageGeneratorDialog : Dialog
    {
        readonly IImageOrchestrator imageOrchestrator = new ImageOrchestrator(new FolderGenerator(), new ImageGenerator.Services.ImageService());

        VBox _mainBox;

        VBox _baseSizeBox;
        Label _baseSizeLabel;
        HBox _radioButtonsBox;
        RadioButtonGroup _radioButtonsGroup;
        RadioButton _4xRadioButton;
        RadioButton _3xRadioButton;

        Label _imagesLabel;
        HBox _imagesBox;
        Button _imagesSelectorButton;
        ListBox _imagesListBox;

        Label _outputFolderLabel;
        HBox _outputFolderBox;
        Button _outputFolderButton;
        TextEntry _outputFolderEntry;

        HBox _buttonBox;
        Button _generateButton;


        OpenFileDialog fileDialog;
        SelectFolderDialog folderDialog;


        public ImageGeneratorDialog()
        {
            Init();
            BuildGui();
            AttachEvents();
        }

        void Init()
        {
            Title = "Image Generator";


            _mainBox = new VBox
            {
                HeightRequest = 340,
                WidthRequest = 500
            };

            _baseSizeBox = new VBox();
            _baseSizeLabel = new Label("Select your design base size");
            _radioButtonsBox = new HBox();
            _radioButtonsGroup = new RadioButtonGroup();
            _4xRadioButton = new RadioButton("4x")
            {
                Group = _radioButtonsGroup
            };
            _3xRadioButton = new RadioButton("3x")
            {
                Group = _radioButtonsGroup
            };


            _imagesLabel = new Label("Select the largest image files to generate different sizes for all platforms");
            _imagesBox = new HBox();
            _imagesListBox = new ListBox
            {
                WidthRequest = 460,
                HeightRequest = 120
            };
            _imagesSelectorButton = new Button("...")
            {
                ExpandVertical = false,
                VerticalPlacement = WidgetPlacement.Start
            };

            _outputFolderLabel = new Label("Select output folder:");

            _outputFolderBox = new HBox();
            _outputFolderEntry = new TextEntry
            {
                WidthRequest = 460,
                Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            _outputFolderButton = new Button("...");

            _buttonBox = new HBox();
            _generateButton = new Button("Generate")
            {
                BackgroundColor = Styles.BaseSelectionBackgroundColor,
                LabelColor = Styles.BaseSelectionTextColor,
                WidthRequest = 100,
                HeightRequest = 40
            };

            fileDialog = new OpenFileDialog("Select images")
            {
                Multiselect = true
            };

            folderDialog = new SelectFolderDialog("Select output folder");
        }

        void BuildGui()
        {
            _baseSizeBox.PackStart(_baseSizeLabel);
            _baseSizeBox.PackEnd(_radioButtonsBox);
            _radioButtonsBox.PackStart(_3xRadioButton);
            _radioButtonsBox.PackStart(_4xRadioButton);
            _mainBox.PackStart(_baseSizeBox, marginBottom: 5);

            _mainBox.PackStart(_imagesLabel);
            _imagesBox.PackStart(_imagesListBox, marginTop: 5);
            _imagesBox.PackEnd(_imagesSelectorButton);
            _mainBox.PackStart(_imagesBox);
            _mainBox.PackStart(_outputFolderLabel, marginTop: 15);
            _outputFolderBox.PackStart(_outputFolderEntry);
            _outputFolderBox.PackEnd(_outputFolderButton);
            _mainBox.PackStart(_outputFolderBox);
            _mainBox.PackEnd(_buttonBox);

            _buttonBox.PackEnd(_generateButton);


            Content = _mainBox;
            Resizable = false;
        }

        void AttachEvents()
        {
            _generateButton.Clicked += OnGenerateClicked;
            _imagesSelectorButton.Clicked += OnFileDialogClicked;
            _outputFolderButton.Clicked += OnOutputFolderClicked;
        }

        private void OnOutputFolderClicked(object sender, EventArgs e)
        {
            if (folderDialog.Run(this))
                _outputFolderEntry.Text = folderDialog.Folder;

        }

        private void OnFileDialogClicked(object sender, EventArgs e)
        {
            if (fileDialog.Run(this))
            {
                _imagesListBox.Items.Clear();
                foreach (var fileName in fileDialog.FileNames)
                    _imagesListBox.Items.Add(fileName);
            }
        }

        void OnGenerateClicked(object sender, EventArgs args)
        {
            Task.Run(() => GenerateImages());
        }

        void Loading(bool isLoading)
        {
            _mainBox.Sensitive = !isLoading;
        }


        private void GenerateImages()
        {
            var progressMonitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor("Generating images...", Stock.StatusSolutionOperation, false, true, false);
            Loading(true);

            bool success = true;
            string outputFolder = _outputFolderEntry.Text;

            foreach (var path in fileDialog.FileNames)
            {
                try
                {
                    var outputProperties = _3xRadioButton.Active ?
                                            ImageOutputPropertiesFactory.CreateForXamarin3x(Path.GetExtension(path), outputFolder) :
                                            ImageOutputPropertiesFactory.CreateForXamarin4x(Path.GetExtension(path), outputFolder);


                    var file = File.ReadAllBytes(path);

                    ImageProperties imageProperties = new ImageProperties()
                    {
                        FileName = Path.GetFileNameWithoutExtension(path),
                        Image = file,
                        ImageOutputProperties = outputProperties
                    };

                    imageOrchestrator.Generate(imageProperties);

                }
                catch (Exception)
                {
                    success = false;
                }
            }

            progressMonitor.EndTask();
            Loading(false);

            if (success)
            {
                FinderUtils.Reveal(Path.Combine(outputFolder, "Images"));
            }

        }
    }
}
