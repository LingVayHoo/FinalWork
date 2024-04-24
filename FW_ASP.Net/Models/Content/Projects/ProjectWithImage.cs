using System.Drawing;

namespace FW_ASP.Net.Models.Content.Projects
{
    public class ProjectWithImage
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImgName { get; set; } = string.Empty;
        public Image? FWImage { get; set; } 
    }
}
