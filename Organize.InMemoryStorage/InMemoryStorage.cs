using System.Linq.Expressions;
using Organize.Shared.Entities;
using Organize.Shared.Interfaces;

namespace Organize.InMemoryStorage;

public class InMemoryStorage : IPersistenceService
{
    private readonly Dictionary<Type, List<BaseEntity>> _dictionary = new();

    private List<BaseEntity> GetOrCreateIfNotExists<T>() where T : BaseEntity
    {
        if (_dictionary.ContainsKey(typeof(T)))
        {
            return _dictionary[typeof(T)];
        }

        var list = new List<BaseEntity>();
        _dictionary.Add(typeof(T), list);

        return list;
    }

    public Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseEntity
    {
        var list = GetOrCreateIfNotExists<T>();
        var entities = list.OfType<T>().Where(whereExpression.Compile());

        return Task.FromResult(entities);
    }

    public Task<int> InsertAsync<T>(T entity) where T : BaseEntity
    {
        var list = GetOrCreateIfNotExists<T>();
        var id = list.Count == 0 ? 1 : list.Max(e => e.Id) + 1;
        
        list.Add(entity);

        return Task.FromResult(id);
    }

    public Task UpdateAsync<T>(T entity) where T : BaseEntity
    {
        return Task.FromResult(true);
    }

    public Task DeleteAsync<T>(T entity) where T : BaseEntity
    {
        var list = GetOrCreateIfNotExists<T>();
        list.Remove(entity);

        return Task.FromResult(true);
    }

    public Task InitAsync()
    {
        return Task.FromResult(true);
    }

    public Task<User> AuthenticateAsync(User user)
    {
        var list = GetOrCreateIfNotExists<User>();
        var match = list.OfType<User>().FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

        return Task.FromResult(match);
    }
}