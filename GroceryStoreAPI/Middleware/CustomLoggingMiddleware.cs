using GroceryStore.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerAdapter _globalLogger;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerAdapter logger)
        {
            _next = next;
            _globalLogger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                string genericMessage = "An unhandled exception has occurred.";
                _globalLogger.Error(ex, genericMessage, httpContext);
                var response = httpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = 500;
                var result = JsonSerializer.Serialize(new { @Message = genericMessage });
                await response.WriteAsync(result);
            }
        }
    }
}
