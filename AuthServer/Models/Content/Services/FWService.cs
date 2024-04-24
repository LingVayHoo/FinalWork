using AuthServer.Interfaces;

namespace AuthServer.Models.Content.Services
{
    public class FWService
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
