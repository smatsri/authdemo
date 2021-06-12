using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuthentication
{
	public static class StartupExt
	{
		public static void AddJwtAuthentication(this IServiceCollection services, JwtTokenOptions options)
		{
			
			services.AddSingleton(options);
			services.AddSingleton<JwtTokenService>();
			services.AddAuthentication("Jwt")
				.AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler>("Jwt", null);
		}
	}
}
