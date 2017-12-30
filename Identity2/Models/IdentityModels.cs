using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity2.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Username { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
		{
			var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
			return userIdentity;
		}
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) {}
		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}