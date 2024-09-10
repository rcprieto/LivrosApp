using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
	public DataContext(DbContextOptions options) : base(options)
	{
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


	}
}
