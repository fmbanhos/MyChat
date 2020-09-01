using Microsoft.OpenApi.Models;

namespace MyChatAPI.Infra.Helpers
{
	public static class SwaggerHelper
	{
		public const string Title = "MyChatAPI";
		public const string Version = "v1";
		public const string EndPointUrl = "/swagger/v1/swagger.json";

		public static OpenApiInfo GetOpenApiInfo()
		{
			OpenApiInfo result = new OpenApiInfo
			{
				Title = Title,
				Version = Version,
				Description = "Simple chat API"
			};

			return result;
		}

		//public static string GetXmlComments()
		//{
		//	// Set the comments path for the Swagger JSON and UI.
		//	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
		//	var result = Path.Combine(AppContext.BaseDirectory, xmlFile);
		//	return result;
		//}
	}
}
