using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthentication4
{
	internal class Handler : AuthenticationHandler<JwtOptions>
	{
		private readonly JwtOptions options;

		public Handler(JwtOptions options)
		{
			this.options = options;
		}

		protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
		{
			var ticket = AuthenticateCore();
			return Task.FromResult(ticket);
		}

		AuthenticationTicket AuthenticateCore()
		{
			var token = Request.Cookies[options.CookieName];
			if (string.IsNullOrEmpty(token))
				return null;

			try
			{
				if (!IsValid(token, out IEnumerable<Claim> claims))
					return null;

				var identity = new ClaimsIdentity(options.AuthenticationType);
				identity.AddClaims(claims);
				var props = new AuthenticationProperties();
				return new AuthenticationTicket(identity, props);
			}
			catch
			{
				return null;
			}

		}


		bool IsValid(string token, out IEnumerable<Claim> claims)
		{
			var handler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(options.Secret);
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				// set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
				ClockSkew = TimeSpan.Zero
			};

			try
			{
				handler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
				var jwtToken = (JwtSecurityToken)validatedToken;
				claims = jwtToken.Claims;
				return true;
			}
			catch (Exception)
			{
				claims = null;
				return false;
			}



		}
	}
}
