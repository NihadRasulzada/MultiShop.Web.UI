using MultiShop.Web.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationSchemes();

builder.AddServices();

builder.RegisterHttpClients();

var app = builder.Build();

// Middleware'leri kullan
app.UseMiddlewares();

app.Run();