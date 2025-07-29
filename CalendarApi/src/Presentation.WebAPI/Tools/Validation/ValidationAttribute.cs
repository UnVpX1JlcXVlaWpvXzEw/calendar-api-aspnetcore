using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Exception.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.Validation
{
    public class ValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                     .SelectMany(v => v.Errors)
                     .Select(v => v.ErrorMessage)
                     .ToArray();

                var responseObj = new ErrorMessage
                {
                    Status = 400,
                    Message = string.Join(", ", errors)
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
