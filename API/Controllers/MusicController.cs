using API.Helpers;
using DAL;
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
        private readonly IMapper<Track> _mapper;

        public MusicController(IGenericRepository<Track> trackRepository, IMapper<Track> mapper)
        {
            _trackRepository = trackRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetTack([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var track = await _trackRepository.GetEntityBySpecification(specification);
            return Ok(_mapper.MapOver(track));
        }

        [HttpGet]
        [Route("tracks")]
        public async Task<ActionResult> GetTacks([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var tracks = await _trackRepository.GetEntitiesBySpecification(specification);
            return Ok(_mapper.MapOver(tracks));
        }
    }
}
