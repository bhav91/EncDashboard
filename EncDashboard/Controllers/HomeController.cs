using EncDashboard.Models;
using EncDashboard.Services.ApiServices;
using EncDashboard.Services.AppSettingServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EncDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiServices _apiServices;

        public HomeController(IApiServices apiServices)
        {
           _apiServices = apiServices;

        }

        public async Task<IActionResult> Index()
        {
            //intialization need to move somewhere
            await _apiServices.getToken();
            await _apiServices.getPersonas();
            return View();
        }

    }
}