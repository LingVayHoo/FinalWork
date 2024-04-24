using AuthServer.Models.Content.Requests;
using AuthServer.Models.DataBase;
using AuthServer.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly AuthenticationDbContext _context;

        public RequestsController(AuthenticationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PGetRequests")]
        public async Task<ActionResult<IEnumerable<FWRequest>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        [HttpGet]
        [Route("PGetRequest/{rawUserId}")]
        public async Task<ActionResult<FWRequest>> GetRequest(string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PPutRequest")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRequest([FromBody] FWRequest request)
        {

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(request.Id))
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
        [Route("PPostRequest")]
        public async Task<IActionResult> PostRequest([FromBody] FWRequest request)
        {
            request.RequestStatus = "Получена";
            request.CreatedTime = DateTime.Now;
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        [HttpPost]
        [Route("PDeleteRequest")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRequest([FromBody] string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RequestExists(Guid id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
