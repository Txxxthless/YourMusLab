using Domain.Entity;

namespace DAL.Interface
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetEntityBySpecification(ISpecification<T> specification);
        T Add(T entity);
        T Delete(T entity);
        Task<IReadOnlyList<T>> GetEntitiesBySpecification(ISpecification<T> specification);
    }
}
