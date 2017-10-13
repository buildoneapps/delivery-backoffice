using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using delivery.backoffice.API;
using delivery.backoffice.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestEase;

namespace delivery.backoffice.Controllers
{
    [Authorize]
    public class SettingController : BaseController
    {
        private IDeliveryAPI _api;

        public SettingController(IDeliveryAPI api)
        {
            _api = api;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Post("list")]
        public async Task<IActionResult> List()
        {
            var user = GetUserInfo();

            var payload = GetDataTablePayload();

            using (var proxy = await _api.GetSettings(user.Token, payload))
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Json(proxy.GetContent());
                    case HttpStatusCode.Unauthorized:
                        await Logout();
                        return Redirect("Home/Index");
                    default:
                        return Redirect("Error");
                }
            }
        }
    }
}