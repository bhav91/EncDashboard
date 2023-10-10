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


        [HttpGet]
        public IActionResult PipelineViews()
        {
            try
            {
                var pipelineViews = new List<string>();
                var _matchingPersonas = _appSettingsServices.extractPersonas();
                if(_matchingPersonas.Count != 0) 
                {
                    foreach(var persona in _matchingPersonas)
                    {
                        foreach(var pipelineView in persona.pipelineViews)
                        {
                            pipelineViews.Add(pipelineView.name);
                        }
                    }
                    _loanViewModel.Personas = _matchingPersonas;
                    return Ok(pipelineViews);
                }
                return NotFound(pipelineViews);
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        public async  Task<IActionResult> filterQuery(string view)
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
                    return Ok(new { _loanViewModel.Columns,_loanViewModel.LoanRecords });
                }


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            return NotFound();
        }

        
    }
}
