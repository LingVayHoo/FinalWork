using AuthServer.Interfaces;
using AuthServer.Models;
using AuthServer.Models.Content.Projects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminWpf.Models.Content.Projects
{
    class ProjectsApiModel : IProjects
    {
        private HttpClient Http_Client { get; set; }

        public ProjectsApiModel()
        {
            Http_Client = new HttpClient();
        }

        public IEnumerable<FWProject> GetProjects()
        {
            string url = @"https://localhost:7195/api/Projects/PGetProjects";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<FWProject>>(json);
        }

        public FWProject GetProject(string? rawUserId)
        {
            string url = @$"https://localhost:7195/api/Projects/PGetProject/{rawUserId}";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<FWProject>(json);
        }

        public async Task<HttpResponseMessage> EditProject(FWProject project)
        {
            string url = @"https://localhost:7195/api/Projects/PPutProject";
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
            string url = @"https://localhost:7195/api/Projects/PPostProject";

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
            string url = @"https://localhost:7195/api/Projects/PDeleteProject";

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
            string url = @"https://localhost:7195/api/Projects/DownloadFile";

            var r = await Http_Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(fileName),
                Encoding.UTF8,
                mediaType: "application/json")
                );

            return r;
        }

        public async Task<HttpResponseMessage> UploadFile(string filePath)
        {
            FileModel fileModel = new FileModel();
            using (FileStream st = File.OpenRead(filePath))
            {
                fileModel.TargetFile = new byte[st.Length];
                await st.ReadAsync(fileModel.TargetFile, 0, fileModel.TargetFile.Length);
            }

            string url = @"https://localhost:7195/api/Projects/UploadFile";

            var r = await Http_Client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(fileModel),
                Encoding.UTF8,
                mediaType: "application/json")
                );

            return r;
        }

       
    }
}
