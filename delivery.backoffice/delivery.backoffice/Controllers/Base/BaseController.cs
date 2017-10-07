using System;
using System.Linq;
using System.Security.Claims;
using delivery.backoffice.API.Model.Proxy;
using delivery.backoffice.Model;
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

        public DataTablePayload GetDataTablePayload()
        {
            var payload = new DataTablePayload();
            
            payload.Draw = HttpContext.Request.Form["draw"].FirstOrDefault();  
  
            // Skip number of Rows count  
            payload.Skip  = Request.Form["start"].FirstOrDefault() != null ? Convert.ToInt32(Request.Form["start"].FirstOrDefault()) : 0;  
  
            // Paging Length 10,20  
            payload.PageSize = Request.Form["length"].FirstOrDefault() != null ? Convert.ToInt32(Request.Form["length"].FirstOrDefault()) : 0;  
  
            // Sort Column Name  
            payload.SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();  
  
            // Sort Column Direction (asc, desc)  
            payload.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();  
  
            // Search Value from (Search box)  
            payload.SearchValue = Request.Form["search[value]"].FirstOrDefault();

            return payload;

        }
        
    }
}