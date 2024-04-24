namespace AuthServer.Models.Content.Blog
{
    public class FWBlogItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
    }
}
