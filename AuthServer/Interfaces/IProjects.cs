using AuthServer.Models;
using AuthServer.Models.Content.Projects;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Interfaces
{
    public interface IProjects
    {
        IEnumerable<FWProject> GetProjects();
        FWProject GetProject(string? rawUserId);
        Task<HttpResponseMessage> EditProject(FWProject project);
        Task<HttpResponseMessage> CreateProject(FWProject project);
        Task<HttpResponseMessage> DeleteProject(string? rawUserId);
        Task<HttpResponseMessage> DownloadFile(string fileName);
        Task<HttpResponseMessage> UploadFile(string filePath);
    }
}
