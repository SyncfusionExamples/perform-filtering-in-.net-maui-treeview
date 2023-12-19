using Syncfusion.Maui.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTreeView
{
    internal class TreeViewBehavior : Behavior<ContentPage>
    {
        SearchBar searchBox;
        SfTreeView treeView;
        FileManagerViewModel viewModel;
        List<Folder> FilteredSource;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            this.treeView = bindable.FindByName<SfTreeView>("treeView");
            this.searchBox = bindable.FindByName<SearchBar>("searchBar");
            this.viewModel = new FileManagerViewModel();
            this.treeView.BindingContext = this.viewModel;
            this.FilteredSource = new List<Folder>();

            this.searchBox.TextChanged += OnSearchBarTextChanged;
            base.OnAttachedTo(bindable);
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                treeView.ItemsSource = viewModel.Folders;
            }
            else
            {
                FilteredSource = viewModel.Folders.Where(x => (x.ItemName.ToLower()).StartsWith(e.NewTextValue.ToLower())).ToList<Folder>();

                if (FilteredSource.Count == 0)
                {
                    foreach (var node in viewModel.Folders)
                        this.GetChildNode(node, e.NewTextValue);
                }
                treeView.ItemsSource = FilteredSource;
                treeView.RefreshView();
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            this.searchBox.TextChanged -= OnSearchBarTextChanged;
            this.searchBox = null;
            this.treeView = null;
            this.viewModel = null;
            base.OnDetachingFrom(bindable);
        }

        private void GetChildNode(Folder node, string searchText)
        {
            if (node.Files.Count < 0) return;

            foreach (var child in node.Files)
            {
                if (child.ItemName.ToLower().StartsWith(searchText.ToLower()))
                {
                    FilteredSource.Add(child);
                }
                if (child.Files != null)
                {
                    this.GetChildNode(child, searchText);
                }
            }
        }
    }
}
