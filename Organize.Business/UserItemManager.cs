using System.Collections.ObjectModel;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.Shared.Interfaces;

namespace Organize.Business;

public class UserItemManager : IUserItemManager
{
    private IItemDataAccess _dataAccess;

    public UserItemManager(IItemDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task RetrieveAllUserItemsOfUserAndSetToUserAsync(User user)
    {
        var items = new List<BaseItem>();
        var textItems = await _dataAccess.GetItemsAsync<TextItem>(user.Id);
        var urlItems = await _dataAccess.GetItemsAsync<UrlItem>(user.Id);
        var parentItems = (await _dataAccess.GetItemsAsync<ParentItem>(user.Id)).ToList();

        foreach (var parent in parentItems)
        {
            var childItems = await _dataAccess.GetItemsAsync<ChildItem>(parent.Id);
            parent.ChildItems = new ObservableCollection<ChildItem>(childItems);
        }

        items.AddRange(textItems);
        items.AddRange(urlItems);
        items.AddRange(parentItems);

        user.Items = new ObservableCollection<BaseItem>(items.OrderBy(i => i.Position));
    }

    public async Task<ChildItem> CreateNewChildItemAndAddItToParentAsync(ParentItem parentItem)
    {
        var childItem = new ChildItem
        {
            ParentId = parentItem.Id,
            Type = ItemType.Child
        };

        await _dataAccess.InsertItemAsync(childItem);

        parentItem.ChildItems.Add(childItem);

        return childItem;
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

        await _dataAccess.InsertItemAsync(item);

        return item;
    }

    public async Task UpdateAsync<T>(T item) where T : BaseItem
    {
        switch (item)
        {
            case TextItem textItem:
                await _dataAccess.UpdateItemAsync(textItem);
                break;
            case UrlItem urlItem:
                await _dataAccess.UpdateItemAsync(urlItem);
                break;
            case ParentItem parentItem:
                await _dataAccess.UpdateItemAsync(parentItem);
                break;
            case ChildItem childItem:
                await _dataAccess.UpdateItemAsync(childItem);
                break;
        }
    }

    public async Task DeleteAllDoneAsync(User user)
    {
        var doneItems = user.Items.Where(i => i.IsDone).ToList();
        var doneParentItem = doneItems.OfType<ParentItem>().ToList();
        var doneChildItems = doneParentItem.SelectMany(i => i.ChildItems).ToList();

        await _dataAccess.DeleteItemsAsync(doneChildItems);
        await _dataAccess.DeleteItemsAsync(doneParentItem);
        await _dataAccess.DeleteItemsAsync(doneItems.OfType<TextItem>());
        await _dataAccess.DeleteItemsAsync(doneItems.OfType<UrlItem>());

        foreach (var doneItem in doneItems)
        {
            user.Items.Remove(doneItem);
        }

        var orderedByPosition = user.Items.OrderBy(i => i.Position);
        var position = 1;

        foreach (var item in orderedByPosition)
        {
            item.Position = position;
            position++;
            await UpdateAsync(item);
        }
    }
}