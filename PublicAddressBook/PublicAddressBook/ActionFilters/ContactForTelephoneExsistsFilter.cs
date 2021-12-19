using System;
using System.Threading.Tasks;
using Contracts;
using Contracts.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PublicAddressBook.ActionFilters
{
    public class ContactForTelephoneExsistsFilter : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ContactForTelephoneExsistsFilter(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var contactId = (Guid)context.ActionArguments["contactId"];

            var contactExsists = await _repository.Contacts.Contains(contactId);

            if (!contactExsists)
            {
                _logger.LogInfo($"Contact Id={contactId} not found.");
                context.Result = new NotFoundResult();
            }
            else
            {
                await next();
            }
        }
    }
}