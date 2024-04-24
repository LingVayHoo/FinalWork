using AuthServer.Models.Content.Services;
using AuthServer.Models.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FWServicesController : ControllerBase
    {
        private readonly AuthenticationDbContext _context;

        public FWServicesController(AuthenticationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PGetFWServices")]
        public async Task<ActionResult<IEnumerable<FWService>>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        [HttpGet]
        [Route("PGetFWService/{rawUserId}")]
        public async Task<ActionResult<FWService>> GetService(string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PPutFWService")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutService([FromBody] FWService service)
        {

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(service.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("PPostFWService")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostService([FromBody] FWService service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        [HttpPost]
        [Route("PDeleteFWService")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteService([FromBody] string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ServiceExists(Guid id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
