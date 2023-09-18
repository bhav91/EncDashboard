﻿using EncDashboard.Models;
using EncDashboard.Services;
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
            var token =await _apiServices.getToken();
            _appSettingsServices.saveToken(token);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}