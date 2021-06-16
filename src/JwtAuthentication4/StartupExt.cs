using Owin;

namespace JwtAuthentication4
{

	public static class StartupExt
	{
		public static IAppBuilder UseJwtAuthentication(this IAppBuilder app, JwtOptions options)
		{
			app.Use(typeof(Middleware), options);
			return app;
		}
	}
}
