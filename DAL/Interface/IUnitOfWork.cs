using Domain.Entity;

namespace DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>()
            where T : BaseEntity;
        Task CommitAsync();
    }
}
