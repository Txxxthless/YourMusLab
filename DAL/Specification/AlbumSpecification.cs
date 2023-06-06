using DAL.Specification.SpecificationParams;
using Domain.Entity;

namespace DAL.Specification
{
    public class AlbumSpecification : BaseSpecification<Album>
    {
        public AlbumSpecification(AlbumSpecificationParams albumParams)
            : base(
                album =>
                    (!albumParams.Id.HasValue || album.Id == albumParams.Id)
                    && (
                        string.IsNullOrEmpty(albumParams.Search)
                        || (
                            album.Name.ToLower().Contains(albumParams.Search.ToLower())
                            || album.Author.Name.ToLower().Contains(albumParams.Search.ToLower())
                        )
                    )
            )
        {
            AddInclude(album => album.Author);
            AddInclude(album => album.Genre);
            AddOrderBy(album => album.Name);
        }

        public AlbumSpecification(int id)
            : base(album => album.Id == id)
        {
            AddInclude(album => album.Author);
            AddInclude(album => album.Genre);
            AddOrderBy(album => album.Name);
        }
    }
}
