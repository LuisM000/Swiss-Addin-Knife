using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;
using SwissAddinKnife.Extensions;
using SwissAddinKnife.Features.AssetsInspector.Core;
using Xwt.Drawing;
using SwissAddinKnife.Features.AssetsInspector.Models;
using SwissAddinKnife.Features.AssetsInspector.Services;
using SwissAddinKnife.Utils;
using Xwt;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.AndroidFilesConditions;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions.IosFilesConditions;

namespace SwissAddinKnife.Features.AssetsInspector.Views
{
    public class AssetsAnalyzerDialog : Dialog
    {
        private Solution solution;
        private List<AssetProperties> assets;
        IImageService imageService = new ImageService();

        Notebook _mainNotebook;
        HPaned _mainPaned;
        VBox _mainLeftBox;
        VBox _mainRightBox;

        Label _selectProjectsLabel;
        ComboBox _selectProjectsComboBox;

        Label _selectResultLabel;
        ComboBox _selectResultComboBox;

        ListView _resultListView;
        ListStore _resultsStore;
        DataField<AssetProperties> _assetProperties;
        DataField<string> _imageFileNameField;
        DataField<string> _projectNameField;
        DataField<string> _resultField;


        ListView _selectedAssetListView;
        ListStore _selectedAssetStore;
        DataField<Image> _selectedAssetImageField;
        DataField<string> _selectedAssetNameField;
        DataField<string> _selectedAssetSizeField;

        ListView _selectedAssetResultListView;
        ListStore _selectedAssetResultStore;
        DataField<Image> _selectedAssetResultIconField;
        DataField<string> _selectedAssetResultDescriptionField;
        
        Button _analyzeButton;


        VBox _mainOptionsBox;
        HBox _analizeOptionsBox;
        VBox _iosAnalizeOptionsBox;
        Label _iosAnalizeOptionsLabel;
        VBox _androidAnalizeOptionsBox;
        Label _androidAnalizeOptionsLabel;

        CheckBox _iosStandardFileConditionCheckbox;
        CheckBox _iosX2FileConditionCheckbox;
        CheckBox _iosX3FileConditionCheckbox;
        CheckBox _iosResolutionFileConditionCheckbox;
        HBox _iosResolutionMarginBox;
        Label _iosResolutionMarginLabel;
        TextEntry _iosResolutionMarginTextEntry;

        CheckBox _androidStandardFileConditionCheckbox;
        CheckBox _androidLdpiFileConditionCheckbox;
        CheckBox _androidMdpiFileConditionCheckbox;
        CheckBox _androidHdpiFileConditionCheckbox;
        CheckBox _androidXhdpiFileConditionCheckbox;
        CheckBox _androidXxhdpiFileConditionCheckbox;
        CheckBox _androidXxxhdpiFileConditionCheckbox;
        CheckBox _androidResolutionFileConditionCheckbox;
        HBox _androidResolutionMarginBox;
        Label _androidResolutionMarginLabel;
        TextEntry _androidResolutionMarginTextEntry;

        public AssetsAnalyzerDialog(Solution solution)
        {
            this.solution = solution;
            this.assets = new List<AssetProperties>();
            Init();
            BuildGui();
            AttachEvents();
            FillAllAssets();
            DrawAssets();
        }

       
        void Init()
        {
            Title = "Assets analyzer";

            _mainNotebook = new Notebook();
            _mainPaned = new HPaned()
            {
                HeightRequest = 600,
                WidthRequest = 600
            };

            _mainLeftBox = new VBox();
            _mainRightBox = new VBox();

            _selectProjectsLabel = new Label("Select the project");
            _selectProjectsComboBox = new ComboBox();
            _selectProjectsComboBox.Items.Add("All");
            foreach (var project in solution.GetAllProjects().Where(p => p.IsIOSProject() || p.IsAndroidProject()))
            {
                _selectProjectsComboBox.Items.Add(project, project.Name);
            }
            _selectProjectsComboBox.SelectedIndex = 0;

            _selectResultLabel = new Label("Filter by result");
            _selectResultComboBox = new ComboBox();
            _selectResultComboBox.Items.Add("All");
            _selectResultComboBox.Items.Add("Not analyzed");
            _selectResultComboBox.Items.Add("OK");
            _selectResultComboBox.Items.Add("Failed");
            _selectResultComboBox.SelectedIndex = 0;

            _assetProperties = new DataField<AssetProperties>();
            _imageFileNameField = new DataField<string>();
            _projectNameField = new DataField<string>();
            _resultField = new DataField<string>();
            _resultsStore = new ListStore(_assetProperties,_imageFileNameField, _projectNameField, _resultField);
            _resultListView = new ListView
            {
                GridLinesVisible = GridLines.Both,
            };
            _resultListView.Columns.Add("Asset", new TextCellView(_imageFileNameField));
            _resultListView.Columns.Add("Project", new TextCellView(_projectNameField));
            _resultListView.Columns.Add("Result", new TextCellView(_resultField));
            _resultListView.DataSource = _resultsStore;

            _analyzeButton = new Button("Analyze")
            {
                BackgroundColor = Styles.BaseSelectionBackgroundColor,
                LabelColor = Styles.BaseSelectionTextColor,
                WidthRequest = 100,
                HeightRequest = 40
            };


            _selectedAssetImageField = new DataField<Image>();
            _selectedAssetNameField = new DataField<string>();
            _selectedAssetSizeField = new DataField<string>();
            _selectedAssetStore = new ListStore(_selectedAssetImageField, _selectedAssetNameField, _selectedAssetSizeField);
            _selectedAssetListView = new ListView
            {
                GridLinesVisible = GridLines.Both,
                HeightRequest = 200
            };
            _selectedAssetListView.Columns.Add(string.Empty, new ImageCellView(_selectedAssetImageField));
            _selectedAssetListView.Columns.Add("Asset name", new TextCellView(_selectedAssetNameField));
            _selectedAssetListView.Columns.Add("Size", new TextCellView(_selectedAssetSizeField));
            _selectedAssetListView.DataSource = _selectedAssetStore;


            _selectedAssetResultIconField = new DataField<Image>();
            _selectedAssetResultDescriptionField = new DataField<string>();
            _selectedAssetResultStore = new ListStore(_selectedAssetResultIconField, _selectedAssetResultDescriptionField);
            _selectedAssetResultListView = new ListView
            {
                GridLinesVisible = GridLines.Both,
                HeightRequest = 200
            };
            _selectedAssetResultListView.Columns.Add(string.Empty, new ImageCellView(_selectedAssetResultIconField));
            _selectedAssetResultListView.Columns.Add("Description", new TextCellView(_selectedAssetResultDescriptionField));
            _selectedAssetResultListView.DataSource = _selectedAssetResultStore;

            
            _mainOptionsBox = new VBox();
            _analizeOptionsBox = new HBox();
            _iosAnalizeOptionsBox = new VBox();
            _iosAnalizeOptionsLabel = new Label("iOS conditions");
            _androidAnalizeOptionsBox = new VBox();
            _androidAnalizeOptionsLabel = new Label("Android conditions");


            _iosResolutionMarginBox = new HBox();
            _iosResolutionMarginLabel = new Label("Margin (px) when checking resolution");

            _iosStandardFileConditionCheckbox = new CheckBox(IOSContainsStandardFileCondition.Description);
            _iosX2FileConditionCheckbox = new CheckBox(IOSContainsX2FileCondition.Description);
            _iosX3FileConditionCheckbox = new CheckBox(IOSContainsX3FileCondition.Description);
            _iosResolutionFileConditionCheckbox = new CheckBox(SizesFilesiOSCondition.Description);
            _iosResolutionMarginTextEntry = new TextEntry() { WidthRequest = 35 };
            _iosStandardFileConditionCheckbox.Active = true;
            _iosX2FileConditionCheckbox.Active = true;
            _iosX3FileConditionCheckbox.Active = true;
            _iosResolutionFileConditionCheckbox.Active = true;
            _iosResolutionMarginTextEntry.Text = "2";


            _androidResolutionMarginBox = new HBox();
            _androidResolutionMarginLabel = new Label("Margin (px) when checking resolution");

            _androidStandardFileConditionCheckbox = new CheckBox(AndroidContainsStandardFileCondition.Description);
            _androidLdpiFileConditionCheckbox = new CheckBox(AndroidContainsLdpiFileCondition.Description);
            _androidMdpiFileConditionCheckbox = new CheckBox(AndroidContainsMdpiFileCondition.Description);
            _androidHdpiFileConditionCheckbox = new CheckBox(AndroidContainsHdpiFileCondition.Description);
            _androidXhdpiFileConditionCheckbox = new CheckBox(AndroidContainsXhdpiFileCondition.Description);
            _androidXxhdpiFileConditionCheckbox = new CheckBox(AndroidContainsXxhdpiFileCondition.Description);
            _androidXxxhdpiFileConditionCheckbox = new CheckBox(AndroidContainsXxxhdpiFileCondition.Description);
            _androidResolutionFileConditionCheckbox = new CheckBox(SizesFilesAndroidCondition.Description);
            _androidResolutionMarginTextEntry = new TextEntry() { WidthRequest = 35 };
            _androidStandardFileConditionCheckbox.Active = false;
            _androidLdpiFileConditionCheckbox.Active = true;
            _androidMdpiFileConditionCheckbox.Active = true;
            _androidHdpiFileConditionCheckbox.Active = true;
            _androidXhdpiFileConditionCheckbox.Active = true;
            _androidXxhdpiFileConditionCheckbox.Active = true;
            _androidXxxhdpiFileConditionCheckbox.Active = true;
            _androidResolutionFileConditionCheckbox.Active = true;
            _androidResolutionMarginTextEntry.Text = "2";
        }

        void BuildGui()
        {
            _mainNotebook.Add(_mainPaned, " Main ");
            _mainNotebook.Add(_mainOptionsBox, " Settings ");

            _mainPaned.Panel1.Content = _mainLeftBox;
            _mainPaned.Panel2.Content = _mainRightBox;

            _mainLeftBox.PackStart(_selectProjectsLabel, expand: false, marginTop: 10);
            _mainLeftBox.PackStart(_selectProjectsComboBox, expand: false, marginBottom: 5);
            _mainLeftBox.PackStart(_selectResultLabel, expand: false);
            _mainLeftBox.PackStart(_selectResultComboBox, expand: false, marginBottom: 5);
            _mainLeftBox.PackStart(_resultListView,expand:true, marginBottom: 5);
            _mainLeftBox.PackEnd(_analyzeButton);

            _mainRightBox.PackStart(_selectedAssetResultListView, expand: true, marginBottom: 5);
            _mainRightBox.PackStart(_selectedAssetListView, expand: true, marginBottom: 5);


            _mainOptionsBox.PackStart(_analizeOptionsBox);
            _analizeOptionsBox.PackStart(_iosAnalizeOptionsBox, hpos: WidgetPlacement.Center, marginTop: 10);
            _analizeOptionsBox.PackEnd(_androidAnalizeOptionsBox, hpos: WidgetPlacement.Center, marginTop: 10);

            _iosAnalizeOptionsBox.PackStart(_iosAnalizeOptionsLabel, hpos: WidgetPlacement.Center);
            _iosResolutionMarginBox.PackStart(_iosResolutionMarginLabel);
            _iosResolutionMarginBox.PackEnd(_iosResolutionMarginTextEntry);
            _iosAnalizeOptionsBox.PackStart(_iosStandardFileConditionCheckbox, marginBottom: -8);
            _iosAnalizeOptionsBox.PackStart(_iosX2FileConditionCheckbox, marginBottom: -8);
            _iosAnalizeOptionsBox.PackStart(_iosX3FileConditionCheckbox, marginBottom: -8);
            _iosAnalizeOptionsBox.PackStart(_iosResolutionFileConditionCheckbox, marginBottom: -10);
            _iosAnalizeOptionsBox.PackStart(_iosResolutionMarginBox, marginBottom: -8, marginLeft: 25);



            _androidAnalizeOptionsBox.PackStart(_androidAnalizeOptionsLabel, hpos: WidgetPlacement.Center);
            _androidResolutionMarginBox.PackStart(_androidResolutionMarginLabel);
            _androidResolutionMarginBox.PackEnd(_androidResolutionMarginTextEntry);
            _androidAnalizeOptionsBox.PackStart(_androidStandardFileConditionCheckbox, marginBottom: -8);
            _androidAnalizeOptionsBox.PackStart(_androidLdpiFileConditionCheckbox, marginBottom: -8);
            _androidAnalizeOptionsBox.PackStart(_androidMdpiFileConditionCheckbox, marginBottom: -8);
            _androidAnalizeOptionsBox.PackStart(_androidHdpiFileConditionCheckbox, marginBottom: -8);
            _androidAnalizeOptionsBox.PackStart(_androidXhdpiFileConditionCheckbox, marginBottom: -8);
            _androidAnalizeOptionsBox.PackStart(_androidXxhdpiFileConditionCheckbox, marginBottom: -8);
            _androidAnalizeOptionsBox.PackStart(_androidXxxhdpiFileConditionCheckbox, marginBottom: -8);
            _androidAnalizeOptionsBox.PackStart(_androidResolutionFileConditionCheckbox, marginBottom: -8);
            _androidAnalizeOptionsBox.PackStart(_androidResolutionMarginBox, marginBottom: -8, marginLeft: 25);


            Content = _mainNotebook;
        }

        void AttachEvents()
        {
            _analyzeButton.Clicked += OnAnalyzeClicked;
            _selectProjectsComboBox.SelectionChanged += OnSelectProjectsChanged;
            _selectResultComboBox.SelectionChanged += OnResultChanged;
            _resultListView.SelectionChanged += OnResultListViewSelectionChanged;
        }

        
        private void OnSelectProjectsChanged(object sender, EventArgs e)
        {
            DrawAssets();
        }
        private void OnResultChanged(object sender, EventArgs e)
        {
            DrawAssets();
        }

        void OnAnalyzeClicked(object sender, EventArgs args)
        {
            AnalizeResources();
        }

        private void OnResultListViewSelectionChanged(object sender, EventArgs e)
        {
            var asset = _resultsStore.GetValue(_resultListView.SelectedRow, _assetProperties);
            DrawSelectedAsset(asset);
        }


        private void FillAllAssets()
        {
            this.assets.Clear();
            foreach (var project in solution.GetAllProjects().Where(p => p.IsIOSProject()))
            {
                var resourcesFolder = Path.Combine(project.BaseDirectory, "Resources");
                var resources = FileUtils.GetFolderImages(resourcesFolder);

                List<AssetiOS> iosAssets = new List<AssetiOS>();
                foreach (var resource in resources)
                {
                    var asset = iosAssets.FirstOrDefault(a => a.CanBeAdded(resource));
                    if(asset == null)
                    {
                        asset = new AssetiOS(resource);
                        iosAssets.Add(asset);
                    }
                    asset.Add(resource);
                }

                this.assets.AddRange(iosAssets.Select(a => new AssetProperties(a, project)));
            }

            foreach (var project in solution.GetAllProjects().Where(p => p.IsAndroidProject()))
            {
                var resources = GetAndroidImageResources(project);

                List<AssetAndroid> androidAssets = new List<AssetAndroid>();
                foreach (var resource in resources)
                {
                    var asset = androidAssets.FirstOrDefault(a => a.CanBeAdded(resource));
                    if (asset == null)
                    {
                        asset = new AssetAndroid(resource);
                        androidAssets.Add(asset);
                    }
                    asset.Add(resource);
                }

                this.assets.AddRange(androidAssets.Select(a => new AssetProperties(a, project)));
            }
        }

        private void DrawAssets()
        {
            var selectedProject = _selectProjectsComboBox.SelectedItem;
            var selectedResult = _selectResultComboBox.SelectedText;
            _resultsStore.Clear();
            foreach (AssetProperties asset in this.assets)
            {
                if (selectedProject is Project project && asset.Project != project)
                    continue;
                if (selectedResult == "Not analyzed" && asset.Result.Value != null)
                    continue;
                if (selectedResult == "OK" && (asset.Result.Value!=null && asset.Result.IsError || asset.Result.Value ==null))
                    continue;
                if (selectedResult == "Failed" && (asset.Result.Value != null && asset.Result.IsSuccess || asset.Result.Value == null))
                    continue;

                var row = _resultsStore.AddRow();
                _resultsStore.SetValue(row, _assetProperties, asset);
                _resultsStore.SetValue(row, _imageFileNameField, asset.Asset.Identifier);
                _resultsStore.SetValue(row, _projectNameField, asset.Project.Name);
                if(asset.Result.Value == null)
                    _resultsStore.SetValue(row, _resultField, "Not analyzed");
                else
                    _resultsStore.SetValue(row, _resultField, asset.Result.IsSuccess ? "OK" : "Failed");                
            }
        }

        private void AnalizeResources()
        {
            for (int i = 0; i < _resultsStore.RowCount; i++)
            {
                var asset = _resultsStore.GetValue(i, _assetProperties);
                var assetConditions = this.GetAssetConditions(asset);
                asset.Result = asset.Asset.Analize(assetConditions);
                _resultsStore.SetValue(i, _resultField, asset.Result.IsSuccess ? "OK" : "Failed");
            }
        }

        private void DrawSelectedAsset(AssetProperties assetProperties)
        {
            _selectedAssetStore.Clear();
            foreach (var file in assetProperties.Asset.Files)
            {
                var row = _selectedAssetStore.AddRow();
                _selectedAssetStore.SetValue(row, _selectedAssetNameField, file.ReducedPath);
                var imageSize = imageService.GetImageSize(file.Path);
                _selectedAssetStore.SetValue(row, _selectedAssetSizeField, imageSize.Width + "x" + imageSize.Height);
                _selectedAssetStore.SetValue(row, _selectedAssetImageField, Image.FromFile(file.Path).WithBoxSize(30, 30));
            }

            _selectedAssetResultStore.Clear();
            if (assetProperties.Result.Value == null)
                return;
            foreach (var condition in assetProperties.Result.Value)
            {
                var row = _selectedAssetResultStore.AddRow();
                Image resultImage = condition.IsFulfilled ? Image.FromResource("SwissAddinKnife.Resources.success.png") :
                                                            Image.FromResource("SwissAddinKnife.Resources.unsuccess.png");

                _selectedAssetResultStore.SetValue(row, _selectedAssetResultIconField, resultImage.WithBoxSize(15));
                _selectedAssetResultStore.SetValue(row, _selectedAssetResultDescriptionField, condition.Description);
            }
        }

        private IEnumerable<string> GetAndroidImageResources(Project project)
        {
            var drawableImages = FileUtils.GetFolderImages(Path.Combine(project.BaseDirectory, "Resources/drawable"));
            var ldpiImages = FileUtils.GetFolderImages(Path.Combine(project.BaseDirectory, "Resources/drawable-ldpi"));
            var mdpiImages = FileUtils.GetFolderImages(Path.Combine(project.BaseDirectory, "Resources/drawable-mdpi"));
            var hdpiImages = FileUtils.GetFolderImages(Path.Combine(project.BaseDirectory, "Resources/drawable-hdpi"));
            var xhdpiImages = FileUtils.GetFolderImages(Path.Combine(project.BaseDirectory, "Resources/drawable-xhdpi"));
            var xxhdpiImages = FileUtils.GetFolderImages(Path.Combine(project.BaseDirectory, "Resources/drawable-xxhdpi"));
            var xxxhdpiImages = FileUtils.GetFolderImages(Path.Combine(project.BaseDirectory, "Resources/drawable-xxxhdpi"));

            return drawableImages.Union(ldpiImages).Union(mdpiImages).Union(hdpiImages).Union(xhdpiImages).Union(xxhdpiImages).Union(xxxhdpiImages);
        }

        private IList<AssetCondition> GetAssetConditions(AssetProperties assetProperties)
        {
            List<AssetCondition> assetConditions = new List<AssetCondition>();
            if (assetProperties.Asset is AssetAndroid assetAndroid)
            {
                if (_androidStandardFileConditionCheckbox.Active)
                    assetConditions.Add(new AndroidContainsStandardFileCondition(assetAndroid));

                if (_androidLdpiFileConditionCheckbox.Active)
                    assetConditions.Add(new AndroidContainsLdpiFileCondition(assetAndroid));

                if (_androidMdpiFileConditionCheckbox.Active)
                    assetConditions.Add(new AndroidContainsMdpiFileCondition(assetAndroid));

                if (_androidHdpiFileConditionCheckbox.Active)
                    assetConditions.Add(new AndroidContainsHdpiFileCondition(assetAndroid));

                if (_androidXhdpiFileConditionCheckbox.Active)
                    assetConditions.Add(new AndroidContainsXhdpiFileCondition(assetAndroid));

                if (_androidXxhdpiFileConditionCheckbox.Active)
                    assetConditions.Add(new AndroidContainsXxhdpiFileCondition(assetAndroid));

                if (_androidXxxhdpiFileConditionCheckbox.Active)
                    assetConditions.Add(new AndroidContainsXxxhdpiFileCondition(assetAndroid));

                if (_androidResolutionFileConditionCheckbox.Active)
                {
                    int.TryParse(_androidResolutionMarginTextEntry.Text, out int sizeMargin);
                    assetConditions.Add(new SizesFilesAndroidCondition(assetAndroid, sizeMargin));
                }
            }
            else if (assetProperties.Asset is AssetiOS assetiOS)
            {
                if (_iosStandardFileConditionCheckbox.Active)
                    assetConditions.Add(new IOSContainsStandardFileCondition(assetiOS));

                if (_iosX2FileConditionCheckbox.Active)
                    assetConditions.Add(new IOSContainsX2FileCondition(assetiOS));

                if (_iosX3FileConditionCheckbox.Active)
                    assetConditions.Add(new IOSContainsX3FileCondition(assetiOS));

                if (_iosResolutionFileConditionCheckbox.Active)
                {
                    int.TryParse(_iosResolutionMarginTextEntry.Text, out int sizeMargin);
                    assetConditions.Add(new SizesFilesiOSCondition(assetiOS, sizeMargin));
                }
            }

            return assetConditions;
        }
    }
}
