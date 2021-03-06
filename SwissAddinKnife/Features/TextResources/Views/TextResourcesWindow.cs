﻿using System;
using System.Linq;
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
            VBox vBox = new VBox();

            this.Add(vBox);

            _resourcesListStore = new ListStore(typeof(string));
            _textResourcesTreeView = new TreeView();

            var filterWidget = CreateFilterWidget();
            vBox.PackStart(filterWidget, false, false, 5);
            vBox.PackEnd(_scrolledWindow, true, true, 5);

            _scrolledWindow.Add(_textResourcesTreeView);

            BuildTextResourcesTreeView();
        }


        public void BuildTextResourcesTreeView()
        {
            TreeViewColumn keysColumn = new TreeViewColumn
            {
                Title = string.Empty,
                Resizable = true
            };
            keysColumn.AddNotification("width",(object o, GLib.NotifyArgs args) => OnColumnWidthChanged(keysColumn));
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
                keysColumn.AddNotification("width", (object o, GLib.NotifyArgs args) => OnColumnWidthChanged(resourcesColumn));
                _textResourcesTreeView.AppendColumn(resourcesColumn);

                CellRendererText valueCell = new Gtk.CellRendererText
                {
                    Width = 200,
                    Editable = true,
                    WrapWidth = 200,
                    SingleParagraphMode = false,
                    WrapMode = Pango.WrapMode.WordChar,
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

        void OnColumnWidthChanged(TreeViewColumn treeViewColumn)
        {
            if(treeViewColumn.Cells.Any() && treeViewColumn.Cells[0] is CellRendererText cellRendererText
                && cellRendererText.WrapWidth > 0 && treeViewColumn.Width > 0 && cellRendererText.WrapWidth != treeViewColumn.Width)
            {
                cellRendererText.WrapWidth = treeViewColumn.Width;
            }
        }


        private Widget CreateFilterWidget()
        {
            VBox filterBox = new VBox();

            Button addResourceButton = new Button("Add new resource");
            addResourceButton.Clicked += OnAddResourceButtonClicked;

            var labelFilter = new Label("Filter by key:");
            _filterByKeyEntry = new Entry();
            _filterByKeyEntry.KeyReleaseEvent+= FilterByKeyEntry_KeyReleaseEvent; ;
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
            _filter.GetIter(out TreeIter iter, new TreePath(args.Path));
            var oldKey = (string)_filter.GetValue(iter, 0);
            int indexOfOldKey = _textResourcesManager.GetAllKeys().IndexOf(oldKey);

            _resourcesListStore.GetIter(out TreeIter iterList, new TreePath(indexOfOldKey.ToString()));
            _resourcesListStore.SetValue(iterList, 0, newKey);

            _textResourcesManager.UpdateKey(oldKey, newKey);
        }

        void FilterByKeyEntry_KeyReleaseEvent(object o, KeyReleaseEventArgs args)
        {
            if (args?.Event.Key == Gdk.Key.Return)
            {
                _filter.Refilter();
            }
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

            cellText.Text = value;
        }

        void HandleKeyCellDataFunc(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter)
        {
            var key = (string)tree_model.GetValue(iter, 0);
            var cellText = (CellRendererText)cell; 
           ((CellRendererText)cell).Text = key;
          
        }
    }
}