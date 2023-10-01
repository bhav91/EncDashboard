using EncDashboard.Models;
using EncDashboard.Models.Others;
using EncDashboard.Services.ApiServices;
using EncDashboard.Services.AppSettingServices;
using Microsoft.AspNetCore.Mvc;

namespace EncDashboard.Controllers
{
    public class LoanController : Controller
    {
        private readonly IAppSettingsServices _appSettingsServices;
        private readonly IApiServices _apiServices;
        private static readonly LoanViewModel _loanViewModel = new LoanViewModel() { Personas=new List<Persona>(),Columns=new List<string>() };
        public LoanController(IAppSettingsServices appSettingsServices,IApiServices apiServices)
        {
            _appSettingsServices = appSettingsServices;
            _apiServices = apiServices;
        }
        
        public IActionResult PipelineViews()
        {
            try
            {
                var _matchingPersonas = _appSettingsServices.extractPersonas();
                _loanViewModel.Personas = _matchingPersonas;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
            return View(_loanViewModel);
        }
        public async  Task<LoanViewModel> filterQuery(string view)
        {
            try
            {
                foreach (var persona in _loanViewModel.Personas)
                {
                    foreach (var pipelineView in persona.pipelineViews)
                    {
                        if (view == pipelineView.name)
                        {
                            _loanViewModel.Columns = pipelineView.resultFields;

                        }
                    }

                }
                var records=await _apiServices.getLoanRecords(_loanViewModel.Columns,view);
                if (records != null)
                {
                    _loanViewModel.LoanRecords = records;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return _loanViewModel;
        }

        
    }
}
