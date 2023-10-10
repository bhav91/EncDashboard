using EncDashboard.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;

namespace EncDashboard.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiServices _apiServices;

        public AuthController(IApiServices apiServices)
        {
           _apiServices = apiServices;

        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> getUserDetails(string username)
        {
            var userDetail = await _apiServices.getUserDetails(username);
            if (userDetail == null)
            {
                return NotFound(userDetail);
            }
            else
            {
                if(userDetail.error != null)
                {
                    return Unauthorized(userDetail.error);
                }
                return Ok(userDetail);
            }
        }

    }
}