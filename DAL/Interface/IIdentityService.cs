using Domain.Entity;

namespace DAL.Interface
{
    public interface IIdentityService
    {  
        Task LikeTrackAsync(int trackId, string userEmail);
        Task UnlikeTrackAsync(int trackId, string userEmail);
        Task<bool> IsTrackLiked(int trackId, string userEmail);
        Task<IReadOnlyList<Track>> GetLikedTracksAsync(string userEmail);
    }
}