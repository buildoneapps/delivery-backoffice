using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using delivery.backoffice.API;
using delivery.backoffice.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace delivery.backoffice.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private IDeliveryAPI _api;

        public HomeController(IDeliveryAPI api)
        {
            _api = api;
        }
        
        public async Task<IActionResult> Index()
        {
            
            ViewData["Title"] = "Dashboard";
            ViewData["Description"] = "Acompanhe a plataforma em tempo real";
            var user = GetUserInfo();
            
            using (var proxy = await _api.GetDashInfo(user.Token, 0))
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var f = proxy.GetContent();
                        return View(f);
                    case HttpStatusCode.Unauthorized:
                        await Logout();
                        return Redirect("Home/Index");
                    default:
                        return Redirect("Error");
                }
            }
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}