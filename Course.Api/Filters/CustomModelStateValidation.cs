using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Filters
{
    public class CustomModelStateValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validateFieldViewModel = new ValidateFieldViewModelOutput(context.ModelState.SelectMany(sm => sm.Value.Errors).Select(s  => s.ErrorMessage));
                context.Result = new BadRequestObjectResult(validateFieldViewModel);
            }
        }
    }
}
