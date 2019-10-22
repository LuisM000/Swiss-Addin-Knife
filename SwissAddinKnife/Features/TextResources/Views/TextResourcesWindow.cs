using System;
using Gtk;
using SwissAddinKnife.Features.TextResources.Services;

namespace SwissAddinKnife.Features.TextResources.Views
{   
    public class TextResourcesWindow : Window
    {
        TextResourcesManager _textResourcesManager;
        ListStore _resourcesListStore;
        TreeModelFilter _filter;

        ScrolledWindow _scrolledWindow;
        TreeView _textResourcesTreeView;
        Entry _filterByKeyEntry;


        public TextResourcesWindow(string folderPath) : base("TextResources manager")
        {
            _textResourcesManager = new TextResourcesManager(folderPath);
            _textResourcesManager.LoadResources();
            BuildUI();
        }

        private void BuildUI()
        {
            _scrolledWindow = new ScrolledWindow();
            this.Add(_scrolledWindow);

            _resourcesListStore = new ListStore(typeof(string));
            _textResourcesTreeView = new TreeView();


            var filterWidget = CreateFilterWidget();
            VBox vBox = new VBox();
            vBox.PackStart(filterWidget, false, false, 5);
            vBox.PackEnd(_textResourcesTreeView, true, true, 5);
            _scrolledWindow.AddWithViewport(vBox);

            BuildTextResourcesTreeView();
        }

        public void BuildTextResourcesTreeView()
        {
            TreeViewColumn keysColumn = new TreeViewColumn
            {
                Title = string.Empty,
                Resizable = true
            };
            _textResourcesTreeView.AppendColumn(keysColumn);

            CellRendererText keyCell = new Gtk.CellRendererText
            {
                Editable = true,
                Width = 300,
                Background = "black"
            };
            keyCell.Edited += OnKeyCellEdited;
            keysColumn.PackStart(keyCell, true);

            keysColumn.SetCellDataFunc(keyCell, new Gtk.TreeCellDataFunc(HandleKeyCellDataFunc));

            foreach (var textResource in _textResourcesManager.TextResources)
            {
                TreeViewColumn resourcesColumn = new TreeViewColumn
                {
                    Title = textResource.Name,
                    Resizable = true
                };
                _textResourcesTreeView.AppendColumn(resourcesColumn);

                CellRendererText valueCell = new Gtk.CellRendererText
                {
                    Width = 200,
                    Editable = true
                };
                valueCell.Edited += (o, args) => OnValueCellEdited((CellRendererText)o, args, textResource);
                resourcesColumn.PackStart(valueCell, true);

                resourcesColumn.SetCellDataFunc(valueCell, new Gtk.TreeCellDataFunc(HandleValueCellDataFunc));
            }

            foreach (var key in _textResourcesManager.GetAllKeys())
            {
                _resourcesListStore.AppendValues(key);
            }

            _textResourcesTreeView.Model = _filter;
        }

      
        private Widget CreateFilterWidget()
        {
            VBox filterBox = new VBox();

            Button addResourceButton = new Button("Add new resource");
            addResourceButton.Clicked += OnAddResourceButtonClicked;

            var labelFilter = new Label("Filter by key:");
            _filterByKeyEntry = new Entry();
            _filterByKeyEntry.FocusOutEvent += FilterByKeyEntry_FocusOutEvent;
            _filter = new Gtk.TreeModelFilter(_resourcesListStore, null)
            {
                VisibleFunc = new Gtk.TreeModelFilterVisibleFunc(FilterByKeyVisibleFunc)
            };

            HBox filterKeyBox = new HBox();
            filterKeyBox.PackStart(addResourceButton, false, false, 5);
            filterKeyBox.PackStart(labelFilter, false, false, 5);
            filterKeyBox.PackStart(_filterByKeyEntry, false, false, 5);

            filterBox.PackStart(filterKeyBox, false, false, 0);

            return filterBox;
        }

        void OnValueCellEdited(CellRendererText cell, Gtk.EditedArgs args, Services.TextResources textResources)
        {
            _filter.GetIter(out TreeIter iter, new TreePath(args.Path));

            string key = (string)_filter.GetValue(iter, 0);

            textResources.SaveValue(key, args.NewText);
        }

        void OnKeyCellEdited(object o, EditedArgs args)
        {
            var newKey = args.NewText;
            _resourcesListStore.GetIter(out TreeIter iter, new TreePath(args.Path));
            var oldKey = (string)_resourcesListStore.GetValue(iter, 0);
            _textResourcesManager.UpdateKey(oldKey, newKey);
            _resourcesListStore.SetValue(iter,0,newKey);
        }


        void FilterByKeyEntry_FocusOutEvent(object sender, EventArgs e)
        {
            _filter.Refilter();
        }


        void OnAddResourceButtonClicked(object sender, EventArgs e)
        {
            var newKey = _textResourcesManager.CreateAvailableKey();
            _textResourcesManager.SaveValueInMainResource(newKey, "new_value");
            var newTreeIter = _resourcesListStore.InsertWithValues(0, newKey);
            var newTreePath = _resourcesListStore.GetPath(newTreeIter);
            _textResourcesTreeView.SetCursorOnCell(newTreePath, _textResourcesTreeView.Columns[0], _textResourcesTreeView.Columns[0].Cells[0], true);
        }

        private bool FilterByKeyVisibleFunc(TreeModel model, TreeIter iter)
        {
            if (string.IsNullOrEmpty(_filterByKeyEntry.Text))
                return true;


            string key = model.GetValue(iter, 0).ToString();
            if (key.ToLower().Contains(_filterByKeyEntry.Text.ToLower()))
                return true;

            return false;
        }

        void HandleValueCellDataFunc(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter)
        {
            var key = (string)tree_model.GetValue(iter, 0);
            var value = _textResourcesManager.GetValue(tree_column.Title, key);
            var cellText = (CellRendererText)cell;

            if (value == null)
            {
                cellText.Background = "red";
            }
            else
            {
                cellText.Background = null;
            }

            ((CellRendererText)cell).Text = value;
        }

        void HandleKeyCellDataFunc(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter)
        {
            var key = (string)tree_model.GetValue(iter, 0);
            var cellText = (CellRendererText)cell; 
           ((CellRendererText)cell).Text = key;
        }
    }
}