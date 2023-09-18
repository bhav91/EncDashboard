using EncDashboard.Models;
using EncDashboard.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EncDashboard.Controllers
{
    public class LoanController : Controller
    {
        private readonly IAppSettingsServices _appSettingsServices;

        public LoanController(IAppSettingsServices appSettingsServices)
        {
            _appSettingsServices = appSettingsServices;
            

        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult PipelineViews()
        {
            var matchingPersonas =_appSettingsServices.extractPersonas();
            var pipelineViews = _appSettingsServices.extractPipelineViews(matchingPersonas);
            return View(pipelineViews);
        }

    }
}
