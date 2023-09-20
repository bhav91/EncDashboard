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
        private readonly IAppSettingsServices _appSettingsServices;
        private readonly IApiServices _apiServices;

        public HomeController(IAppSettingsServices appSettingsServices, IApiServices apiServices)
        {
            _appSettingsServices = appSettingsServices;
           _apiServices = apiServices;

        }

        public async Task<IActionResult> Index()
        {
            //intialization
            await _apiServices.getToken();
            await _apiServices.getPersonas();
            return View();
        }

    }
}