using DAL;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MusicController : BaseApiController
    {
        private readonly AppDbContext _context;

        public MusicController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Do()
        {
            return Ok(_context.Tracks.ToList());
        }
    }
}
