using CustomerAPI.Models.Errors;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class CommonError
    {
        public CommonError()
        {

        }
        public CommonError(string code, string message) : this(code, message,  new List<CommonErrorField>())
        {

        }
        public CommonError(string code, string message, IList<CommonErrorField> commonErrorFields)
        {

        }

        public string Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<CommonErrorField> Errors{ get; set; }

        public static explicit operator CommonError(ModelStateDictionary modelState)
        {
            if(modelState == null)
            {
                return null;
            }

            return new CommonError("BadRequest", "Data validation errors")
            {
                Errors = modelState.SelectMany(kv => kv.Value.Errors.Select(error =>
                {
                    return new CommonErrorField
                    {
                        Field = kv.Key,

                        Message = string.IsNullOrWhiteSpace(error.ErrorMessage)
                        ? "The data is invalid for this field"
                        : error.ErrorMessage,
                        Code = "DataInvalid"
                    };
                }))
            };
        }
    }
}
