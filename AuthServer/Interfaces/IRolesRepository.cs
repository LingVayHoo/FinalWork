using AuthServer.Models;
using AuthServer.Models.Auth;

namespace AuthServer.Interfaces
{
    public interface IRolesRepository
    {
        Task<CustomRole> GetById(Guid userId);
        Task Create(CustomRole customRole);
        Task Delete(Guid id);
    }
}
