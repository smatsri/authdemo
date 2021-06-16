using Microsoft.Owin.Security;

namespace JwtAuthentication4
{
	public class JwtOptions : AuthenticationOptions
	{
		public JwtOptions(string authenticationType, string seacret, string cookieName) : base(authenticationType)
		{
			Secret = seacret;
			CookieName = cookieName;
		}

		public string Secret { get; }
		public string CookieName { get; }
	}
}
