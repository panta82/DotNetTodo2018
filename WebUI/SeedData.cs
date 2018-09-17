using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebUI {
	public class SeedData {
		private const string AdminEmail = "admin@pantas.net";
		private const string AdminPassword = "qwe123QWE!@#";
		
		public static async Task InitializeAsync(IServiceProvider services) {
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
			await EnsureRolesAsync(roleManager);

			var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
			await EnsureTestAdminAsync(userManager);
		}

		private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager) {
			if (!await roleManager.RoleExistsAsync(Constants.AdministratorRole)) {
				await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
			}
		}
		
		private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager) {
			if (await userManager.Users.AnyAsync(x => x.Email == AdminEmail)) {
				return;
			}

			var admin = new IdentityUser {
				UserName = AdminEmail,
				Email = AdminEmail
			};

			await userManager.CreateAsync(admin, AdminPassword);
			await userManager.AddToRoleAsync(admin, Constants.AdministratorRole);
		}
	}
}