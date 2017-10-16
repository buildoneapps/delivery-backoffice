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
        
        #region SETTING
        [Get("setting")]
        Task<Response<DataTableProxy<SettingProxy>>> GetSettings([Header("Authorization")] string authorization, [Body]DataTablePayload payload);
        
        [Get("setting/edit")]
        Task<Response<SettingProxy>> GetSetting([Header("Authorization")] string authorization, [Query]Guid id);
        
        [Put("setting")]
        Task<Response<SettingProxy>> SetSetting([Header("Authorization")] string authorization, [Body]SettingProxy payload);   

        #endregion
        
        #region APP TOPIC
        
        [Get("topic")]
        Task<Response<DataTableProxy<AppTopicProxy>>> GetAppTopics([Header("Authorization")] string authorization, [Body]DataTablePayload payload);
         
        [Post("topic")]
        Task<Response<AppTopicProxy>> CreateAppTopic([Header("Authorization")] string authorization, [Body]AppTopicProxy payload);
        
        [Put("topic/edit")]
        Task<Response<AppTopicProxy>> UpdateAppTopic([Header("Authorization")] string authorization, [Body]AppTopicProxy payload);
        
        [Get("topic/edit")]
        Task<Response<AppTopicProxy>> GetAppTopic([Header("Authorization")] string authorization, [Query]Guid id);
        
        #endregion
        
        #region DRIVER
        
        [Get("driver")]
        [AllowAnyStatusCode]
        Task<Response<DataTableProxy<DriverProxy>>> GetDrivers([Header("Authorization")] string authorization, [Body]DataTablePayload payload);
        
        [Get("driver/edit")]
        Task<Response<DriverProxy>> GetDriver([Header("Authorization")] string authorization, [Query]Guid id);
        
        [Put("driver")]
        Task<Response<DriverProxy>> SetDriver([Header("Authorization")] string authorization,
            [Query]Guid id, [Query] bool isBlocked, [Query] int reason, [Query] int level);  
        
        #endregion
        
        #region DASH
        
        [Get("dash")]
        Task<Response<DashboardProxy>> GetDashInfo([Header("Authorization")] string authorization, [Query]int type);
        
        #endregion
        
    }
}