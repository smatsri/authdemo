//using AuthDemo.MyService.Auth;
//using AuthDemo.MyService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthentication;

namespace AuthDemo.MyService
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var tokenSeacret = Configuration["Jwt:Seacret"];
			var authCookieName = Configuration["Jwt:CookieName"];

			services.AddJwtAuthentication(new JwtTokenOptions(tokenSeacret, authCookieName));
			//var jwtTokenService = new JwtTokenService(new JwtTokenOptions(tokenSeacret));

			//services.AddSingleton(jwtTokenService);

			//services.AddAuthentication("Jwt")
			//	.AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler>("Jwt", null);
			services.AddAuthorization();
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthDemo.MyService", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthDemo.MyService v1"));
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
