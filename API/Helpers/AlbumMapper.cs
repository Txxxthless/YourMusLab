using Domain.Entity;
using Domain.ViewModels;

namespace API.Helpers
{
    public class AlbumMapper : BaseMapper<Album>
    {
        public AlbumMapper(IConfiguration configuration)
            : base(configuration) { }

        public override object MapOver(Album entity)
        {
            return new AlbumViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Author = entity.Author.Name,
                Genre = entity.Genre.Name,
                ReleaseYear = entity.ReleaseYear,
                PictureUrl = HandleUrl(entity.PictureUrl)
            };
        }

        public override IEnumerable<object> MapOver(IEnumerable<Album> entityList)
        {
            var viewModels = new List<object>();
            foreach (var entity in entityList)
            {
                viewModels.Add(MapOver(entity));
            }
            return viewModels;
        }
    }
}
