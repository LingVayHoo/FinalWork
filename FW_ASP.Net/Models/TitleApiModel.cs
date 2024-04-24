using AuthServer.Interfaces;
using AuthServer.Models.Title;
using Newtonsoft.Json;
using System.Text;

namespace FW_ASP.Net.Models
{
    public class TitleApiModel :ITitle
    {
        private HttpClient Http_Client { get; set; }

        public TitleApiModel()
        {
            Http_Client = new HttpClient();
        }

        public FWTitle GetFWTitle()
        {
            return new FWTitle();
        }

        public IEnumerable<FWTitle> GetTitles()
        {
            string url = @"https://localhost:7195/api/Titles/PGetTitles";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<FWTitle>>(json);
        }

        public FWTitle GetTitle(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/Titles/PGetTitle/{rawUserId}";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<FWTitle>(json);
        }

        public async Task<HttpResponseMessage> EditTitle(FWTitle Title)
        {
            string url = @"https://localhost:7195/api/Titles/PPutTitle";
            var r = await Http_Client.PutAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(Title),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> CreateTitle(FWTitle Title)
        {
            string url = @"https://localhost:7195/api/Titles/PPostTitle";

            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(Title),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> DeleteTitle(string? rawUserId)
        {
            string url = @"https://localhost:7195/api/Titles/PDeleteTitle";

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
