using Hangfire;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI;
using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Services.NotificationDeliveryJob;
using Serilog;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = CreateBuilder(args);

Startup startup = new(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<NotificationDeliveryJob>(
    "deliver-notifications",
    job => job.RunAsync(CancellationToken.None),
    "*/1 * * * *");

await app.RunAsync();

static WebApplicationBuilder CreateBuilder(string[] args)
{
    var webApplicationOptions = new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = $"{Directory.GetCurrentDirectory()}/Configuration"
    };

    var builder = WebApplication.CreateBuilder(webApplicationOptions);

    builder.Host.ConfigureAppConfiguration((builderContext, config) =>
    {
        var hostingEnvironment = builderContext.HostingEnvironment;
        config.SetBasePath(hostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddConfigServer(hostingEnvironment.EnvironmentName)
            .AddEnvironmentVariables();
    })
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
    });

    return builder;
}