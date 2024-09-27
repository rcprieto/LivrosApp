using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace API.Data;

public class DataContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
	private string _connString;
	public DataContext(DbContextOptions options, IConfiguration config) : base(options)
	{
		this._connString = config.GetConnectionString("DbConnectionString");
		//dotnet ef migrations add InitialCreate -o Data/Migrations
		//dotnet ef migrations add NomeMigracao
		//dotnet ef database update
	}

	public DbSet<Livro> Livros { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<AppUser>()
		.HasMany(c => c.UserRoles)
		.WithOne(c => c.User)
		.HasForeignKey(c => c.UserId)
		.IsRequired();

		builder.Entity<AppRole>()
		.HasMany(c => c.UserRoles)
		.WithOne(c => c.Role)
		.HasForeignKey(c => c.RoleId)
		.IsRequired();

		//Não precisa mais no pomelo se for configurado como no onconfiguring, mas se precisar...
		// builder.Entity<AppUser>().Property(p => p.UserName).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.Id).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.Email).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.ConcurrencyStamp).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.NormalizedEmail).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.NormalizedUserName).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.PasswordHash).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.PhoneNumber).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.SecurityStamp).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.LockoutEnd).HasMaxLength(128);
		// builder.Entity<AppUser>().Property(p => p.PasswordHash).HasMaxLength(128);
		// builder.Entity<AppUserRole>().Property(p => p.UserId).HasMaxLength(128);
		// builder.Entity<AppUserRole>().Property(p => p.RoleId).HasMaxLength(128);
		// builder.Entity<AppRole>().Property(p => p.Id).HasMaxLength(128);
		// builder.Entity<AppRole>().Property(p => p.ConcurrencyStamp).HasMaxLength(128);
		// builder.Entity<AppRole>().Property(p => p.Name).HasMaxLength(128);
		// builder.Entity<AppRole>().Property(p => p.NormalizedName).HasMaxLength(128);
		// builder.Entity<IdentityRole>().Property(p => p.ConcurrencyStamp).HasMaxLength(128);
		// builder.Entity<IdentityRole>().Property(p => p.Name).HasMaxLength(128);
		// builder.Entity<IdentityRole>().Property(p => p.Id).HasMaxLength(128);
		// builder.Entity<IdentityUserClaim<string>>().Property(p => p.Id).HasMaxLength(128);
		// builder.Entity<IdentityUserClaim<string>>().Property(p => p.UserId).HasMaxLength(128);


	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var serviceProvider = new ServiceCollection()
			.AddEntityFrameworkMySql()
			.AddSingleton<ISqlGenerationHelper, CustomMySqlSqlGenerationHelper>()
			.AddScoped(
				s => LoggerFactory.Create(
					b => b
						.AddFilter(level => level >= LogLevel.Information)))
			.BuildServiceProvider();

	}
	private class CustomMySqlSqlGenerationHelper : MySqlSqlGenerationHelper
	{
		public CustomMySqlSqlGenerationHelper(
			RelationalSqlGenerationHelperDependencies dependencies,
			IMySqlOptions options)
			: base(dependencies, options)
		{
		}

		public override string GetSchemaName(string name, string schema)
			=> schema; // <-- this is the first part that is needed to map schemas to databases 
	}
}
