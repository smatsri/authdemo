using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace JwtAuthentication
{
	internal class JwtAuthenticationOptions : AuthenticationSchemeOptions
	{
	}

	internal class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthenticationOptions>
	{
		private readonly JwtTokenService jwtTokenService;
		private readonly string cookieName;
		static readonly string[] nameAliases = new string[] { "unique_name", "nameid" };


		public JwtAuthenticationHandler(
			IOptionsMonitor<JwtAuthenticationOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock,
			JwtTokenService jwtTokenService,
			JwtTokenOptions jwtTokenOptions)
			: base(options, logger, encoder, clock)
		{
			this.jwtTokenService = jwtTokenService;
			cookieName = jwtTokenOptions.CookieName;
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			AuthenticateResult res = HandleAuthenticate();
			return Task.FromResult(res);
		}

		private AuthenticateResult HandleAuthenticate()
		{
			if (!Request.Cookies.ContainsKey(cookieName))
				return AuthenticateResult.Fail("Unauthorized");

			string token = Request.Cookies[cookieName];

			if (string.IsNullOrEmpty(token))
			{
				return AuthenticateResult.Fail("Unauthorized");
			}

			try
			{
				return ValidateToken(token);
			}
			catch (Exception ex)
			{
				return AuthenticateResult.Fail(ex.Message);
			}
		}

		AuthenticateResult ValidateToken(string token)
		{
			var valid = jwtTokenService.Validate(token, out IEnumerable<Claim> claims);
			if (!valid)
			{
				return AuthenticateResult.Fail("Unauthorized");
			}

			var identity = new ClaimsIdentity(claims, Scheme.Name);
			FixName(identity);

			var principal = new System.Security.Principal.GenericPrincipal(identity, null);
			var ticket = new AuthenticationTicket(principal, Scheme.Name);
			return AuthenticateResult.Success(ticket);
		}

		static void FixName(ClaimsIdentity identity)
		{
			if (!string.IsNullOrEmpty(identity.Name))
				return;
			
			var name = GetName(identity);
			if (!string.IsNullOrEmpty(name))
			{
				identity.AddClaim(new Claim(ClaimTypes.Name, name));
			}
		}

		static string GetName(ClaimsIdentity identity)
		{
			foreach (var nameAlias in nameAliases)
			{
				var c = identity.FindFirst(nameAlias);
				if (c != null)
					return c.Value;
			}
			return "";
		}

	}
}
