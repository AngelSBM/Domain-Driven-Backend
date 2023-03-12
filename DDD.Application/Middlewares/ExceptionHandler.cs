using DDD.Utilities.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace DDD.Application.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(NotFoundException ex)
            {
                await HandleException(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            var code = statusCode;
            var errors = new List<string>(){ exception.Message };       
            var result = JsonConvert.SerializeObject(new { errors = errors });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
