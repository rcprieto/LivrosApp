using System.ComponentModel.DataAnnotations;

namespace API;

public class RegisterDto
{
	[Required]
	public required string UserName { get; set; }

	public required string Email { get; set; }

	public List<string>? Roles { get; set; }

	[Required]
	[StringLength(8, MinimumLength = 4)]
	public required string Password { get; set; }
}
