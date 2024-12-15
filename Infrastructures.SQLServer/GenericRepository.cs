using System.Linq.Expressions;
using AutoMapper;
using Infrastructure.DataContext;
using Infrastructures.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using UseCases.GenericRepository;

namespace Infrastructures.SQLServer;

public class GenericRepository<TModel, TEntity> : IGenericRepository<TModel>
    where TModel : class
    where TEntity : class
{
    private readonly SchoolContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IMapper _mapper;

    public GenericRepository(SchoolContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        _mapper = mapper;
    }
    

    public async Task<IEnumerable<TModel>> GetAsync(
        Expression<Func<TModel, bool>> filter = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        var entityFilter = MapFilter(filter);
        if (entityFilter != null)
        {
            query = query.Where(entityFilter);
        }

        if (orderBy != null)
        {
            var modelQuery = MapQuery(query);
            return await Task.FromResult(orderBy(modelQuery).ToList()); // Use `ToListAsync()` if mapping supports async.
        }

        return await MapQuery(query).ToListAsync();
    }


    public async Task<TModel> GetByIdAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        return MapToModel(entity);
    }

    public async Task InsertAsync(TModel model)
    {
        var entity = MapToEntity(model);
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(TModel model)
    {
        var entity = MapToEntity(model);
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(TModel model)
    {
        var entity = MapToEntity(model);
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }

    private TModel MapToModel(TEntity entity)
    {
        if (entity == null) return null;
        return _mapper.Map<TModel>(entity);
    }

    private TEntity MapToEntity(TModel model)
    {
        if (model == null) return null;
        return _mapper.Map<TEntity>(model);
    }

    private IQueryable<TModel> MapQuery(IQueryable<TEntity> query)
    {
        return query.Select(entity => _mapper.Map<TModel>(entity));
    }

    private Expression<Func<TEntity, bool>> MapFilter(Expression<Func<TModel, bool>> filter)
    {
        if (filter == null) return null;

        // Use AutoMapper to project the filter to TEntity
        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var mappedBody = _mapper.Map<Expression<Func<TEntity, bool>>>(filter).Body;
        return Expression.Lambda<Func<TEntity, bool>>(mappedBody, parameter);
    }
}
