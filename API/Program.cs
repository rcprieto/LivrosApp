using System.Globalization;
using API;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;

//ng new client para criar o site em angular, rodar na raiz fora de API
//mkdir ssl
//cd ssl
//mkcert localhost
//No angular.json na tag serve:{}
//"options": {
// "ssl": true,
// "sslCert": "ssl/localhost.pem",
// "sslKey": "ssl/localhost-key.pem"
// },



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
	var supportedCultures = new List<CultureInfo>
	{
		new CultureInfo("pt-BR")
	};
	opts.DefaultRequestCulture = new RequestCulture("pt-BR");
	opts.SupportedCultures = supportedCultures;
	opts.SupportedUICultures = supportedCultures;
	var provider = new RouteDataRequestCultureProvider { RouteDataStringKey = "lang", UIRouteDataStringKey = "lang", Options = opts };
	opts.RequestCultureProviders = new[] { provider };
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supportedCultures = new[]
	{
					new CultureInfo("pt-BR")
				};
	var supportedUICultures = new[]
	{
					new CultureInfo("en"),
					new CultureInfo("es"),
					new CultureInfo("pt-BR")
				};
	options.DefaultRequestCulture = new RequestCulture("pt-BR");
	options.SetDefaultCulture("pt-BR");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedUICultures;
});


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.UseCors(x =>
	x.AllowAnyHeader()
	.AllowAnyMethod()
	.AllowCredentials() //SignaIR precisa
	.WithOrigins(["https://localhost:4200", "http://localhost:4200"]));

app.UseAuthentication(); //Vc tem um token valido
app.UseAuthorization(); //Vc tem um token mas o que vc pode fazer

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
	// var context = services.GetRequiredService<DataContext>();
	// var userManager = services.GetRequiredService<UserManager<AppUser>>();
	// var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
	// await context.Database.MigrateAsync();
	//await context.Database.ExecuteSqlRawAsync("DELETE FROM [Livros]"); //Para sqllite
	//await context.Database.ExecuteSqlRawAsync("TRUNCATE FROM TABLE [Connections]");
	//await Seed.SeedUser(userManager, roleManager, context);

}
catch (Exception ex)
{
	var logger = services.GetService<ILogger<Program>>();
	if (logger != null)
		logger.LogError(ex, "Erro durante o migration");

}

app.Run();
