using System.Linq;
using System.Security.Claims;
using delivery.backoffice.API.Model.Proxy;
using Microsoft.AspNetCore.Mvc;
using delivery.backoffice.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace delivery.backoffice.Controllers.Base
{
    public class BaseController : Controller
    {
        public UserProxy GetUserInfo()
        {
            if (HttpContext.User != null)
            {
                var loggedInUser = HttpContext.User;
                return JsonConvert.DeserializeObject<UserProxy>(loggedInUser.Identity.Name);
            }
            return null;
        }
        
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(GetUserInfo() != null)
                ViewData["UserInfo"] = GetUserInfo();
        }
        
    }
}