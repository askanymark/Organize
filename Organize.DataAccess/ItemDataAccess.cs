using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.DataAccess;

public class ItemDataAccess : IItemDataAccess
{
    public Task<IEnumerable<T>> GetItemsAsync<T>(int parentId) where T : BaseItem
    {
        throw new NotImplementedException();
    }

    public Task InsertItemAsync<T>(T item) where T : BaseItem
    {
        throw new NotImplementedException();
    }

    public Task UpdateItemAsync<T>(T item) where T : BaseItem
    {
        throw new NotImplementedException();
    }

    public Task DeleteItemsAsync<T>(IEnumerable<T> items) where T : BaseItem
    {
        throw new NotImplementedException();
    }
}