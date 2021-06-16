using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using JwtAuthentication4;

namespace AuthDemo.Front.App_Start
{
	public class Config
	{
		static JwtOptions jwtOptions = null;
		public static JwtOptions JwtOptions
		{
			get
			{
				if (jwtOptions == null)
					jwtOptions = GetJwtOptions();
				return jwtOptions;
			}
		}

		static JwtOptions GetJwtOptions()
		{
			var secret = ConfigurationManager.AppSettings["auth:jwtTokenSecret"];
			var cookieName = ConfigurationManager.AppSettings["auth:CookieName"];
			var schema = ConfigurationManager.AppSettings["auth:Schema"];
			return new JwtOptions(schema, secret, cookieName);
		}

	}
}