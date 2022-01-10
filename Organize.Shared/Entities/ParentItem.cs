using System.Collections.ObjectModel;

namespace Organize.Shared.Entities;

public class ParentItem : BaseItem
{
    public ObservableCollection<ChildItem>? ChildItems
    {
        get => _childItems;
        set => SetProperty(ref _childItems, value);
    }

    private ObservableCollection<ChildItem> _childItems;
}