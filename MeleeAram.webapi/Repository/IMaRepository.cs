using System;
using System.Linq.Expressions;

namespace MeleeAram.webapi.Repository;

public interface IMaRepository<T>
{
    Task<IEnumerable<T>> GetAll();

    Task<T> GetEntityById(int id);
    Task<T> GetEntityByColumnValue(Expression<Func<T, bool>> boolFunc);
    Task<T> UpdateEntityById(int id, T entity);
    Task<T> CreateEntity(T entity);
    Task<T> DeleteEntityById(int id);
    public bool Exists(Expression<Func<T, bool>> exist);

}
