using AuthServer.Models.Content.Blog;
using AuthServer.Models.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FWBlogItemsController : ControllerBase
    {
        private readonly AuthenticationDbContext _context;

        public FWBlogItemsController(AuthenticationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PGetFWBlogItems")]
        public async Task<ActionResult<IEnumerable<FWBlogItem>>> GetBlog()
        {
            return await _context.Blog.ToListAsync();
        }

        [HttpGet]
        [Route("PGetFWBlogItem/{rawUserId}")]
        public async Task<ActionResult<FWBlogItem>> GetBlogItem(string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var blogItem = await _context.Blog.FindAsync(id);

            if (blogItem == null)
            {
                return NotFound();
            }

            return blogItem;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PPutFWBlogItem")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBlogItem([FromBody] FWBlogItem blogItem)
        {

            _context.Entry(blogItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(blogItem.Id))
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
        [Route("PPostFWBlogItem")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostBlogItem([FromBody] FWBlogItem blogItem)
        {
            _context.Blog.Add(blogItem);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        [HttpPost]
        [Route("PDeleteFWBlogItem")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBlogItem([FromBody] string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var blogItem = await _context.Blog.FindAsync(id);
            if (blogItem == null)
            {
                return NotFound();
            }

            _context.Blog.Remove(blogItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ServiceExists(Guid id)
        {
            return _context.Blog.Any(e => e.Id == id);
        }
    }
}
