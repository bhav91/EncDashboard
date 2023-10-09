using EncDashboard.Models;
using EncDashboard.Models.auth;
using EncDashboard.Services.ApiServices;
using EncDashboard.Services.AppSettingServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Web.Http;

namespace EncDashboard.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiServices _apiServices;

        public AuthController(IApiServices apiServices)
        {
           _apiServices = apiServices;

        }

        [System.Web.Http.HttpGet]
        public async Task<IActionResult> getToken(string username,string password)
        {
            
            var token = await _apiServices.getToken(username,password);
            if(token == null)
            {
                return Unauthorized(token);
            }
            else
            {
                return Ok(token);
            }

        }

        [System.Web.Http.HttpGet]
        public async Task<IActionResult> getPersonas(string username)
        {
            var personas = await _apiServices.getPersonas(username);
            if (personas == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(personas);
            }
        }

    }
}