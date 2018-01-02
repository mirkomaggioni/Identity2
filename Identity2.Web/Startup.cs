using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Identity2.Web.App_Start;
using Identity2.Web.Models;
using Autofac;
using System.Reflection;
using Autofac.Integration.WebApi;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(Identity2.Web.Startup))]

namespace Identity2.Web
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions());

			var containerBuilder = new ContainerBuilder();

			containerBuilder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
			containerBuilder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
			containerBuilder.Register(b => new UserStore<ApplicationUser>(b.Resolve<ApplicationDbContext>())).AsImplementedInterfaces().InstancePerRequest();
			containerBuilder.Register(b => new IdentityFactoryOptions<ApplicationUserManager>() {
				DataProtectionProvider = new DpapiDataProtectionProvider("Identity2 Application")
			}).AsSelf().InstancePerRequest();

			containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			var container = containerBuilder.Build();

			var config = new HttpConfiguration
			{
				DependencyResolver = new AutofacWebApiDependencyResolver(container)
			};

			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			app.UseWebApi(config);
		}
	}
}
