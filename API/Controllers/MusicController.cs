using System.Security.Claims;
using API.Helpers;
using DAL.Interface;
using DAL.Specification;
using DAL.Specification.SpecificationParams;
using Domain.Entity;
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

        [HttpGet]
        [Route("track")]
        public async Task<ActionResult> GetTack([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var track = await _unitOfWork
                .Repository<Track>()
                .GetEntityBySpecification(specification);
            return Ok(_trackMapper.MapOver(track));
        }

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

        [HttpGet]
        [Route("genres")]
        public async Task<ActionResult> GetGenres()
        {
            return Ok(await _unitOfWork.Repository<Genre>().GetAllAsync());
        }

        [HttpGet]
        [Route("authors")]
        public async Task<ActionResult> GetAuthors()
        {
            return Ok(await _unitOfWork.Repository<Author>().GetAllAsync());
        }

        [HttpPost]
        [Route("liketrack")]
        [Authorize]
        public async Task<ActionResult> LikeTrack([FromQuery] int trackId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            await _identityService.LikeTrackAsync(trackId, email);
            return Ok();
        }

        [HttpPost]
        [Route("unliketrack")]
        [Authorize]
        public async Task<ActionResult> UnlikeTrack([FromQuery] int trackId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            await _identityService.UnlikeTrackAsync(trackId, email);
            return Ok();
        }

        [HttpGet]
        [Route("getliked")]
        public async Task<ActionResult> Get()
        {
            return Ok(await _unitOfWork.Repository<LikedTrack>().GetAllAsync());
        }
    }
}
