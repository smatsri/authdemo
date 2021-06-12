using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace AuthDemo.Front.App_Start
{
	[RuntimeLevel(MinLevel = RuntimeLevel.Run)]
	public class WebAPi : ComponentComposer<MapHttpRoutesComponent>
	{
	}

	public class MapHttpRoutesComponent : IComponent
	{
		public MapHttpRoutesComponent()
		{
			var config = GlobalConfiguration.Configuration;

			config.MapHttpAttributeRoutes();
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			config.Formatters.JsonFormatter.SerializerSettings
				.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.Formatters.JsonFormatter
				.UseDataContractJsonSerializer = false;
		}
		public void Initialize()
		{

		}

		public void Terminate()
		{

		}
	}
}
