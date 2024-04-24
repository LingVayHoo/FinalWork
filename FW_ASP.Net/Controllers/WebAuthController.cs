using AuthServer.Interfaces;
using AuthServer.Models.Requests;
using AuthServer.Models.Responce;
using Azure;
using FW_ASP.Net.Interfaces;
using FW_ASP.Net.Models;
using FW_ASP.Net.Models.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FW_ASP.Net.Controllers
{
    public class WebAuthController : Controller
    {
        private readonly IAuth _authModel;
        private readonly IClaimsGetter _claimsGetter;

        public WebAuthController(IAuth authModel, IClaimsGetter claimsGetter)
        {
            _authModel = authModel;
            _claimsGetter = claimsGetter;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(_authModel.GetFWRegisterRequest());
        }

        public async Task<IActionResult> Register(FWRegisterRequest fWRegisterRequest)
        {
            var r = await _authModel.PostRegister(fWRegisterRequest);
            if(r.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("RegisterDone", "WebAuth");
            }
            else
            {
                return RedirectToAction("Register", "WebAuth");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(_authModel.GetFWLoginRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Login(FWLoginRequest model)
        {
            CustomUser.IsAuthenticated = false;
            CustomUser.UserName = string.Empty;
            var r = await _authModel.PostLogin(model);
            if (r.StatusCode == System.Net.HttpStatusCode.OK)
            {
                
                string json = r.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticatedUserResponce>(json);
                Response.Cookies.Append("tasty-cookies", $"{result.AccessToken}");
                CustomUser.IsAuthenticated = true;
                CustomUser.UserName = model.UserName;

                return RedirectToAction("Index", "WebAuth");
            }
            else return RedirectToAction("Index", "Home");
        }

        public IActionResult RegisterDone()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var id = _claimsGetter.GetClaim(Request.Cookies["tasty-cookies"], "id");
            var r = await _authModel.Logout(Request.Cookies["tasty-cookies"], id);
            
            if (r.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Response.Cookies.Delete("tasty-cookies");
                CustomUser.IsAuthenticated = false;
                CustomUser.UserName = string.Empty;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "WebAuth");
            }
        }

        //[Authorize]
        public IActionResult Index() 
        {
            if (_authModel.GetData(Request.Cookies["tasty-cookies"]) != null)
            {
                return View(_authModel.GetData(Request.Cookies["tasty-cookies"]));
            }
            else
            {
                return RedirectToAction("Login", "WebAuth");
            }            
        }
    }
}
