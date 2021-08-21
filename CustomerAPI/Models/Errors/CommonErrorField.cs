using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models.Errors
{
    public class CommonErrorField
    {
        public string Field{ get; set; }
        public string Message{ get; set; }
        public string Code{ get; set; }
    }
}
