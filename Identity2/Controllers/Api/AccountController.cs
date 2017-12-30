using Identity2.App_Start;
using System.Web.Http;

namespace Identity2.Controllers.Api
{
	[Authorize]
	public class AccountController : ApiController
	{
		private readonly ApplicationUserManager _applicationUserManager;

		public AccountController(ApplicationUserManager applicationUserManager)
		{
			_applicationUserManager = applicationUserManager;
		}
    }
}
