using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Gtk;
using SwissAddinKnife.Features.TextResources.Services;

namespace SwissAddinKnife.Features.TextResources.Views
{
   
    public class TextResourcesWindow : Window
    {
        TextResourcesManager _textResourcesManager;
        ListStore _resourcesListStore;
        TreeModelFilter _filter;

        TreeView _textResourcesTreeView;
        Entry _filterByKeyEntry;


        public TextResourcesWindow(string folderPath) : base("TextResources manager")
        {
            _textResourcesManager = new TextResourcesManager(folderPath);
            _textResourcesManager.LoadResources();
            BuildUI();
        }

        public void BuildUI()
        {
            Window window = new Gtk.Window("TreeView Example");
            window.Modal = true;
            window.Maximize();

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            window.Add(scrolledWindow);

            _filterByKeyEntry = new Entry();
            _filterByKeyEntry.Changed += FilterByKeyEntry_Changed;
            _textResourcesTreeView = new TreeView();
            _resourcesListStore = new ListStore(typeof(string));
            _filter = new Gtk.TreeModelFilter(_resourcesListStore, null)
            {
                VisibleFunc = new Gtk.TreeModelFilterVisibleFunc(FilterByKeyVisibleFunc)
            };

            VBox vBox = new VBox();
            vBox.PackStart(_filterByKeyEntry, false, false, 5);
            vBox.PackEnd(_textResourcesTreeView, true, true, 5);
            scrolledWindow.AddWithViewport(vBox);


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

            window.ShowAll();
        }


        void OnValueCellEdited(CellRendererText cell, Gtk.EditedArgs args, Services.TextResources textResources)
        {
            _filter.GetIter(out TreeIter iter, new TreePath(args.Path));

            string key = (string)_filter.GetValue(iter, 0);

            textResources.SaveValue(key, args.NewText);
        }

        void FilterByKeyEntry_Changed(object sender, EventArgs e)
        {
            _filter.Refilter();
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