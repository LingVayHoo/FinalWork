using AuthServer.Interfaces;
using AuthServer.Models.Content.Blog;
using Newtonsoft.Json;
using System.Text;

namespace FW_ASP.Net.Models.Content.Blog
{
    public class BlogApiModel : IBlog
    {
        private HttpClient Http_Client { get; set; }

        public BlogApiModel()
        {
            Http_Client = new HttpClient();
        }

        public IEnumerable<FWBlogItem> GetBlogItem()
        {
            string url = @"https://localhost:7195/api/FWBlogItems/PGetFWBlogItems";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<FWBlogItem>>(json);
        }

        public FWBlogItem GetBlogItem(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/FWBlogItems/PGetFWBlogItem/{rawUserId}";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<FWBlogItem>(json);
        }

        public async Task<HttpResponseMessage> EditBlogItem(FWBlogItem blogItem)
        {
            string url = @"https://localhost:7195/api/FWBlogItems/PPutBlogItem";
            var r = await Http_Client.PutAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(blogItem),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> CreateBlogItem(FWBlogItem blogItem)
        {
            string url = @"https://localhost:7195/api/FWBlogItems/PPostFWBlogItem";

            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(blogItem),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> DeleteBlogItem(string? rawUserId)
        {
            string url = @"https://localhost:7195/api/FWBlogItems/PDeleteFWBlogItem";

            var r = await Http_Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(rawUserId),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }
    }
}
