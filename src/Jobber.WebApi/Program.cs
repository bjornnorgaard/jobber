using Jobber.Hang;
using Jobber.Tick;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
HangConfigurator.AddHang(builder);
TickConfigurator.AddTick(builder);

var app = builder.Build();
app.MapOpenApi();
HangConfigurator.UseHang(app);
TickConfigurator.UseTick(app);

app.Run();