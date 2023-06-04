using Domain.Entity;

namespace API.Helpers
{
    public interface IMapper<T>
        where T : BaseEntity
    {
        object MapOver(T entity);
        IEnumerable<object> MapOver(IEnumerable<T> entityList);
    }
}
