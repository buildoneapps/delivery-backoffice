using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using delivery.backoffice.API;
using delivery.backoffice.API.Model.Proxy;
using delivery.backoffice.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestEase;
using delivery.backoffice.Utils;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;


namespace delivery.backoffice.Controllers
{
    public class AccountController : Controller
    {

        private IDeliveryAPI _api;

        public AccountController(IDeliveryAPI api)
        {
            _api = api;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm] LoginModel loginModel)
        {
            try
            {
                var token = await _api.Authenticate(new Dictionary<string, object>
                {
                    {"username", loginModel.username},
                    {"password", loginModel.password}
                }, true);
                
                var user = await _api.GetUser(token.Token);
            
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(user)),
                };
                
                var userIdentity = new ClaimsIdentity(claims, "login");
     
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.Authentication.SignInAsync("CookieAuthentication", principal);
 
                //Just redirect to our index after logging in. 
                return Ok(new { accessGranted = true});
                
            }
            catch (RestEase.ApiException exception)
            {
                var result = JsonConvert.DeserializeObject<AuthFailed>(exception.Content);
                result.AccessGranted = false;
                
                return Ok(result);
            }
          
        }
 
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("CookieAuthentication");
            return Ok(new
            {
                Ok = true
            });
        }
        
        
        public IActionResult Forbidden()
        {
            ViewData["Message"] = "Your Forbidden page.";

            return View();
        }
    }
}