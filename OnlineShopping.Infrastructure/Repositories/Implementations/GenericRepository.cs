
namespace OnlineShopping.Infrastructure.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entites = await _dbSet.ToListAsync();
            return entites;
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            var entity = await query.FirstOrDefaultAsync();
            return entity;
        }

        public async Task<List<T>?> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            var entities = await query.ToListAsync();
            return entities;
        }

        public async Task<T?> GetByIdAsync(int id) 
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        protected IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
            return query;
        }

        public T Update(T entity)
        {
            var updatedEntity = _dbSet.Update(entity).Entity;
            return updatedEntity;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
