using System.Linq;
using System.Text.Json;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PublicAddressBook.ActionFilters
{
    public class ValidationFilter : IActionFilter
    {
        private readonly ILoggerManager _logger;

        public ValidationFilter(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var args = context.ActionArguments.SingleOrDefault(a => a.Value.ToString().Contains("Dto")).Value;

            if (args == null)
            {
                _logger.LogError($"Received object is null. {controller}Controller -> {action} ({JsonSerializer.Serialize(args)})");
                context.Result = new BadRequestObjectResult($"Received object is null. {controller}Controller -> {action}");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"Received object is invalid. {controller}Controller -> {action} ({JsonSerializer.Serialize(args)})");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}