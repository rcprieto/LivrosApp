using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API;

public static class IdentityServiceExtensions
{
	public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
	{

		services.AddIdentityCore<AppUser>(opt =>
		{
			opt.Password.RequireNonAlphanumeric = false;
			opt.User.AllowedUserNameCharacters = "abcçdefgğhijklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXY0123456789-._@+/ ";
		})
		.AddRoles<AppRole>()
		.AddRoleManager<RoleManager<AppRole>>()
		.AddEntityFrameworkStores<DataContext>();


		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(o =>
		{
			o.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"] ?? "")),
				ValidateIssuer = false,
				ValidateAudience = false,

			};

		});

		services.AddAuthorization(opt =>
		{
			opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
			opt.AddPolicy("UserRole", policy => policy.RequireRole("Admin", "User"));
		});

		return services;
	}
}
