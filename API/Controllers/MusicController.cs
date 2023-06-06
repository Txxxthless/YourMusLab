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
        private readonly IGenericRepository<Track> _trackRepository;
        private readonly IGenericRepository<Album> _albumRepository;
        private readonly IMapper<Track> _trackMapper;
        private readonly IMapper<Album> _albumMapper;

        public MusicController(
            IGenericRepository<Track> trackRepository,
            IMapper<Track> mapper,
            IGenericRepository<Album> albumRespository,
            IMapper<Album> albumMapper
        )
        {
            _trackRepository = trackRepository;
            _albumRepository = albumRespository;
            _trackMapper = mapper;
            _albumMapper = albumMapper;
        }

        [HttpGet]
        [Route("track")]
        public async Task<ActionResult> GetTack([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var track = await _trackRepository.GetEntityBySpecification(specification);
            return Ok(_trackMapper.MapOver(track));
        }

        [HttpGet]
        [Route("tracks")]
        public async Task<ActionResult> GetTacks([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var tracks = await _trackRepository.GetEntitiesBySpecification(specification);
            return Ok(_trackMapper.MapOver(tracks));
        }

        [HttpGet]
        [Route("albums")]
        public async Task<ActionResult> GetAlbums([FromQuery] AlbumSpecificationParams albumParams)
        {
            var specification = new AlbumSpecification(albumParams);
            var albums = await _albumRepository.GetEntitiesBySpecification(specification);
            return Ok(_albumMapper.MapOver(albums));
        }
    }
}
