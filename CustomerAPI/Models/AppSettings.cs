using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Bff.Models
{
    public class AppSettings
    {
        public AppConfiguration AppConfiguration { get; set; }
        public CustomerApiSettings CustomerApi { get; set; }
    }
}
