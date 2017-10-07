using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using delivery.backoffice.API.Model.Proxy;
using delivery.backoffice.Model;
using RestEase;

namespace delivery.backoffice.API
{
    public interface IDeliveryAPI
    {
        [Post("auth/login")]
        [Header("Content-Type", "application/x-www-form-urlencoded")]
        Task<AuthProxy> Authenticate([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data, [Query] bool isAdmin);

        [Get("user/me")]
        Task<UserProxy> GetUser([Header("Authorization")] string authorization);
        
        [Get("setting")]
        Task<DataTableProxy<SettingProxy>> GetSettings([Header("Authorization")] string authorization, [Body]DataTablePayload payload);
        
        
        
    }
}