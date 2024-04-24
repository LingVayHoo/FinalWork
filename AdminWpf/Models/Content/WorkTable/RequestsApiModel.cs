using AuthServer.Interfaces;
using AuthServer.Models.Content.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models.Content
{
    internal class RequestsApiModel : IRequest
    {
        private HttpClient Http_Client { get; set; }
        public string AccessToken => TokenManager.GetAccessToken();

        public RequestsApiModel()
        {
            Http_Client = new HttpClient();
        }

        public FWRequest GetFWRequest()
        {
            return new FWRequest();
        }

        public IEnumerable<FWRequest> GetRequests()
        {
            string url = @"https://localhost:7195/api/Requests/PGetRequests";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<FWRequest>>(json);
        }

        public FWRequest GetRequest(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/Requests/PGetRequest/{rawUserId}";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<FWRequest>(json);
        }

        public async Task<HttpResponseMessage> EditRequest(FWRequest request)
        {
            string url = @"https://localhost:7195/api/Requests/PPutRequest";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

            var r = await Http_Client.PutAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> CreateRequest(FWRequest request)
        {
            string url = @"https://localhost:7195/api/Requests/PPostRequest";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> DeleteRequest(string? rawUserId)
        {
            string url = @"https://localhost:7195/api/Requests/PDeleteRequest";
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
    }
}
