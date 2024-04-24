using AuthServer.Models.Content.Requests;

namespace AuthServer.Interfaces
{
    public interface IRequest
    {
        public FWRequest GetFWRequest();
        IEnumerable<FWRequest> GetRequests();
        FWRequest GetRequest(string? rawRequestId);
        Task<HttpResponseMessage> EditRequest(FWRequest request);
        Task<HttpResponseMessage> CreateRequest(FWRequest request);
        Task<HttpResponseMessage> DeleteRequest(string? rawRequestId);
    }
}
