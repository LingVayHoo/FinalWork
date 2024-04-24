using AuthServer.Interfaces;

namespace AuthServer.Models.Content.Projects
{
    public class FWProject
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImgName { get; set; } = string.Empty;
    }
}
