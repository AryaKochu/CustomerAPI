using System;
using System.Diagnostics.CodeAnalysis;


namespace CustomerAPI.Swagger
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerHeaderAttribute : Attribute
    {
        public string Name { get; }
        public string Type { get; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public string DefaultValue { get; }
        public SwaggerHeaderAttribute(string name, string type)
        {
            Name = name;
            Type = type;
            Description = string.Empty;
            Required = false;
            DefaultValue = "Bearer ";
        }
    }
}