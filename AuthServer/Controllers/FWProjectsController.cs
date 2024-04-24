using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthServer.Models.Content.Projects;
using Microsoft.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using System.Drawing;
using AuthServer.Models;
using AuthServer.Models.DataBase;
using Microsoft.AspNetCore.Authorization;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FWProjectsController : ControllerBase
    {
        private readonly AuthenticationDbContext _context;

        public FWProjectsController(AuthenticationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PGetFWProjects")]
        public async Task<ActionResult<IEnumerable<FWProject>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet]
        [Route("PGetFWProject/{rawUserId}")]
        public async Task<ActionResult<FWProject>> GetProject(string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PPutFWProject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProject([FromBody] FWProject project)
        {
           
            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
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
        [Route("PPostFWProject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProject([FromBody] FWProject project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        [HttpPost]
        [Route("PDeleteFWProject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProject([FromBody] string? rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid id))
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            DeleteFile(project.ImgName);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("DownloadFile/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var image = System.IO.File.OpenRead($"Models/Content/img/{fileName}");
            return File(image, "image/jpeg");
        }

        public IActionResult DeleteFile(string fileName)
        {
            if (IsFileExists($"Models/Content/img/{fileName}"))
            {
                System.IO.File.Delete($"Models/Content/img/{fileName}");
            }    
            return Ok();
        }

        [HttpPost]
        [Route("UploadFile")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadFile([FromBody] FileModel fileModel)
        {
            string fileName = GetUniqueName();
            string path = $"Models/Content/img/{fileName}";
            using (Stream st = new FileStream(path, FileMode.Create))
            {
                await st.WriteAsync(fileModel.TargetFile, 0, fileModel.TargetFile.Length);
                return Ok(fileName);
            }            
        }



        private string GetUniqueName()
        {
            var r = GenerateFileName();
            if (IsFileExists($"Models/Content/img/{r}"))
            {
                return GetUniqueName();
            }
            else
            {
                return r;
            }
        }

        private bool IsFileExists(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            return file.Exists;
        }

        private string GenerateFileName()
        {
            return $"{Guid.NewGuid().ToString().Substring(0, 5)}.jpg";
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
