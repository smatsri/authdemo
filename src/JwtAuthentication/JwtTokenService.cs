using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication
{
	internal class JwtTokenService
	{
		private readonly JwtTokenOptions options;
		private readonly TokenValidationParameters tokenValidationParameters;
		private readonly JwtSecurityTokenHandler tokenHandler;

		public JwtTokenService(JwtTokenOptions options)
		{
			this.options = options;
			var key = Encoding.ASCII.GetBytes(options.Secret);
			tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				// set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
				ClockSkew = TimeSpan.Zero
			};
			tokenHandler = new JwtSecurityTokenHandler();
		}

		public bool Validate(string token, out IEnumerable<Claim> claims)
		{

			try
			{
				tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
				var jwtToken = (JwtSecurityToken)validatedToken;
				claims = jwtToken.Claims;
				return true;
			}
			catch//(Exception _)
			{
				claims = null;
				return false;
			}

		}
	}
}
