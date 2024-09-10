﻿using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Seed
{



	public static async Task SeedUser(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
	{
		if (await userManager.Users.AnyAsync()) return;

		// var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
		// var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

		// var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

		var roles = new List<AppRole>{
			new AppRole{Name = "User"},
			new AppRole{Name = "Admin"},

		};

		foreach (var role in roles)
		{
			await roleManager.CreateAsync(role);
		}


		// foreach (var user in users)
		// {
		// 	using var hmac = new HMACSHA512();
		// 	user.UserName = user.UserName.ToLower();
		// 	await userManager.CreateAsync(user, "Pa$$w0rd");
		// 	await userManager.AddToRoleAsync(user, "Member");
		// }

		var admin = new AppUser
		{
			UserName = "admin",
			Email = "rcprieto@gmail.com",
			EmailConfirmed = true

		};
		await userManager.CreateAsync(admin, "Pa$$w0rd");
		await userManager.AddToRolesAsync(admin, ["Admin", "User"]);


	}

}
