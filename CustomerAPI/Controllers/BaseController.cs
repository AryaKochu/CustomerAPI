using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;

namespace CustomerAPI.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            
        }
        protected IActionResult Ok<T>(T data)
        {
          return new OkObjectResult(data);
        }

        protected virtual bool ModelStateIsValid => ModelState.IsValid;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (ModelStateIsValid)
            {
                return;
            }

            context.Result = new BadRequestObjectResult((CommonError)ModelState);
        }
    }
}

