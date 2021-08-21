using System;

namespace CustomerAPI.Bff.Models
{
    public class AppConfiguration
    {
        public string ApiVersion { get; set; }
        public string ApiDescription { get; set; }

        public string ApplicationIdentifier { get; set; }
        public string Environment { get; set; }

        public static implicit operator AppConfiguration(AppSettings settings)
        {
            return settings.AppConfiguration ?? throw new ArgumentException(nameof(AppConfiguration));
        }
    }
}
