using AuthServer;
using AuthServer.Interfaces;
using AuthServer.Models;
using AuthServer.Models.Content.Projects;
using AuthServer.Models.Requests;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FW_ASP.Net.Models.Content.Projects
{
    public class ProjectsApiModel : IProjects
    {
        private HttpClient Http_Client { get; set; }

        public ProjectsApiModel()
        {
            Http_Client = new HttpClient();
        }

        public IEnumerable<FWProject> GetProjects()
        {
            string url = @"https://localhost:7195/api/FWProjects/PGetFWProjects";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<FWProject>>(json);
        }

        public FWProject GetProject(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/FWProjects/PGetFWProject/{rawUserId}";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<FWProject>(json);
        }

        public async Task<HttpResponseMessage> EditProject(FWProject project)
        {
            string url = @"https://localhost:7195/api/FWProjects/PPutFWProject";
            var r = await Http_Client.PutAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(project),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> CreateProject(FWProject project)
        {
            string url = @"https://localhost:7195/api/FWProjects/PPostFWProject";

            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(project),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> DeleteProject(string? rawUserId)
        {
            string url = @"https://localhost:7195/api/FWProjects/PDeleteFWProject";

            var r = await Http_Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(rawUserId),
                Encoding.UTF8,
                mediaType: "application/json")
                );
            return r;
        }

        public async Task<HttpResponseMessage> DownloadFile(string fileName)
        {
            string url = @"https://localhost:7195/api/FWProjects/DownloadFile";

            var r = await Http_Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(fileName),
                Encoding.UTF8,
                mediaType: "application/json")
                );

            return r;
        }

        public Task<HttpResponseMessage> UploadFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
