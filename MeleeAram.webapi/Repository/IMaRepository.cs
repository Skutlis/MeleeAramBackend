using System;

namespace MeleeAram.webapi.Repository;

public interface IMaRepository<T>
{
    Task<IEnumerable<T>> GetAll();

    Task<T> GetEntityById(int id);
    Task<T> UpdateEntityById(int id, T entity);
    Task<T> CreateEntity(T entity);
    Task<T> DeleteEntityById(int id);
    public bool Exists(Func<T, bool> exist);

}
