using Domain.Entity;
using Domain.ViewModels;

namespace API.Helpers
{
    public class TrackMapper : BaseMapper<Track>
    {
        public TrackMapper(IConfiguration configuration)
            : base(configuration) { }

        public override object MapOver(Track entity)
        {
            return new TrackViewModel()
            {
                Name = entity.Name,
                Genre = entity.Genre.Name,
                Author = entity.Author.Name,
                Album = entity.Album.Name,
                PicturePath = HandleUrl(entity.Album.PictureUrl),
                FilePath = HandleUrl(entity.FilePath),
            };
        }

        public override IEnumerable<object> MapOver(IEnumerable<Track> entityList)
        {
            var viewModels = new List<object>();
            foreach (var entity in entityList)
            {
                viewModels.Add(
                    new TrackViewModel()
                    {
                        Name = entity.Name,
                        Genre = entity.Genre.Name,
                        Author = entity.Author.Name,
                        Album = entity.Album.Name,
                        PicturePath = HandleUrl(entity.Album.PictureUrl),
                        FilePath = HandleUrl(entity.FilePath),
                    }
                );
            }
            return viewModels;
        }
    }
}
