using Microsoft.Owin;
using Owin;
using Umbraco.Web;
using JwtAuthentication4;

[assembly: OwinStartup("Startup", typeof(AuthDemo.Front.App_Start.Startup))]
namespace AuthDemo.Front.App_Start
{
	public class Startup : UmbracoDefaultOwinStartup
	{
		public override void Configuration(IAppBuilder app)
		{
			base.Configuration(app);
			app.UseJwtAuthentication(Config.JwtOptions);
		}
	}
}