
using System.Text.Json;

namespace OnlineShopping.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        // TODO: User Logger here to log the exception at catch statement
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env) 
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment() ?
                    new RestDto<int?>(StatusCodes.Status500InternalServerError,
                    null,
                    ex.Message,
                    new List<string> { ex.StackTrace?.ToString() ?? "Stacktrace is null"}) :
                    new RestDto<int?>(StatusCodes.Status500InternalServerError,
                    null,
                    "Something went wrong");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
