using AuthServer.Models.Content.Projects;
using AuthServer.Models.DataBase;
using AuthServer.Models.Title;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly AuthenticationDbContext _context;

        public TitlesController(AuthenticationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PGetTitles")]
        public async Task<ActionResult<IEnumerable<FWTitle>>> GetTitles()
        {
            return await _context.Titles.ToListAsync();
        }

        [HttpPut("PPutTitle")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTitle([FromBody] FWTitle title)
        {

            _context.Entry(title).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(title.Id))
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

        [HttpPost]
        [Route("PPostProject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProject([FromBody] FWTitle title)
        {
            _context.Titles.Add(title);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("PDeleteProject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProject([FromBody] string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var title = await _context.Titles.FindAsync(id);
            if (title == null)
            {
                return NotFound();
            }

            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
