using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.DataAccess;

public class ItemDataAccess : IItemDataAccess
{
    private IPersistenceService _persistenceService;
    
    public ItemDataAccess(IPersistenceService persistenceService)
    {
        _persistenceService = persistenceService;
    }
    
    public async Task<IEnumerable<T>> GetItemsAsync<T>(int parentId) where T : BaseItem
    {
        return await _persistenceService.GetAsync<T>(i => i.ParentId == parentId);
    }

    public async Task InsertItemAsync<T>(T item) where T : BaseItem
    {
        var id = await _persistenceService.InsertAsync<T>(item);
        item.Id = id;
    }

    public async Task UpdateItemAsync<T>(T item) where T : BaseItem
    {
        await _persistenceService.UpdateAsync<T>(item);
    }

    public async Task DeleteItemsAsync<T>(IEnumerable<T> items) where T : BaseItem
    {
        foreach (var item in items)
        {
            await _persistenceService.DeleteAsync<T>(item);
        }
    }
}