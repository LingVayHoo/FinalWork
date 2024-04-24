using AuthServer.Interfaces;
using AuthServer.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IRolesRepository _rolesRepository;

        public RolesController(UserManager<User> userManager, IRolesRepository rolesRepository)
        {
            _userManager = userManager;
            _rolesRepository = rolesRepository;            
        }

        [HttpPost, Route("FWCreateRole")]
        public async Task<IActionResult> Create([FromBody] CustomRole customRole)
        {
            if (IsValidID(customRole.Id))
            {
                await _rolesRepository.Create(customRole);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, Route("FWDeleteRole")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (IsValidID(id))
            {
                await _rolesRepository.Delete(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet, Route("FWGetUserRole")]
        public async Task<IActionResult> GetUserRoleById(Guid userId)
        {
            var r = await _rolesRepository.GetById(userId);
            if (r == null) return NotFound();
            else return Ok();
        }

        private bool IsValidID(Guid id)
        {
            return _userManager.Users.Any(x => x.Id == id);
        }
    }
}
