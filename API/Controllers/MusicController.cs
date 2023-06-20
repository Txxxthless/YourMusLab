using API.Helpers;
using DAL.Interface;
using DAL.Specification;
using DAL.Specification.SpecificationParams;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MusicController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Track> _trackMapper;
        private readonly IMapper<Album> _albumMapper;

        public MusicController(
            IUnitOfWork unitOfWork,
            IMapper<Track> mapper,
            IMapper<Album> albumMapper
        )
        {
            _unitOfWork = unitOfWork;
            _trackMapper = mapper;
            _albumMapper = albumMapper;
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
    }
}
