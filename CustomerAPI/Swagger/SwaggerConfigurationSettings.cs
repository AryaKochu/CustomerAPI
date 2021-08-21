using CustomerAPI.Bff.Models;
using System;

namespace CustomerAPI.Swagger
{
    public class SwaggerConfigurationSettings
    {
        private const string DefaultEnvironmentName = "Unknown Environment";
        public string ApiName { get; set; }
        public string ApiVersion { get; set; }
        public string ApiDescription { get; set; }

        public static implicit operator SwaggerConfigurationSettings(AppSettings settings)
        {
            SwaggerConfigurationSettings options = new SwaggerConfigurationSettings();
            options.ApiDescription = settings.AppConfiguration.ApiDescription;
            options.ApiVersion = settings.AppConfiguration.ApiVersion;
            options.ApiName = $"Customer API - {(settings.AppConfiguration.Environment ?? DefaultEnvironmentName).Trim()}";
            return options;
        }
    }
}
