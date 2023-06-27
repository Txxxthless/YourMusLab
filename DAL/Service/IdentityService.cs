using DAL.Interface;
using DAL.Specification;
using DAL.Specification.SpecificationParams;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace DAL.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Track>> GetLikedTracksAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return null;
            }

            var specification = new LikedTrackSpecification(userEmail);
            var likedTracks = await _unitOfWork
                .Repository<LikedTrack>()
                .GetEntitiesBySpecification(specification);

            var tracks = new List<Track>();

            foreach (var likedTrack in likedTracks)
            {
                var trackSpecification = new TrackSpecification(new TrackSpecificationParams()
                {
                    Id = likedTrack.TrackId
                });
                tracks.Add(
                    await _unitOfWork.Repository<Track>().GetEntityBySpecification(trackSpecification));
            }

            return tracks;
        }

        public async Task<bool> IsTrackLiked(int trackId, string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return false;
            }

            var specification = new LikedTrackSpecification(trackId, userEmail);
            var likedTrack = await _unitOfWork
                .Repository<LikedTrack>()
                .GetEntityBySpecification(specification);

            return likedTrack != null;
        }

        public async Task LikeTrackAsync(int trackId, string userEmail)
        {
            var track = await _unitOfWork.Repository<Track>().GetByIdAsync(trackId);
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (track == null || user == null)
            {
                return;
            }

            var specification = new LikedTrackSpecification(trackId, userEmail);
            var likedTrack = await _unitOfWork
                .Repository<LikedTrack>()
                .GetEntityBySpecification(specification);

            if (likedTrack != null)
            {
                return;
            }

            likedTrack = new LikedTrack() { UserEmail = user.Email, TrackId = track.Id };

            _unitOfWork.Repository<LikedTrack>().Add(likedTrack);
            await _unitOfWork.CommitAsync();
        }

        public async Task UnlikeTrackAsync(int trackId, string userEmail)
        {
            var track = await _unitOfWork.Repository<Track>().GetByIdAsync(trackId);
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (track == null || user == null)
            {
                return;
            }

            var specification = new LikedTrackSpecification(trackId, userEmail);
            var likedTrack = await _unitOfWork
                .Repository<LikedTrack>()
                .GetEntityBySpecification(specification);

            if (likedTrack == null)
            {
                return;
            }

            _unitOfWork.Repository<LikedTrack>().Delete(likedTrack);
            await _unitOfWork.CommitAsync();
        }
    }
}
