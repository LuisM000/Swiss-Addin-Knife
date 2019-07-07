using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageGenerator;
using ImageGenerator.Contracts;
using ImageGenerator.Services;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;
using SwissAddinKnife.Extensions;
using Xwt;
using ImageOutputPropertiesFactory = SwissAddinKnife.Features.AssetsGenerator.Utils.ImageOutputPropertiesFactory;

namespace SwissAddinKnife.Features.AssetsGenerator.Views
{
    public class ImageAssetsGeneratorDialog : Dialog
    {
        readonly Solution solution;
        readonly IImageOrchestrator imageOrchestrator = new ImageOrchestrator(new FolderGenerator(), new ImageGenerator.Services.ImageService());

        Notebook _mainNotebook;
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
        OpenFileDialog fileDialog;

        ListView _imagesListView;
        ListStore _imagesStore;
        DataField<string> _imagePathField;
        DataField<string> _imageNameField;


        HBox _iOSBox;
        CheckBox _iOSCheckBox;
        Label _iOSOptionsLabel;


        HBox _androidBox;
        CheckBox _androidCheckBox;
        Label _androidOptionsLabel;

        HBox _androidDrawableBox;
        CheckBox _androidDrawableCheckBox;
        Label _androidDrawableOptionsLabel;


        ListView _resultListView;
        ListStore _resultsStore;
        DataField<string> _imageFileNameField;
        DataField<string> _projectField;
        DataField<string> _statusField;

        CheckBox _overwriteFilesCheckBox;

        Button _generateButton;


        public ImageAssetsGeneratorDialog(Solution solution)
        {
            this.solution = solution;
            Init();
            BuildGui();
            AttachEvents();
        }

        void Init()
        {
            Title = "Assets Generator";

            _mainNotebook = new Notebook();
            _mainBox = new VBox()
            {
                HeightRequest = 370,
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

            _imageNameField = new DataField<string>();
            _imagePathField = new DataField<string>();
            _imagesStore = new ListStore(_imageNameField, _imagePathField);
            _imagesListView = new ListView
            {
                WidthRequest = 460,
                HeightRequest = 120
            };
            _imagesListView.Columns.Add("Output name (editable)", new TextCellView(_imageNameField) { Editable = true });
            _imagesListView.Columns.Add("Path", new TextCellView(_imagePathField));
            _imagesListView.DataSource = _imagesStore;


            _imagesSelectorButton = new Button("...")
            {
                ExpandVertical = false,
                VerticalPlacement = WidgetPlacement.Start
            };
            fileDialog = new OpenFileDialog("Select images");
            fileDialog.Multiselect = true;


            _iOSBox = new HBox();
            _iOSCheckBox = new CheckBox("iOS")
            {
                Active = true
            };
            if (!solution.ContainsAnyIOSProject())
            {
                _iOSCheckBox.Active = false;
                _iOSCheckBox.Sensitive = false;
            }
            _iOSOptionsLabel = new Label("1x  2x  3x");


            _androidBox = new HBox();
            _androidCheckBox = new CheckBox("Android")
            {
                Active = true
            };
            _androidDrawableBox = new HBox();
            _androidDrawableCheckBox = new CheckBox("Android")
            {
                Active = false
            };
            if (!solution.ContainsAnyAndroidProject())
            {
                _androidCheckBox.Active = false;
                _androidCheckBox.Sensitive = false;
                _androidDrawableCheckBox.Active = false;
                _androidDrawableCheckBox.Sensitive = false;
            }
            _androidOptionsLabel = new Label("ldpi mdpi hdpi xhdpi xxhdpi xxxhdpi");
            _androidDrawableOptionsLabel = new Label("Include mdpi asset in Resources/drawable");


            _overwriteFilesCheckBox = new CheckBox("Allow rewrite files that already exist");

            _imageFileNameField = new DataField<string>();
            _projectField = new DataField<string>();
            _statusField = new DataField<string>();
            _resultsStore = new ListStore(_projectField, _imageFileNameField, _statusField);
            _resultListView = new ListView
            {
                GridLinesVisible = GridLines.Both,
                HeightRequest = 305,
                WidthRequest = 500
            };
            _resultListView.Columns.Add("Project", new TextCellView(_projectField));
            _resultListView.Columns.Add("Asset", new TextCellView(_imageFileNameField));
            _resultListView.Columns.Add("Status", new TextCellView(_statusField));
            _resultListView.DataSource = _resultsStore;



            _generateButton = new Button("Generate")
            {
                BackgroundColor = Styles.BaseSelectionBackgroundColor,
                LabelColor = Styles.BaseSelectionTextColor,
                WidthRequest = 100,
                HeightRequest = 40
            };
        }

        void BuildGui()
        {
            _baseSizeBox.PackStart(_baseSizeLabel);
            _baseSizeBox.PackEnd(_radioButtonsBox);
            _radioButtonsBox.PackStart(_3xRadioButton);
            _radioButtonsBox.PackStart(_4xRadioButton);
            _mainBox.PackStart(_baseSizeBox, marginBottom: 5);

            _mainBox.PackStart(_imagesLabel);
            _imagesBox.PackStart(_imagesListView, marginTop: 5);
            _imagesBox.PackEnd(_imagesSelectorButton);
            _mainBox.PackStart(_imagesBox);

            _mainBox.PackStart(_overwriteFilesCheckBox, marginBottom: 10);

            _iOSBox.PackStart(_iOSCheckBox);
            _iOSBox.PackStart(_iOSOptionsLabel);
            _mainBox.PackStart(_iOSBox);

            _androidBox.PackStart(_androidCheckBox);
            _androidBox.PackStart(_androidOptionsLabel);
            _mainBox.PackStart(_androidBox);

            _androidDrawableBox.PackStart(_androidDrawableCheckBox);
            _androidDrawableBox.PackStart(_androidDrawableOptionsLabel);
            _mainBox.PackStart(_androidDrawableBox);

            _mainBox.PackEnd(_generateButton, hpos: WidgetPlacement.End);
            _mainBox.Margin = new WidgetSpacing(10, 10, 10, 10);

            _mainNotebook.Add(_mainBox, " Main ");
            _mainNotebook.Add(_resultListView, " Output ");

            Content = _mainNotebook;
            Resizable = false;
        }

        void AttachEvents()
        {
            _3xRadioButton.ActiveChanged += _3xRadioButton_ActiveChanged;
            _4xRadioButton.ActiveChanged += _4xRadioButton_ActiveChanged;

            _imagesSelectorButton.Clicked += OnFileDialogClicked;
            _generateButton.Clicked += OnGenerateClicked;
        }

        void _3xRadioButton_ActiveChanged(object sender, EventArgs e)
        {
            _androidOptionsLabel.Text = "ldpi mdpi hdpi xhdpi xxhdpi";
        }
        void _4xRadioButton_ActiveChanged(object sender, EventArgs e)
        {
            _androidOptionsLabel.Text = "ldpi mdpi hdpi xhdpi xxhdpi xxxhdpi";
        }
        void OnFileDialogClicked(object sender, EventArgs e)
        {
            if (fileDialog.Run(this))
            {
                _imagesStore.Clear();
                foreach (var fileName in fileDialog.FileNames)
                {
                    var row = _imagesStore.AddRow();
                    _imagesStore.SetValue(row, _imageNameField, Path.GetFileNameWithoutExtension(fileName));
                    _imagesStore.SetValue(row, _imagePathField, fileName);
                }
            }
        }
        async void OnGenerateClicked(object sender, EventArgs args)
        {
            await GenerateImageAssets();
        }
        void Loading(bool isLoading)
        {
            _mainBox.Sensitive = !isLoading;
        }


        async Task GenerateImageAssets()
        {
            var progressMonitor = IdeApp.Workbench.ProgressMonitors.GetStatusProgressMonitor("Generating assets...", Stock.StatusSolutionOperation, false, true, false);
            Loading(true);
            _resultsStore.Clear();
            _mainNotebook.CurrentTabIndex = 1;
            bool overwriteFiles = _overwriteFilesCheckBox.Active;
            bool includeDrawableAsset = _androidDrawableCheckBox.Active;

            if (_androidCheckBox.Active)
            {
                Func<string, string, bool, IList<ImageOutputProperties>> outputPropertiesFactory;
                if (_3xRadioButton.Active)
                {
                    if (includeDrawableAsset)
                        outputPropertiesFactory = ImageOutputPropertiesFactory.CreateForAndroid3xWithDrawable;
                    else
                        outputPropertiesFactory = ImageOutputPropertiesFactory.CreateForAndroid3xWithoutDrawable;
                }
                else
                {
                    if (includeDrawableAsset)
                        outputPropertiesFactory = ImageOutputPropertiesFactory.CreateForAndroid4xWithDrawable;
                    else
                        outputPropertiesFactory = ImageOutputPropertiesFactory.CreateForAndroid4xWithoutDrawable;
                }

                foreach (var project in solution.GetAllProjects().Where(p => p.IsAndroidProject()))
                {
                    var outputFolder = Path.Combine(project.BaseDirectory, "Resources");
                    await GenerateAssetsAsync(project, outputFolder, overwriteFiles, outputPropertiesFactory);
                    await project.SaveAsync(progressMonitor);
                }
            }

            if (_iOSCheckBox.Active)
            {
                Func<string, string, bool, IList<ImageOutputProperties>> outputPropertiesFactory;
                if (_3xRadioButton.Active)
                {
                    outputPropertiesFactory = ImageOutputPropertiesFactory.CreateForIOS3x;
                }
                else
                {
                    outputPropertiesFactory = ImageOutputPropertiesFactory.CreateForIOS4x;
                }

                foreach (var project in solution.GetAllProjects().Where(p => p.IsIOSProject()))
                {
                    var outputFolder = Path.Combine(project.BaseDirectory, "Resources");
                    await GenerateAssetsAsync(project, outputFolder, overwriteFiles, outputPropertiesFactory);
                    await project.SaveAsync(progressMonitor);
                }
            }


            Loading(false);
            progressMonitor.EndTask();
        }

        async Task GenerateAssetsAsync(Project project, string outputFolder, bool overwriteFiles, Func<string, string, bool, IList<ImageOutputProperties>> outputPropertiesFactory)
        {
            for (int i = 0; i < _imagesStore.RowCount; i++)
            {
                var path = _imagesStore.GetValue(i, _imagePathField);
                var outputAssetName = _imagesStore.GetValue(i, _imageNameField);

                var outputProperties = outputPropertiesFactory(Path.GetExtension(path), outputFolder, overwriteFiles);
                var file = File.ReadAllBytes(path);

                ImageProperties imageProperties = new ImageProperties()
                {
                    FileName = outputAssetName,
                    Image = file,
                    ImageOutputProperties = outputProperties
                };

                var row = _resultsStore.AddRow();
                _resultsStore.SetValue(row, _imageFileNameField, imageProperties.FileName);
                _resultsStore.SetValue(row, _projectField, project.Name);
                _resultsStore.SetValue(row, _statusField, "Generating");

                try
                {
                    await GenerateImageAsync(imageProperties);

                    foreach (var outputProperty in outputProperties)
                    {
                        string outputPath = string.Concat(outputProperty.GetFullPath(imageProperties.FileName), outputProperty.ImageFormat.GetExtension());
                        project.AddFile(outputPath);
                    }

                    _resultsStore.SetValue(row, _statusField, "Generated");
                }
                catch (Exception ex)
                {
                    _resultsStore.SetValue(row, _statusField, $"Exception: {ex.Message}");
                }
            }
        }

        async Task GenerateImageAsync(ImageProperties imageProperties)
        {
            await Task.Run(() =>
            {
                imageOrchestrator.Generate(imageProperties);
            });
        }
    }
}
