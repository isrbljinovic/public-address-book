using System;
using System.Threading.Tasks;
using Contracts;
using Contracts.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PublicAddressBook.ActionFilters
{
    public class TelephoneForContactExsistsFilter : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public TelephoneForContactExsistsFilter(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var contactId = (Guid)context.ActionArguments["contactId"];

            var contactExsists = await _repository.Contacts.Contains(contactId);

            if (contactExsists)
            {
                var id = (Guid)context.ActionArguments["id"];

                var number = await _repository.TelephoneNumbers.GetTelephoneNumber(contactId, id, trackChanges);

                if (number == null)
                {
                    _logger.LogInfo($"Telephone number Id={id} not found.");
                    context.Result = new NotFoundResult();
                }
                else
                {
                    context.HttpContext.Items.Add("number", number);
                    await next();
                }
            }
            else
            {
                _logger.LogInfo($"Contact Id={contactId} not found.");
                context.Result = new NotFoundResult();
            }
        }
    }
}