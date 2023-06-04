using DAL.Specification.SpecificationParams;
using Domain.Entity;

namespace DAL.Specification
{
    public class TrackSpecification : BaseSpecification<Track>
    {
        public TrackSpecification(TrackSpecificationParams trackParams)
            : base(
                track =>
                    (!trackParams.Id.HasValue || track.Id == trackParams.Id)
                    && (
                        string.IsNullOrEmpty(trackParams.Search)
                        || (
                            track.Name.ToLower().Contains(trackParams.Search.ToLower())
                            || track.Author.ToLower().Contains(trackParams.Search.ToLower())
                        )
                    )
            )
        {
            AddOrderBy(track => track.Name);
        }

        public TrackSpecification(int id)
            : base(track => track.Id == id)
        {
            AddOrderBy(track => track.Name);
        }
    }
}
