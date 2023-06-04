using DAL.Interface;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<T> GetEntityBySpecification(ISpecification<T> specification)
        {
            var query = ApplySpecification(specification);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetEntitiesBySpecification(
            ISpecification<T> specification
        )
        {
            var query = ApplySpecification(specification);
            return await query.ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(
                _context.Set<T>().AsQueryable(),
                specification
            );
        }
    }
}
