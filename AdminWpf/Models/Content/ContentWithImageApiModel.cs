using AuthServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models.Content
{
    public class ContentWithImageApiModel<T> : ContentApiModel<T>
    {
        public async Task<HttpResponseMessage> DownloadFile(string fileName)
        {
            string url = @"https://localhost:7195/api/FWProjects/DownloadFile";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

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

            string url = @"https://localhost:7195/api/FWProjects/UploadFile";
            Http_Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken);

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
