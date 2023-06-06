using Domain.Entity;

namespace API.Helpers
{
    public abstract class BaseMapper<T> : IMapper<T>
        where T : BaseEntity
    {
        protected readonly IConfiguration _configuration;

        public BaseMapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract object MapOver(T entity);
        public abstract IEnumerable<object> MapOver(IEnumerable<T> entityList);

        protected string HandleUrl(string uri)
        {
            return _configuration["ApiUrl"] + uri;
        }
    }
}
