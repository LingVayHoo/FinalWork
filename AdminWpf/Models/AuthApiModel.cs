using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AuthServer;
using AuthServer.Models.Requests;
using AuthServer.Interfaces;

namespace AdminWpf.Models
{
    class AuthApiModel : IAuth
    {
        private HttpClient Http_Client { get; set; }

        public AuthApiModel()
        {
            Http_Client = new HttpClient();

        }

        public FWRegisterRequest GetFWRegisterRequest()
        {
            return new FWRegisterRequest();
        }

        public async Task<HttpResponseMessage> PostRegister(FWRegisterRequest fWRegisterRequest)
        {
            string url = @"https://localhost:7195/api/Auth/FWregister";

            var r = await Http_Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(fWRegisterRequest),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public FWLoginRequest GetFWLoginRequest()
        {
            return new FWLoginRequest();
        }

        public async Task<HttpResponseMessage> PostLogin(FWLoginRequest fWLoginRequest)
        {
            string url = @"https://localhost:7195/api/Auth/FWlogin";

            var r = await Http_Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(fWLoginRequest),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }



        public IEnumerable<WeatherForecast> GetData(string token)
        {
            string url = @"https://localhost:7195/api/Auth/test";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            try
            {
                string json = Http_Client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(json);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<HttpResponseMessage> Logout(string token, string id)
        {
            string url = @"https://localhost:7195/api/Auth/FWlogout/";// + id;
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(id),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            //var r = await Http_Client.DeleteAsync(url);
            return r;
        }
    }
}
