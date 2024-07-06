using System.Net;

namespace NZWalksAPI.MiddleWare
{
    public class ExceptionHandlerMiddleware
    {

        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

       public async Task InvokeAsync(HttpContext httpcontext)

        {
            try
            {
                await next(httpcontext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //log this exception 
                logger.LogError(ex,$"{errorId} : {ex.Message}");


                //return custom error response 
                httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpcontext.Response.ContentType = "applicaton.json";
                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! we are loking int resolbing these ",
                };
                await httpcontext.Response.WriteAsJsonAsync(error);

            }
        }


    }

}

