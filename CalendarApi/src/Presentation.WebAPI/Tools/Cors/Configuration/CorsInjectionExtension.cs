using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Cors.Common;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Cors.Configuration
{
    public static class CorsInjectionExtension
    {
        public static void AddCors(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
                options.AddPolicy(
                    CorsConfigConstantCollection
                    .GetCorsOriginCollectionName(configuration), builder =>
                {
                    builder.WithOrigins(CorsConfigConstantCollection
                        .GetCorsAllowedOrigins(configuration))
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void UseCors(
            this IApplicationBuilder app,
            IConfiguration configuration)
        {
            app.UseCors(CorsConfigConstantCollection
                .GetCorsOriginCollectionName(configuration));

            app.UseHttpsRedirection();
        }
    }
}
