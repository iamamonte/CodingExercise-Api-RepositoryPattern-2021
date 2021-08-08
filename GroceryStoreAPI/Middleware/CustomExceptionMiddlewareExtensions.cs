using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Middleware
{

    public static  class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomLogging(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
