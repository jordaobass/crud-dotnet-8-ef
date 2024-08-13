using Desafio.Application.Repository.Context;
using Desafio.Application.Repository.Interfaces;

namespace Desafio.Application.Repository;


public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
{
    private readonly DesafioContext _dbContext;

    protected RepositoryBase(DesafioContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return _dbContext.Set<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Não foi possível recuperar entidades: {ex.Message}");
        }
    } 
    
    public async Task<TEntity?> Get(object? id)
    {
        try
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Não foi possível recuperar entidade: {ex.Message}");
        }
    }

    public async Task Remove(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(Remove)} entidade não deve ser nula");
        }
        try
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} não pôde ser salvo: {ex.Message}");
        }
        
    }


    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entidade não deve ser nula");
        }

        try
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} não pôde ser salvo: {ex.Message}");
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entidade não deve ser nula ");
        }

        try
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} não pôde ser atualizado: {ex.Message}");
        }
    }
}