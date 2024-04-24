using AuthServer.Interfaces;
using AuthServer.Models.Content.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models.Content.Services
{
    class ServicesApiModel : IServices
    {
        private HttpClient Http_Client { get; set; }

        public ServicesApiModel()
        {
            Http_Client = new HttpClient();
        }

        public IEnumerable<FWService> GetServices()
        {
            string url = @"https://localhost:7195/api/Services/PGetServices";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<FWService>>(json);
        }

        public FWService GetService(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/Services/PGetService/{rawUserId}";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<FWService>(json);
        }

        public async Task<HttpResponseMessage> EditService(FWService service)
        {
            string url = @"https://localhost:7195/api/Services/PPutService";
            var r = await Http_Client.PutAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(service),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> CreateService(FWService service)
        {
            string url = @"https://localhost:7195/api/Services/PPostService";

            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(service),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> DeleteService(string? rawUserId)
        {
            string url = @"https://localhost:7195/api/Services/PDeleteService";

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
