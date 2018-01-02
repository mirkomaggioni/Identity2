using Identity2.App_Start;
using Identity2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
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

		[Route("ChangePassword")]
		public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		[Route("SetPassword")]
		public async Task<IHttpActionResult> SetPassword(SetPasswordModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(RegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = new ApplicationUser() { Username = model.Username, Email = model.Email };

			IdentityResult result = await UserManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && _userManager != null)
			{
				_userManager.Dispose();
				_userManager = null;
			}

			base.Dispose(disposing);
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

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded && result.Errors != null)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error);
				}

				if (ModelState.IsValid)
				{
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}
