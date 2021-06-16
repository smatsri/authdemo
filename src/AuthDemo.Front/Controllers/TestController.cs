using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace AuthDemo.Front.Controllers
{
	public class TestController : SurfaceController
	{

		public JsonResult T1()
		{
			var res = new { isAuthenticated = User.Identity.IsAuthenticated };
			return Json(res, System.Web.Mvc.JsonRequestBehavior.AllowGet);
		}
	}
}