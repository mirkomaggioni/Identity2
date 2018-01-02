using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Identity2.App_Start;
using Identity2.Models;

[assembly: OwinStartup(typeof(Identity2.Startup))]

namespace Identity2
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.CreatePerOwinContext(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.UseCookieAuthentication(new CookieAuthenticationOptions());
		}
	}
}
