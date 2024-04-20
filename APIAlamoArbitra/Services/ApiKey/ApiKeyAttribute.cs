using Microsoft.AspNetCore.Mvc;

namespace APIAlamoArbitra.Services.ApiKey
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}
