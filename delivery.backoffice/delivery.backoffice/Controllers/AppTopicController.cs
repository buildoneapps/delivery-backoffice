using System;
using System.Net;
using System.Threading.Tasks;
using delivery.backoffice.API;
using delivery.backoffice.API.Model.Proxy;
using delivery.backoffice.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestEase;

namespace delivery.backoffice.Controllers
{
    
    [Authorize]
    public class AppTopicController : BaseController
    {
        private IDeliveryAPI _api;

        public AppTopicController(IDeliveryAPI api)
        {
            _api = api;
        }
        
        // GET
        [HttpGet]
        public async Task<IActionResult> Create([FromQuery]Guid? id)
        {
            ViewData["Title"] = !id.HasValue ? "Criar Tópico" : "Editar Tópico";
            ViewData["Description"] = "Crie e altere os tópicos mostrados nos aplicativos";

            var user = GetUserInfo();
            
            if (id.HasValue)
            {
                using (var proxy = await _api.GetAppTopic(user.Token, id.Value))
                {
                    switch (proxy.ResponseMessage.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return View(proxy.GetContent());
                        case HttpStatusCode.Unauthorized:
                            await Logout();
                            return Redirect("home/index");
                        default:
                            return Redirect("home/error");
                    }
                }
            }
            
            return
                View(new AppTopicProxy());
        }
        
        // GET
        [HttpPost]
        public async Task<IActionResult> Create(AppTopicProxy payload)
        {
            var user = GetUserInfo();
            
            using (var proxy = await _api.CreateAppTopic(user.Token, payload))
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Redirect("index");
                    case HttpStatusCode.Unauthorized:
                        await Logout();
                        return Redirect("home/index");
                    default:
                        return Redirect("home/error");
                }
            }
        }
        
        // GET
        public IActionResult Index()
        {
            ViewData["Title"] = "Tópicos";
            ViewData["Description"] = "Crie e altere os tópicos mostrados nos aplicativos";
            
            return
            View();
        }
        
        public async Task<IActionResult> List()
        {
            var user = GetUserInfo();

            var payload = GetDataTablePayload();

            using (var proxy = await _api.GetAppTopics(user.Token, payload))
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Json(proxy.GetContent());
                    case HttpStatusCode.Unauthorized:
                        await Logout();
                        return Redirect("home/index");
                    default:
                        return Redirect("home/error");
                }
            }
        }
    }
}