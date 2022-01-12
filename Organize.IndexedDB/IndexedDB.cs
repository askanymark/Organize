using System.Linq.Expressions;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.IndexedDB;

public class IndexedDB : IPersistenceService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly string _objectIdentifier = "organizedIndexedDb";

    public IndexedDB(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseEntity
    {
        var tableName = typeof(T).Name;
        var entities = await _jsRuntime.InvokeAsync<T[]>($"{_objectIdentifier}.getAllAsync", tableName);

        return entities.Where(whereExpression.Compile());
    }

    public async Task<int> InsertAsync<T>(T entity) where T : BaseEntity
    {
        var tableName = typeof(T).Name;
        var serializedEntity = JsonConvert.SerializeObject(entity);
        var id = await _jsRuntime.InvokeAsync<int>($"{_objectIdentifier}.addAsync", tableName, serializedEntity);

        return id;
    }

    public async Task UpdateAsync<T>(T entity) where T : BaseEntity
    {
        var tableName = typeof(T).Name;
        var serializedEntity = JsonConvert.SerializeObject(entity);

        await _jsRuntime.InvokeVoidAsync($"{_objectIdentifier}.putAsync", tableName, serializedEntity, entity.Id);
    }

    public async Task DeleteAsync<T>(T entity) where T : BaseEntity
    {
        var tableName = typeof(T).Name;

        await _jsRuntime.InvokeVoidAsync($"{_objectIdentifier}.deleteAsync", tableName, entity.Id);
    }

    public async Task InitAsync()
    {
        await _jsRuntime.InvokeVoidAsync($"{_objectIdentifier}.initAsync");
    }

    public Task<User> AuthenticateAsync(User user)
    {
        throw new NotImplementedException();
    }
}