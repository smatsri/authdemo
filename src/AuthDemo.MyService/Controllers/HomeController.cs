using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthDemo.MyService.Controllers
{
	[Route("api/home")]
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Get()
		{
			var identity = (ClaimsIdentity)User.Identity;
			var claims = identity.Claims.Select(a => new { a.Type, a.Value });
			var name = identity.Name;

			var res = new
			{
				message = $"welcome home, {User.Identity.Name}",
				claims,
				name,
				nameType = identity.NameClaimType
			};
			return Ok(res);
		}
	}
}
