using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Identity2.Web.App_Start;
using Identity2.Web.Models;

[assembly: OwinStartup(typeof(Identity2.Web.Startup))]

namespace Identity2.Web
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
