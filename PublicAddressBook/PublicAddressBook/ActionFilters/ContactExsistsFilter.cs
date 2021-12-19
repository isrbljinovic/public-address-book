using System;
using System.Threading.Tasks;
using Contracts;
using Contracts.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PublicAddressBook.ActionFilters
{
    public class ContactExsistsFilter : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ContactExsistsFilter(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];

            var contact = await _repository.Contacts.Get(id, trackChanges);

            if (contact == null)
            {
                _logger.LogInfo($"Contact Id={id} not found.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("contact", contact);
                await next();
            }
        }
    }
}