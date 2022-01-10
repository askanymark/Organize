using System.Collections.ObjectModel;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.Shared.Interfaces;

namespace Organize.Business;

public class UserItemManager : IUserItemManager
{
    public async Task<ChildItem> CreateNewChildItemAndAddItToParentAsync(ParentItem parentItem)
    {
        var childItem = new ChildItem
        {
            ParentId = parentItem.Id,
            Type = ItemType.Child
        };

        parentItem.ChildItems.Add(childItem);

        return await Task.FromResult(childItem);
    }

    public async Task<BaseItem> CreateNewUserItemAndAddItToUserAsync(User user, ItemType type)
    {
        BaseItem item = null;

        switch (type)
        {
            case ItemType.Text:
                item = await CreateAndInsertItemAsync<TextItem>(user, type);
                break;
            case ItemType.Url:
                item = await CreateAndInsertItemAsync<UrlItem>(user, type);
                break;
            case ItemType.Parent:
                var parent = await CreateAndInsertItemAsync<ParentItem>(user, type);
                parent.ChildItems = new ObservableCollection<ChildItem>();
                item = parent;
                break;
        }

        user.Items.Add(item);

        return item;
    }

    private async Task<T> CreateAndInsertItemAsync<T>(User user, ItemType type) where T : BaseItem, new()
    {
        var item = new T
        {
            Type = type,
            Position = user.Items.Count + 1,
            ParentId = user.Id
        };

        return await Task.FromResult(item);
    }
}