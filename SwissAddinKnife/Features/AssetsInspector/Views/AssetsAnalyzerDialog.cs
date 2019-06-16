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


        ListView _resultListView;
        ListStore _resultsStore;
        DataField<AssetProperties> _assetProperties;
        DataField<string> _imageFileNameField;
        DataField<string> _projectNameField;
        DataField<string> _resultField;
        DataField<Result<IList<Condition>>> _analysisResultField;

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

            _assetProperties = new DataField<AssetProperties>();
            _imageFileNameField = new DataField<string>();
            _projectNameField = new DataField<string>();
            _resultField = new DataField<string>();
            _analysisResultField = new DataField<Result<IList<Condition>>>();
            _resultsStore = new ListStore(_assetProperties,_imageFileNameField, _projectNameField, _resultField, _analysisResultField);
            _resultListView = new ListView
            {
                GridLinesVisible = GridLines.Both,
                HeightRequest = 305,
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
            _mainBox.PackStart(_resultListView,expand:true, marginBottom: 5);
            _mainBox.PackEnd(_analyzeButton);

            Content = _mainBox;
        }

        void AttachEvents()
        {
            _analyzeButton.Clicked += OnAnalyzeClicked;
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
                var resourcesFolder = Path.Combine(project.BaseDirectory, "Resources");
                var resources = FileUtils.GetFolderImages(resourcesFolder);

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
            _resultsStore.Clear();
            foreach (AssetProperties asset in this.assets)
            {
                var row = _resultsStore.AddRow();
                _resultsStore.SetValue(row, _assetProperties, asset);
                _resultsStore.SetValue(row, _imageFileNameField, asset.Asset.Identifier);
                _resultsStore.SetValue(row, _projectNameField, asset.Project.Name);
                _resultsStore.SetValue(row, _resultField, "not analyzed");
            }
        }

        private void AnalizeResources()
        {
            for (int i = 0; i < _resultsStore.RowCount; i++)
            {
                var asset = _resultsStore.GetValue(i, _assetProperties);

                var result = asset.Asset.Analize();
                _resultsStore.SetValue(i, _resultField, result.IsSuccess.ToString());
                _resultsStore.SetValue(i, _analysisResultField, result);
            }
        }
    }
}
