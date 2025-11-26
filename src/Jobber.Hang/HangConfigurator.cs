using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobber.Hang;

public static class HangConfigurator
{
    public static void AddHang(WebApplicationBuilder builder)
    {
        var cs = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddHangfire(conf => conf
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(o => o.UseNpgsqlConnection(cs)));

        builder.Services.AddHangfireServer();
    }

    public static void UseHang(WebApplication app)
    {
        app.MapHangfireDashboard();

        var manager = app.Services.GetRequiredService<IRecurringJobManager>();
        manager.AddOrUpdate<HangWorker>("HangWorker", w => w.Run(), Cron.Minutely);
    }
}