using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundWave.Server.Data;
using SoundWave.Server.DTOs;
using SoundWave.Server.Mapper;
using SoundWave.Server.Utilities;

namespace SoundWave.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly DataContext _context;

        public SecurityController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("/register")]
        public async Task<ActionResult<SecurityResponse>> Register(SecurityRequest user)
        {
            _context.Users.Add(user.ToEntity());
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("/login")]
        public async Task<ActionResult<SecurityResponse>> Login(SecurityRequest user)
        {
            var found = await _context.Users
            .FirstOrDefaultAsync(p => p.Login == user.Login && p.Password == Encoder.ComputeSHA256Hash(user.Password));

            return found == null ? Ok(found) : BadRequest();

        }

    }
}
