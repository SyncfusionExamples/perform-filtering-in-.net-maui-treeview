# perform-filtering-in-.net-maui-treeview
This example demonstrates about how to filter the nodes in .NET MAUI TreeView(SfTreeView).

In [.NET MAUI TreeView](https://www.syncfusion.com/maui-controls/maui-treeview), You can filter the TreeView nodes based on the search text by changing the underlaying collection.

**XAML** 
Add Searchbar above .NET MAUI TreeView, to search the TreeViewNodes.

```xaml
<StackLayout>
    <SearchBar Placeholder="Search Treeview"
               x:Name="searchBar" />
    <syncfusion:SfTreeView x:Name="treeView"
                           ExpandActionTarget="Node"
                           ItemsSource="{Binding Folders}"
                           ChildPropertyName="Files">

    </syncfusion:SfTreeView>
</StackLayout>
```
Wire the TextChanged event of the SerachBar to filter the TreeView collection.

C#
``` csharp
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
```

Compare the search text to the model class property to filter the item. The filtered objects are set to TreeView.ItemsSource.

``` csharp
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
```

**Conclusion** 
I hope you enjoyed learning about How to filter the TreeViewNodes in .NET MAUI TreeView.
You can refer to our [.NET MAUI TreeView](https://www.syncfusion.com/maui-controls/maui-treeview) feature tour page to know about its other groundbreaking feature representations and documentation, and how to quickly get started for configuration specifications. You can also explore our .NET MAUI TreeView example to understand how to create and present data. For current customers, you can check out our components from the License and Downloads page. If you are new to Syncfusion, you can try our 30-day free trial to check out our other controls.
If you have any queries or require clarifications, please let us know in the comments section below. You can also contact us through our support forums, Direct-Trac, or feedback portal. We are always happy to assist you!
