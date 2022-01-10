using Organize.Shared.Entities;

namespace Organize.Shared.Interfaces;

public interface IItemDataAccess
{
    Task<IEnumerable<T>> GetItemsAsync<T>(int parentId) where T : BaseItem;

    Task InsertItemAsync<T>(T item) where T : BaseItem;

    Task UpdateItemAsync<T>(T item) where T : BaseItem;

    Task DeleteItemsAsync<T>(IEnumerable<T> items) where T : BaseItem;
}