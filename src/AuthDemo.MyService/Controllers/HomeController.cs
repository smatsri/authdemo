using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

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
				claims,
				name
			};
			return Ok(res);
		}
	}
}
