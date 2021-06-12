namespace JwtAuthentication
{
	public class JwtTokenOptions
	{
		public JwtTokenOptions(string secret, string cookieName = "Auth")
		{
			Secret = secret;
			CookieName = cookieName;
		}

		public string Secret { get; }
		public string CookieName { get; }
	}
}
