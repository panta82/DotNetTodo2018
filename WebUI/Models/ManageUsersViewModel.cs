using Microsoft.AspNetCore.Identity;

namespace WebUI.Models {
	public class ManageUsersViewModel {
		public IdentityUser[] Everyone { get; set; }
		public IdentityUser[] Administrators { get; set; }
	}
}