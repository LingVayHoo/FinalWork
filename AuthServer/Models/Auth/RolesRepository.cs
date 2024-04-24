using AuthServer.Interfaces;
using AuthServer.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Models.Auth
{
    public class RolesRepository : IRolesRepository
    {
        private readonly AuthenticationDbContext _context;

        public RolesRepository(AuthenticationDbContext context)
        {
            _context = context;
        }

        public async Task Create(CustomRole customRole)
        {
            _context.CustomRoles.Add(customRole);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            CustomRole? customRole = await _context.CustomRoles.FindAsync(id);
            if (customRole != null)
            {
                _context.CustomRoles.Remove(customRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CustomRole> GetById(Guid userId)
        {
            return await _context.CustomRoles.FirstOrDefaultAsync(t => t.Id == userId);
        }
    }
}
