using AuthServer.Models.Content.Blog;

namespace AuthServer.Interfaces
{
    public interface IBlog
    {
        IEnumerable<FWBlogItem> GetBlogItem();
        FWBlogItem GetBlogItem(string? rawUserId);
        Task<HttpResponseMessage> EditBlogItem(FWBlogItem blogItem);
        Task<HttpResponseMessage> CreateBlogItem(FWBlogItem blogItem);
        Task<HttpResponseMessage> DeleteBlogItem(string? rawUserId);
    }
}
