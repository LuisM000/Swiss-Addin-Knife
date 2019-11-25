using System;
using System.Collections.Generic;
using System.Linq;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;
using Xwt;

namespace SwissAddinKnife.Features.AssetsGenerator.Views
{
    public class SelectProjectsDialog : Dialog
    {
        public IList<Project> Result { get; private set; }

        private readonly IList<Project> _projects;

        private VBox _mainVBox;
        private Label _descriptionLabel;
        private ListStore _projectsStore;
        private ListView _projectsListView;
        private DataField<bool> _projectSelectedDataField;
        private DataField<string> _projectNameDataField;

        private Button _acceptButton;


        public SelectProjectsDialog(IList<Project> projects)
        {
            this._projects = projects;
            Init();
            BuildGui();
            AttachEvents();
            FillProjects();
        }

       
        private void Init()
        {
            _mainVBox = new VBox();
            _descriptionLabel = new Label("Select the projects");

            _projectSelectedDataField = new DataField<bool>();
            _projectNameDataField = new DataField<string>();
            _projectsStore = new ListStore(_projectNameDataField, _projectSelectedDataField);
            _projectsListView = new ListView
            {
                GridLinesVisible = GridLines.Both,
                HeightRequest = 280,
                WidthRequest = 350
            };
            _projectsListView.Columns.Add(string.Empty, new CheckBoxCellView(_projectSelectedDataField) { Editable = true });
            _projectsListView.Columns.Add("Project", new TextCellView(_projectNameDataField));
            _projectsListView.DataSource = _projectsStore;

            _acceptButton = new Button("Accept")
            {
                BackgroundColor = Styles.BaseSelectionBackgroundColor,
                LabelColor = Styles.BaseSelectionTextColor,
                WidthRequest = 100,
                HeightRequest = 40
            };
        }

        private void BuildGui()
        {
            _mainVBox.PackStart(_descriptionLabel);
            _mainVBox.PackStart(_projectsListView);
            _mainVBox.PackEnd(_acceptButton);

            Content = _mainVBox;
            Resizable = false;
        }

        private void AttachEvents()
        {
            _acceptButton.Clicked += OnAcceptButtonClicked;
        }

        private void OnAcceptButtonClicked(object sender, EventArgs e)
        {
            Result = new List<Project>();
            for (int i = 0; i < _projectsStore.RowCount; i++)
            {
                if(_projectsStore.GetValue(i, _projectSelectedDataField))
                {
                    var project = _projects.FirstOrDefault(p => p.Name == _projectsStore.GetValue(i, _projectNameDataField));
                    Result.Add(project);
                }
               
            }
            this.Close();
        }

        private void FillProjects()
        {
            foreach (var project in _projects)
            {
                var row = _projectsStore.AddRow();
                _projectsStore.SetValue(row, _projectSelectedDataField, true);
                _projectsStore.SetValue(row, _projectNameDataField, project.Name);
            }
        }

    }
}
