using System;
using System.Net;
using System.Threading.Tasks;
using delivery.backoffice.API;
using delivery.backoffice.API.Model.Proxy;
using delivery.backoffice.Controllers.Base;
using delivery.backoffice.ViewModel;
using Microsoft.AspNetCore.Mvc;
using RestEase;

namespace delivery.backoffice.Controllers
{
    public class DriverController : BaseController
    {
        private IDeliveryAPI _api;

        public DriverController(IDeliveryAPI api)
        {
            _api = api;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Estregadores";
            ViewData["Description"] = "Acompanhe os entregadores da plataforma";
            
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            
            ViewData["Title"] = "Detalhes do entregador";
            ViewData["Description"] = "Acompanhe o entregador em tempo real";
            var user = GetUserInfo();
            
            using (var proxy = await _api.GetDriver(user.Token, id))
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
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id,  bool isBlocked,  int level)
        {
            var user = GetUserInfo();
            
            using (var proxy = await _api.SetDriver(user.Token, id, isBlocked, level))
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Redirect("Index");
                    case HttpStatusCode.Unauthorized:
                        await Logout();
                        return Redirect("~/home/index");
                    default:
                        return Redirect("~/home/error");
                }
            }
        }

        [Post("list")]
        public async Task<IActionResult> List()
        {
            var user = GetUserInfo();

            var payload = GetDataTablePayload();

            try
            {
                using (var proxy = await _api.GetDrivers(user.Token, payload))
                {
                    switch (proxy.ResponseMessage.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            var list = proxy.GetContent();
                            return Json(list);
                        case HttpStatusCode.Unauthorized:
                            await Logout();
                            return Redirect("Home/Index");
                        default:
                            return Redirect("Error");
                    }
                }
            }
            catch (Exception e)
            {
                return Redirect("Error");
            }
        }
    }
}