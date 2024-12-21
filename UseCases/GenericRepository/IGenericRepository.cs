using System.Linq.Expressions;
using UseCase.Commons;

namespace UseCases.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(object id);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<Pagination<T>> ToPagination(int pageNumber = 0, int pageSize = 10);


}