using AuthServer.Models.Title;

namespace AuthServer.Interfaces
{
    public interface ITitle
    {
        public FWTitle GetFWTitle();
        IEnumerable<FWTitle> GetTitles();
        FWTitle GetTitle(string? rawTitleId);
        Task<HttpResponseMessage> EditTitle(FWTitle Title);
        Task<HttpResponseMessage> CreateTitle(FWTitle Title);
        Task<HttpResponseMessage> DeleteTitle(string? rawTitleId);
    }
}
