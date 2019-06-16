using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;
using SwissAddinKnife.Extensions;
using SwissAddinKnife.Features.AssetsInspector.Core;
using SwissAddinKnife.Features.AssetsInspector.Core.AssetsConditions;
using SwissAddinKnife.Features.AssetsInspector.Models;
using SwissAddinKnife.Utils;
using Xwt;

namespace SwissAddinKnife.Features.AssetsInspector.Views
{
    public class AssetsAnalyzerDialog : Dialog
    {
        private Solution solution;
        private List<AssetProperties> assets;

        VBox _mainBox;

        Label _selectProjectsLabel;
        ComboBox _selectProjectsComboBox;

        ListView _resultListView;
        ListStore _resultsStore;
        DataField<AssetProperties> _assetProperties;
        DataField<string> _imageFileNameField;
        DataField<string> _projectNameField;
        DataField<string> _resultField;

        Button _analyzeButton;


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

            _mainBox = new VBox()
            {
                HeightRequest = 350,
                WidthRequest = 500
            };

            _selectProjectsLabel = new Label("Select the project");
            _selectProjectsComboBox = new ComboBox();
            _selectProjectsComboBox.Items.Add("All");
            foreach (var project in solution.GetAllProjects().Where(p => p.IsIOSProject() || p.IsAndroidProject()))
            {
                _selectProjectsComboBox.Items.Add(project, project.Name);
            }
            _selectProjectsComboBox.SelectedIndex = 0;

            _assetProperties = new DataField<AssetProperties>();
            _imageFileNameField = new DataField<string>();
            _projectNameField = new DataField<string>();
            _resultField = new DataField<string>();
            _resultsStore = new ListStore(_assetProperties,_imageFileNameField, _projectNameField, _resultField);
            _resultListView = new ListView
            {
                GridLinesVisible = GridLines.Both,
                WidthRequest = 500                
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
        }
        
        void BuildGui()
        {
            _mainBox.PackStart(_selectProjectsLabel, expand: false);
            _mainBox.PackStart(_selectProjectsComboBox, expand: false, marginBottom: 5);
            _mainBox.PackStart(_resultListView,expand:true, marginBottom: 5);
            _mainBox.PackEnd(_analyzeButton);

            Content = _mainBox;
        }

        void AttachEvents()
        {
            _analyzeButton.Clicked += OnAnalyzeClicked;
            _selectProjectsComboBox.SelectionChanged += OnSelectProjectsChanged;
        }

        private void OnSelectProjectsChanged(object sender, EventArgs e)
        {
            DrawAssets();
        }

        void OnAnalyzeClicked(object sender, EventArgs args)
        {
            AnalizeResources();
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
            _resultsStore.Clear();
            foreach (AssetProperties asset in this.assets)
            {
                if (selectedProject is Project project && asset.Project != project)
                    continue;

                var row = _resultsStore.AddRow();
                _resultsStore.SetValue(row, _assetProperties, asset);
                _resultsStore.SetValue(row, _imageFileNameField, asset.Asset.Identifier);
                _resultsStore.SetValue(row, _projectNameField, asset.Project.Name);
                if(asset.Result.Value == null)
                    _resultsStore.SetValue(row, _resultField, "not analyzed");
                else
                    _resultsStore.SetValue(row, _resultField, asset.Result.IsSuccess.ToString());
            }
        }

        private void AnalizeResources()
        {
            for (int i = 0; i < _resultsStore.RowCount; i++)
            {
                var asset = _resultsStore.GetValue(i, _assetProperties);
                asset.Result = asset.Asset.Analize();
                _resultsStore.SetValue(i, _resultField, asset.Result.IsSuccess.ToString());
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
    }
}
