using System;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUserRole : IdentityUserRole<string>
{
	public required AppUser User { get; set; }
	public required AppRole Role { get; set; }

}
