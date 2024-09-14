using OnlineShopping.Core.Specifications;

namespace OnlineShopping.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<List<T>?> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Remove(T entity);
    }
}
