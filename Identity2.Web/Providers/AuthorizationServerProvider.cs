using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.Owin.Security.OAuth;

namespace Identity2.Web.Providers
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
			return Task.CompletedTask;
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			var applicationUserManager = context.OwinContext.GetAutofacLifetimeScope().Resolve<ApplicationUserManager>();
			var user = await applicationUserManager.FindAsync(context.UserName, context.Password);

			if (user == null)
			{
				context.SetError("invalid_grant", "The user name or password is incorrect.");
				return;
			}

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim("sub", context.UserName));
			identity.AddClaim(new Claim("role", "user"));
			context.Validated(identity);
		}
	}
}