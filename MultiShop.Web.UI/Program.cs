using MultiShop.Web.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationSchemes();

builder.AddServices();

builder.RegisterHttpClients();

var app = builder.Build();

app.UseMiddlewares();

app.Run();