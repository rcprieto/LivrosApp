using System.ComponentModel.DataAnnotations;

namespace API;

public class LoginDto
{
	[Required]
	public required string Email { get; set; }

	[Required]
	public required string Password { get; set; }
}

public class ResetDto
{
	[Required]
	public required string Email { get; set; }

}
