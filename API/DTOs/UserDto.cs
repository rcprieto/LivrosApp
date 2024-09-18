namespace API;

public class UserDto
{
	public string? UserName { get; set; }
	public string? Email { get; set; }
	public string? Token { get; set; }
	public string? Id { get; set; }
	public List<string>? Roles { get; set; }

}
