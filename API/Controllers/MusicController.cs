using System.Security.Claims;
using API.Helpers;
using DAL.Interface;
using DAL.Specification;
using DAL.Specification.SpecificationParams;
using Domain.Entity;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MusicController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Track> _trackMapper;
        private readonly IMapper<Album> _albumMapper;
        private readonly IIdentityService _identityService;

        public MusicController(
            IUnitOfWork unitOfWork,
            IMapper<Track> mapper,
            IMapper<Album> albumMapper,
            IIdentityService identityService
        )
        {
            _unitOfWork = unitOfWork;
            _trackMapper = mapper;
            _albumMapper = albumMapper;
            _identityService = identityService;
        }

        [Cached(600)]
        [HttpGet]
        [Route("track")]
        public async Task<ActionResult> GetTrack([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var track = await _unitOfWork
                .Repository<Track>()
                .GetEntityBySpecification(specification);
            return Ok(_trackMapper.MapOver(track));
        }

        [Cached(600)]
        [HttpGet]
        [Route("tracks")]
        public async Task<ActionResult> GetTacks([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var tracks = await _unitOfWork
                .Repository<Track>()
                .GetEntitiesBySpecification(specification);
            return Ok(_trackMapper.MapOver(tracks));
        }

        [Cached(600)]
        [HttpGet]
        [Route("albums")]
        public async Task<ActionResult> GetAlbums([FromQuery] AlbumSpecificationParams albumParams)
        {
            var specification = new AlbumSpecification(albumParams);
            var albums = await _unitOfWork
                .Repository<Album>()
                .GetEntitiesBySpecification(specification);
            return Ok(_albumMapper.MapOver(albums));
        }

        [Cached(600)]
        [HttpGet]
        [Route("genres")]
        public async Task<ActionResult> GetGenres()
        {
            return Ok(await _unitOfWork.Repository<Genre>().GetAllAsync());
        }

        [Cached(600)]
        [HttpGet]
        [Route("authors")]
        public async Task<ActionResult> GetAuthors()
        {
            return Ok(await _unitOfWork.Repository<Author>().GetAllAsync());
        }

        [HttpPost]
        [Route("liketrack")]
        [Authorize]
        public async Task<ActionResult> LikeTrack(LikedTrackViewModel likedTrackViewModel)
        {
            var email = GetCurrentUserEmail();
            await _identityService.LikeTrackAsync(likedTrackViewModel.TrackId, email);
            return Ok();
        }

        [HttpPost]
        [Route("unliketrack")]
        [Authorize]
        public async Task<ActionResult> UnlikeTrack(LikedTrackViewModel likedTrackViewModel)
        {
            var email = GetCurrentUserEmail();
            await _identityService.UnlikeTrackAsync(likedTrackViewModel.TrackId, email);
            return Ok();
        }

        [HttpGet]
        [Route("getliked")]
        public async Task<ActionResult> Get()
        {
            return Ok(await _unitOfWork.Repository<LikedTrack>().GetAllAsync());
        }

        [HttpGet]
        [Authorize]
        [Route("likedtracks")]
        public async Task<ActionResult> GetLikedTracks()
        {
            var email = GetCurrentUserEmail();
            var likedTracks = await _identityService.GetLikedTracksAsync(email);
            return Ok(_trackMapper.MapOver(likedTracks));
        }

        [HttpGet]
        [Route("isliked")]
        [Authorize]
        public async Task<ActionResult> IsLiked(int trackId)
        {
            var email = GetCurrentUserEmail();
            return Ok(await _identityService.IsTrackLiked(trackId, email));
        }

        private string GetCurrentUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
