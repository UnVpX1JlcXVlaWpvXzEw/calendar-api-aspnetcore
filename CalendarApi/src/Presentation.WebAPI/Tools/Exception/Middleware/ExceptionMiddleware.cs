using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Common;
using Newtonsoft.Json;
using System.Net;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly Dictionary<Type, HttpStatusCode> exceptionCodes;

        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;

            this.exceptionCodes = new();

            this.MapExceptionsAndCodes();
        }

        public async Task InvokeAsync(
            HttpContext context,
            ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await this.next(context);
            }
            catch (System.Exception exception)
            {
                logger.LogError(exception, $"{this.GetType().Name}.InvokeAsync");

                await this.HandleExceptionAsync(context, exception);
            }
        }

        private HttpStatusCode GetStatusCode(System.Exception exception)
        {
            var type = exception.GetType();

            if (this.exceptionCodes.ContainsKey(type))
            {
                return this.exceptionCodes.GetValueOrDefault(type);
            }

            return HttpStatusCode.InternalServerError;
        }

        private Task HandleExceptionAsync(
            HttpContext context,
            System.Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)this.GetStatusCode(exception);

            var task = context.Response.WriteAsync(
                JsonConvert.SerializeObject(
                    new ErrorMessage
                    {
                        Status = context.Response.StatusCode,
                        Message = exception.Message
                    },
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }));

            return task;
        }

        private void MapExceptionsAndCodes()
        {
            this.exceptionCodes
                .Add(typeof(ArgumentException), HttpStatusCode.BadRequest);
            this.exceptionCodes
                .Add(typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized);
            this.exceptionCodes
                .Add(typeof(KeyNotFoundException), HttpStatusCode.NotFound);
        }
    }
}
