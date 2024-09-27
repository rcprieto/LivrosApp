using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace API;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
	{
		// services.AddDbContext<DataContext>(opt =>
		// {
		// 	opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
		// });


		services.AddDbContext<DataContext>(options => options
			.UseMySql(config.GetConnectionString("DbConnectionString"),
			new MySqlServerVersion(new Version(8, 0, 19)),
			b => b.SchemaBehavior(MySqlSchemaBehavior.Translate, (schemaName, objectName) => objectName)
			)
			.EnableSensitiveDataLogging()
			.EnableDetailedErrors());

		services.AddCors();
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<ITokenService, TokenService>();
		services.AddScoped<IPhotoService, PhotoService>();
		services.AddScoped<IEmailService, EmailService>();
		services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

		return services;
	}

}
