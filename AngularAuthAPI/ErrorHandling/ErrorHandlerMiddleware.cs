using Newtonsoft.Json;
using System.Net;

namespace AuthProjectAPI.ErrorHandling
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;


        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle the exception and return an appropriate JSON response
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new
                {
                    Message = "Some internal error occured",
                    Error = ex.Message
                    // You can include additional error details such as StackTrace if needed
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
