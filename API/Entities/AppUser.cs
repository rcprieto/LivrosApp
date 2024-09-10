using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser : IdentityUser
{
	public ICollection<AppUserRole>? UserRoles { get; set; }

}

