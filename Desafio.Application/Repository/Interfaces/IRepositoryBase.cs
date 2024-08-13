namespace Desafio.Application.Repository.Interfaces;

public interface IRepositoryBase<T> where T : class, new()
{
    IQueryable<T> GetAll();
    Task<T?> Get(object?  id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task Remove(T entity);
}