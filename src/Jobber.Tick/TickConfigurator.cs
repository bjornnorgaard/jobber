using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DbContextFactory;
using TickerQ.EntityFrameworkCore.DependencyInjection;

namespace Jobber.Tick;

public static class TickConfigurator
{
    public static void AddTick(WebApplicationBuilder builder)
    {
        var cs = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddTickerQ(tickerOptions =>
        {
            tickerOptions.AddDashboard();
            tickerOptions.AddOperationalStore(storeOptions =>
            {
                storeOptions.SetDbContextPoolSize(34);
                storeOptions.UseTickerQDbContext<TickerQDbContext>(contextOptions =>
                {
                    contextOptions.UseNpgsql(cs);
                });
            });
        });
    }

    public static void UseTick(WebApplication app)
    {
        app.UseTickerQ();
    }
}