using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Jobber.Hang;

public static class HangConfigurator
{
    public static void AddHang(WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(configuration =>
        {
            var cs = builder.Configuration.GetConnectionString("DefaultConnection");

            configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(o => o.UseNpgsqlConnection(cs));
        });

        builder.Services.AddHangfireServer();
    }

    public static void UseHang(WebApplication app)
    {
        app.MapHangfireDashboard();
    }
}