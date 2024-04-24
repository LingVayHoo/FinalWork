using AuthServer.Interfaces;
using AuthServer.Models.Content.Projects;
using AuthServer.Models.Responce;
using FW_ASP.Net.Models.Content.Projects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

namespace FW_ASP.Net.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjects _projects;

        public ProjectsController(IProjects projects)
        {
            _projects = projects;
        }

        public IActionResult Index()
        {

            return View(_projects.GetProjects());
        }

        public IActionResult Test()
        {
            return View(_projects.GetProjects());
        }

        private ProjectWithImage Convert(FWProject project, Image image)
        {
            var r = new ProjectWithImage
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ImgName = project.ImgName,
                FWImage = image
            };
            return r;
        }

        //private async void ConvertToList(IEnumerable<FWProject> projects)
        //{
        //    List<ProjectWithImage> l = new List<ProjectWithImage>();
        //    foreach (var e in projects)
        //    {
        //        var r = await _projects.DownloadFile(e.ImgName);
        //        if (r.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            string json = r.Content.ReadAsStringAsync().Result;
        //            var result = JsonConvert.DeserializeObject<AuthenticatedUserResponce>(json);
        //        }

        //        r.Add(Convert(e, _projects.DownloadFile(e.ImgName).Result.Content.);
        //    }
        //}
    }
}
