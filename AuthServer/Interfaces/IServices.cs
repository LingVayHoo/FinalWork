using AuthServer.Models.Content.Services;

namespace AuthServer.Interfaces
{
    public interface IServices 
    {
        IEnumerable<FWService> GetServices();
        FWService GetService(string? rawUserId);
        Task<HttpResponseMessage> EditService(FWService service);
        Task<HttpResponseMessage> CreateService(FWService service);
        Task<HttpResponseMessage> DeleteService(string? rawUserId);
    }
}
