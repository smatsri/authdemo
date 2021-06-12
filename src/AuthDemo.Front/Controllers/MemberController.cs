using AuthDemo.Front.Services;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Umbraco.Web.WebApi;

namespace AuthDemo.Front.Controllers
{
	public class MemberController : UmbracoApiController
	{
		private readonly IJwtAuthenticationManager jwtAuthenticationManager;

		public MemberController(IJwtAuthenticationManager jwtAuthenticationManager)
		{
			this.jwtAuthenticationManager = jwtAuthenticationManager;
		}
		[HttpGet]
		public IHttpActionResult IsAuthenticated()
		{
			return Ok(new { res = false });
		}

		[HttpGet]
		public IHttpActionResult Login()
		{
			var username = "tester";
			var password = "Aa123456789";


			var success = Membership.ValidateUser(username, password);
			//var success = Members.GetByUsername(username);

			if (success)
			{
				var claims = new List<Claim> {
					new Claim(ClaimTypes.Name, username),
					new Claim(ClaimTypes.NameIdentifier, username)
				};

				var token = jwtAuthenticationManager.CreateToken(claims);
				var authCookie = new HttpCookie("Auth", token);
				HttpContext.Current.Response.SetCookie(authCookie);

				return Ok("you are logged in");
			}


			return Ok(new { res = false });
		}
	}

	
}