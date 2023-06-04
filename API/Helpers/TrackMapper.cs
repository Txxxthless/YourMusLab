using Domain.Entity;
using Domain.ViewModels;

namespace API.Helpers
{
    public class TrackMapper : IMapper<Track>
    {
        private readonly IConfiguration _configuration;

        public TrackMapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object MapOver(Track entity)
        {
            return new TrackViewModel()
            {
                Name = entity.Name,
                Genre = entity.Genre,
                Author = entity.Author,
                FilePath = _configuration["ApiUrl"] + entity.FilePath,
            };
        }

        public IEnumerable<object> MapOver(IEnumerable<Track> entityList)
        {
            var viewModels = new List<object>();
            foreach (var entity in entityList)
            {
                viewModels.Add(
                    new TrackViewModel()
                    {
                        Name = entity.Name,
                        Genre = entity.Genre,
                        Author = entity.Author,
                        FilePath = _configuration["ApiUrl"] + entity.FilePath,
                    }
                );
            }
            return viewModels;
        }
    }
}
