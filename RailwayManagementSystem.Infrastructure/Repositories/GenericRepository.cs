using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly RailwayManagementSystemDbContext _context;
    
    protected GenericRepository(RailwayManagementSystemDbContext context)
    {
        _context = context;
    }
    
    public async Task<T?> GetById(int id)
        => await _context.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> GetAll()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task Add(T entity)
        => await _context.Set<T>().AddAsync(entity);

    public async Task AddRange(IEnumerable<T> entities)
        => await _context.Set<T>().AddRangeAsync(entities);

    public async Task Update(T entity)
        => await Task.FromResult(_context.Set<T>().Update(entity));

    public async Task Remove(T entity)
        => await Task.FromResult(_context.Set<T>().Remove(entity));

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}