using AuthDemo.Front.Services;
using System.Configuration;
using Umbraco.Core.Composing;

namespace AuthDemo.Front.App_Start
{
	public class DI : IUserComposer
	{
		public void Compose(Composition composition)
		{
			var tokenSecret = ConfigurationManager.AppSettings["auth:jwtTokenSecret"];
			var jwtAuthenticationManager = new JwtAuthenticationManager(tokenSecret);

			composition.Register<IJwtAuthenticationManager>((a) => jwtAuthenticationManager);

		}
	}
}