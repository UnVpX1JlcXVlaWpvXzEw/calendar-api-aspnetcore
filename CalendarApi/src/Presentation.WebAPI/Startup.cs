using HustleAddiction.Platform.CalendarApi.Infrastructure;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Configuration;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Cors.Configuration;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Middleware;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Jwt.Common;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CalendarAPIDbContext>(options =>
                options.UseLazyLoadingProxies(),
                ServiceLifetime.Scoped);

            services.Configure<JwtOptions>(Configuration.GetSection(JwtOptions.SectionName));

            var jwtOptions = Configuration
                .GetSection(JwtOptions.SectionName)
                .Get<JwtOptions>();

            var key = Encoding.UTF8.GetBytes(jwtOptions?.SecurityKey!);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions?.ValidIssuer,
                        ValidAudience = jwtOptions?.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        RequireExpirationTime = true
                    };
                });

            services.AddAuthorization();

            Console.WriteLine("JWT Key: " + jwtOptions?.SecurityKey);

            services.RegisterPresentationServices(Configuration);

            services.AddCors(Configuration);

            services.AddControllers();
            services.AddSwagger();
        }

        public void Configure(WebApplication app)
        {
            MigrateDatabase(app);

            app.UseCors(Configuration);

            app.UseExceptionMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar API V1");
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }

        private static void MigrateDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CalendarAPIDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
        }
    }
}
