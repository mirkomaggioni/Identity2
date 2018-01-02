using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity2.Web.Models
{
	public class ApplicationUser : IdentityUser {}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) {}
	}
}