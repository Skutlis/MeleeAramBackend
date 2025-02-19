using System;
using MeleeAram.webapi.DataContext;
using MeleeAram.webapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeleeAram.webapi.Repository;

public class MaRepository<T> : IMaRepository<T> where T : class, IMaEntities
{
    private MaContext _db;
    private DbSet<T> _table;

    public MaRepository(MaContext context)
    {
        _db = context;
        _table = _db.Set<T>();
    }


    public async Task<T> CreateEntity(T entity)
    {

        entity.Id = _table.Max(e => e.Id) + 1;
        entity.CreatedAt = DateTime.Now.ToUniversalTime();
        entity.UpdatedAt = DateTime.Now.ToUniversalTime();
        await _table.AddAsync(entity);
        await _db.SaveChangesAsync();
        return await GetEntityById(entity.Id); //Ensures related objects are returned aswell

    }

    public async Task<T> DeleteEntityById(int id)
    {
        T entity = await _table.FindAsync(id);
        if (entity != null)
        {
            _table.Remove(entity);
            await _db.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _table.ToListAsync();
    }

    //The use of "lazy loading" enables us to get all relationed entities without include
    public async Task<T> GetEntityById(int id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<T> UpdateEntityById(int id, T entity)
    {
        T entityToUpdate = await _table.FindAsync(id);

        try
        {
            //Updates only fields that are not empty
            entityToUpdate.Update(entity);
            entityToUpdate.UpdatedAt = DateTime.Now.ToUniversalTime();
            await _db.SaveChangesAsync();

            return entityToUpdate;
        }
        catch (Exception)
        {
            return entityToUpdate;
        }
    }

    public bool Exists(Func<T, bool> exist)
    {
        return _table.Any(exist);
    }


}
