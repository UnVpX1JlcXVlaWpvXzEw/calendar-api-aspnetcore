namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Middleware
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
