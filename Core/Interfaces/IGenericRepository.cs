using Core.Entities;
using Core.Specification;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T:BaseEntity
{
   Task<IReadOnlyList<T>>  ListAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<List<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
}
