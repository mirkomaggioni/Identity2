using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Identity2.Web.App_Start;
using Identity2.Web.Models;
using Autofac;
using System.Reflection;
using Autofac.Integration.WebApi;
using System.Web.Http;

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

			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterType<ApplicationUserManager>();

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
