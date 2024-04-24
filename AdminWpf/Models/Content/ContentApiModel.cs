using AuthServer.Interfaces;
using AuthServer.Models.Content.Services;
using Newtonsoft.Json;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models.Content
{
    public class ContentApiModel<T> : IContentModel<T>
    {
        protected HttpClient Http_Client { get; set; }

        public string AccessToken => TokenManager.GetAccessToken();

        public ContentApiModel()
        {
            Http_Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> CreateContent(T content)
        {
            string url = @$"https://localhost:7195/api/{typeof(T).Name}s/PPost{typeof(T).Name}";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> DeleteContent(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/{typeof(T).Name}s/PDelete{typeof(T).Name}";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

            var r = await Http_Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(rawUserId),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> EditContent(T content)
        {
            string url = @$"https://localhost:7195/api/{typeof(T).Name}s/PPut{typeof(T).Name}";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

            var r = await Http_Client.PutAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public IEnumerable<T> GetAllContent()
        {
            string url = @$"https://localhost:7195/api/{typeof(T).Name}s/PGet{typeof(T).Name}s";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        public T GetSingleContent(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/{typeof(T).Name}s/PGet{typeof(T).Name}/{rawUserId}";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
