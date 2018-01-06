using Microsoft.AspNet.Identity.EntityFramework;

namespace Identity2.Web.Models
{
	public class ApplicationUser : IdentityUser {}

	// ReSharper disable once ClassNeverInstantiated.Global
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) {}
	}
}