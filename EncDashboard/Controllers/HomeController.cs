using EncDashboard.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;

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
            await _apiServices.getToken();
            await _apiServices.getUserDetails();
            return View();
        }

        

    }
}