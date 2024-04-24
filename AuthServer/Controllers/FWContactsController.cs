using AuthServer.Models.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthServer.Models.Contacts;
using Microsoft.AspNetCore.Authorization;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FWContactsController : ControllerBase
    {
        private readonly AuthenticationDbContext _context;

        public FWContactsController(AuthenticationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PGetFWContacts")]
        public async Task<ActionResult<IEnumerable<FWContact>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        [HttpPut("PPutFWContact")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutContact([FromBody] FWContact Contact)
        {

            _context.Entry(Contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Contact.Id))
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
        [Route("PPostFWContact")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostContact([FromBody] FWContact Contact)
        {
            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("PDeleteFWContact")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteContact([FromBody] string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var Contact = await _context.Contacts.FindAsync(id);
            if (Contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(Contact);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
