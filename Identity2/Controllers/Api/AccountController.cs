using Identity2.App_Start;
using Identity2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Identity2.Controllers.Api
{
	[Authorize]
	public class AccountController : ApiController
	{
		private readonly ApplicationUserManager _applicationUserManager;
		private ApplicationUserManager _userManager;

		public AccountController(ApplicationUserManager applicationUserManager)
		{
			_applicationUserManager = applicationUserManager;
		}

		[Route("Logout")]
		public IHttpActionResult Logout()
		{
			Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
			return Ok();
		}

		[Route("UserInfo")]
		public async Task<IHttpActionResult> GetUserInfoAsync()
		{
			var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

			if (user == null)
			{
				return null;
			}

			return Ok(new UserInfoModel
			{
				Username = user.Username,
				Email = user.Email
			});
		}

		private IAuthenticationManager Authentication
		{
			get
			{
				return Request.GetOwinContext().Authentication;
			}
		}

		private ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			set
			{
				_userManager = value;
			}
		}
	}
}
