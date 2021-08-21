using System;
using System.Collections.Generic;
using CustomerAPI.Bff.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using static CustomerAPI.Swagger.EnvironmentVariableNames;


namespace CustomerAPI.Swagger
{
    internal class SwaggerConfiguration
    {
        private const string DefaultScheme = "https";
        private const string DefaultBasePath = "/";

        private static SwaggerConfigurationSettings swaggerOptions;

        private static string ApiName => swaggerOptions.ApiName;
        private static string ApiVersion => swaggerOptions.ApiVersion;
        private static string ApiDescription => swaggerOptions.ApiDescription;

        public static void WithSwaggerOptions(SwaggerOptions options)
        {
            var scheme = Environment.GetEnvironmentVariable(SWAGGER_SCHEME)?.ToLowerInvariant() ??
                         DefaultScheme;
            options.SerializeAsV2 = true;

            options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                string basePath = Environment.GetEnvironmentVariable(SWAGGER_API_BASE_PATH) ?? DefaultBasePath;
                string apiHost = Environment.GetEnvironmentVariable(SWAGGER_API_HOST);
                string host = string.IsNullOrEmpty(apiHost) ?
                    // For APIM V2 Publish
                    httpReq.Host.Value :
                    // For APIM V1 Publish
                    apiHost;

                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new OpenApiServer
                    {
                        Url = $"{scheme}://{host}{basePath}"
                    }
                };
            });
        }

        public static void WithSwaggerGenServiceOptions(SwaggerGenOptions options)
        {
            options.DescribeAllParametersInCamelCase();

            options.SwaggerDoc(
                ApiVersion,
                new OpenApiInfo
                {
                    Title = ApiName,
                    Version = ApiVersion,
                    Description = ApiDescription
                });

            options.OperationFilter<SwaggerHeaderFilter>();
            options.UseInlineDefinitionsForEnums();
            options.EnableAnnotations();
        }

        public static void WithSwaggerUiOptions(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiName);
        }

        public static void ConfigureSwaggerWith(AppSettings configuration)
        {
            swaggerOptions = configuration;
        }
    }
}
