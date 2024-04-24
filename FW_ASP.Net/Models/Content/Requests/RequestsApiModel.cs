using AuthServer.Interfaces;
using AuthServer.Models.Content.Requests;
using Newtonsoft.Json;
using System.Text;

namespace FW_ASP.Net.Models.Content.Requests
{
    public class RequestsApiModel : IRequest
    {
        private HttpClient Http_Client { get; set; }

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
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<FWRequest>>(json);
        }

        public FWRequest GetRequest(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/Requests/PGetRequest/{rawUserId}";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<FWRequest>(json);
        }

        public async Task<HttpResponseMessage> EditRequest(FWRequest request)
        {
            string url = @"https://localhost:7195/api/Requests/PPutRequest";
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
