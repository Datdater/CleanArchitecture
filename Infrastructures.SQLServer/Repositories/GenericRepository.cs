using System.Linq.Expressions;
using AutoMapper;
using Entities;
using Infrastructures.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using UseCase.Commons;
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

    public async Task<Pagination<TModel>> ToPagination(int pageIndex = 0, int pageSize = 10)
    {
        var itemCount = await _dbSet.CountAsync();
        var items = await _dbSet
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
        var result = new Pagination<TModel>()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItemsCount = itemCount,
            Items = _mapper.Map<List<TModel>>(items)
        };

        return result;
    }

}
