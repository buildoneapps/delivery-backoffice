using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace delivery.backoffice.Utils
{
    public class AuthMidleware
    {
        private readonly RequestDelegate _next;
        public AuthMidleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext httpContext)
        {
            return _next(httpContext);
        }
    }
}