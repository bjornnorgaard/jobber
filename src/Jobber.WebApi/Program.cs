using Jobber.Hang;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
HangConfigurator.AddHang(builder);

var app = builder.Build();
app.MapOpenApi();
HangConfigurator.UseHang(app);

app.Run();