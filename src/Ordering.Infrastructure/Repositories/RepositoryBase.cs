using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    private protected readonly OrderContext dbContext;

    public RepositoryBase(OrderContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        dbContext.Set<T>().Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = dbContext.Set<T>();
        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = dbContext.Set<T>();
        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        
        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}
