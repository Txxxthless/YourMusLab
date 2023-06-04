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

        public MusicController(IGenericRepository<Track> trackRepository)
        {
            _trackRepository = trackRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTack([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var track = await _trackRepository.GetEntityBySpecification(specification);
            return Ok(track);
        }

        [HttpGet]
        [Route("tracks")]
        public async Task<ActionResult> GetTacks([FromQuery] TrackSpecificationParams trackParams)
        {
            var specification = new TrackSpecification(trackParams);
            var track = await _trackRepository.GetEntitiesBySpecification(specification);
            return Ok(track);
        }
    }
}
