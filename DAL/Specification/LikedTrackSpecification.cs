using Domain.Entity;

namespace DAL.Specification
{
    public class LikedTrackSpecification : BaseSpecification<LikedTrack>
    {
        public LikedTrackSpecification(int trackId, string email)
            : base(likedTrack => likedTrack.TrackId == trackId && likedTrack.UserEmail == email) { }

        public LikedTrackSpecification(string email)
            : base(likedTrack => likedTrack.UserEmail == email) { }
    }
}
