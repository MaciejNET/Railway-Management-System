namespace RailwayManagementSystem.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task Add(T entity);
    Task AddRange(IEnumerable<T> entities);
    public Task Update(T entity);
    Task Remove(T entity);
    Task SaveChanges();
}