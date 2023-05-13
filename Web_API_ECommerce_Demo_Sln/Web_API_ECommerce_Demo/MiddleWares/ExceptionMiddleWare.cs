using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Web_API_ECommerce_Demo.Errors;

namespace Web_API_ECommerce_Demo.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                // log Exception in the database

                context.Response.ContentType = "appliction/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                
               
                var response = _env.IsDevelopment()?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
