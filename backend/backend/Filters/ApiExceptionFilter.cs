using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace backend.Filters
{
    public class ApiExceptionFIlter
    {
        public class ApiExceptionFilter : IActionFilter
        {
            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (context.Exception is ArgumentException ex)
                {
                    context.ExceptionHandled = true;
                    context.Result = new BadRequestObjectResult(new ErrorModel()
                    {
                        Message = ex.Message,
                        Date = DateTime.Now
                    });
                }
            }
            public void OnActionExecuting(ActionExecutingContext context)
            {
            }
        }
    }
}
