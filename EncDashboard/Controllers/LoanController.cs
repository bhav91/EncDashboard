using EncDashboard.Models;
using EncDashboard.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EncDashboard.Controllers
{
    public class LoanController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAppSettingsServices _appSettingsServices;
        public LoanController(IConfiguration configuration, IAppSettingsServices appSettingsServices)
        {
            _appSettingsServices = appSettingsServices;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult PipelineViews()
        {
            var matchingPersonas=_appSettingsServices.extractPersonas();
            var pipelineViews = _appSettingsServices.extractPipelineViews(matchingPersonas);
            return View(pipelineViews);
        }

        [HttpPost]
        public IActionResult filterQuery()
        {
            
            return View("Index");
        }
    }
}
