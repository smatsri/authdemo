using AuthDemo.Front.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

			if (success)
			{
				///TODO: use model builder to work with strongly typed Member type 
				///      get member from umbraco
				///      and add first,last name

				var claims = new List<Claim> {
					new Claim(ClaimTypes.Name, username)
				};

				var token = jwtAuthenticationManager.CreateToken(claims);
				var authCookie = new HttpCookie("Auth", token)
				{
					HttpOnly = true,
					Expires = DateTime.Now.AddDays(1),
					SameSite = SameSiteMode.Lax
				};

				HttpContext.Current.Response.SetCookie(authCookie);

				var res = new
				{
					claims = claims.Select(a=> new { a.Value, a.Type })
				};

				return Ok(res);
			}

			return Ok(new { res = false });
		}
	}
}