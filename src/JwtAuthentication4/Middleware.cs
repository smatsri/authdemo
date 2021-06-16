using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace JwtAuthentication4
{
	internal class Middleware : AuthenticationMiddleware<JwtOptions>
	{
		private readonly JwtOptions options;

		public Middleware(OwinMiddleware next, JwtOptions options) : base(next, options)
		{
			this.options = options;
		}

		protected override AuthenticationHandler<JwtOptions> CreateHandler()
		{

			return new Handler(options);
		}
	}
}
