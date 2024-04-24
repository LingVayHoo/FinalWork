using AuthServer.Interfaces;
using AuthServer.Models.Contacts;
using Newtonsoft.Json;
using System.Text;

namespace FW_ASP.Net.Models
{
    public class ContactsApiModel : IContacts
    {
        private HttpClient Http_Client { get; set; }

        public ContactsApiModel()
        {
            Http_Client = new HttpClient();
        }

        public FWContact GetFWContact()
        {
            return new FWContact();
        }

        public IEnumerable<FWContact> GetContacts()
        {
            string url = @"https://localhost:7195/api/FWContacts/PGetFWContacts";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<FWContact>>(json);
        }

        public FWContact GetContact(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/FWContacts/PGetFWContact/{rawUserId}";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<FWContact>(json);
        }

        public async Task<HttpResponseMessage> EditContact(FWContact contacts)
        {
            string url = @"https://localhost:7195/api/FWContacts/PPutFWContact";
            var r = await Http_Client.PutAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(contacts),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> CreateContact(FWContact contacts)
        {
            string url = @"https://localhost:7195/api/FWContacts/PPostFWContact";

            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(contacts),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> DeleteContact(string? rawUserId)
        {
            string url = @"https://localhost:7195/api/FWContacts/PDeleteFWContact";

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
