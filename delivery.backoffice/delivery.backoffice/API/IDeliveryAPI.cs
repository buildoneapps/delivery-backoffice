using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using delivery.backoffice.API.Model.Proxy;
using delivery.backoffice.Model;
using delivery.backoffice.ViewModel;
using RestEase;

namespace delivery.backoffice.API
{ 
    [AllowAnyStatusCode]
    public interface IDeliveryAPI
    {
        [Post("auth/login")]
        [Header("Content-Type", "application/x-www-form-urlencoded")]
        Task<AuthProxy> Authenticate([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data, [Query] bool isAdmin);

        [Get("user/me")]
        Task<UserProxy> GetUser([Header("Authorization")] string authorization);
        
        [Get("setting")]
        Task<Response<DataTableProxy<SettingProxy>>> GetSettings([Header("Authorization")] string authorization, [Body]DataTablePayload payload);
        
        [Get("setting/edit")]
        Task<Response<SettingProxy>> GetSetting([Header("Authorization")] string authorization, [Query]Guid id);
        
        [Put("setting")]
        Task<Response<SettingProxy>> SetSetting([Header("Authorization")] string authorization, [Body]SettingProxy payload);   

        
        [Get("driver")]
        [AllowAnyStatusCode]
        Task<Response<DataTableProxy<DriverProxy>>> GetDrivers([Header("Authorization")] string authorization, [Body]DataTablePayload payload);
        
        [Get("driver/edit")]
        Task<Response<DriverProxy>> GetDriver([Header("Authorization")] string authorization, [Query]Guid id);
        
        [Get("dash")]
        Task<Response<DashboardProxy>> GetDashInfo([Header("Authorization")] string authorization, [Query]int type);
        
        [Put("driver")]
        Task<Response<DriverProxy>> SetDriver([Header("Authorization")] string authorization,
            [Query]Guid id, [Query] bool isBlocked, [Query] int reason, [Query] int level);  
        
    }
}