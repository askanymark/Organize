using System.Linq.Expressions;
using Organize.Shared.Entities;

namespace Organize.Shared.Interfaces;

public interface IPersistenceService
{
    Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseEntity;

    Task<int> InsertAsync<T>(T entity) where T : BaseEntity;

    Task UpdateAsync<T>(T entity) where T : BaseEntity;

    Task DeleteAsync<T>(T entity) where T : BaseEntity;

    Task InitAsync();

    Task<User> AuthenticateAsync(User user);
}