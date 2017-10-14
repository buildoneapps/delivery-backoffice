﻿using System;
using System.Net;
using System.Net.Http;
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
            ViewData["Title"] = "Configurações";
            ViewData["Description"] = "Altere as configurações de toda a plataforma";
            
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewData["Title"] = "Editar configuração";
            ViewData["Description"] = "Alteração reflete em toda a plataforma";
            var user = GetUserInfo();
            
            using (var proxy = await _api.GetSetting(user.Token, id))
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return View(proxy.GetContent());
                    case HttpStatusCode.Unauthorized:
                        await Logout();
                        return Redirect("Home/Index");
                    default:
                        return Redirect("Error");
                }
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(SettingProxy model)
        {
            var user = GetUserInfo();
            
            using (var proxy = await _api.SetSetting(user.Token, model))
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