using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Repository;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Creation;
using Entities.DataTransferObjects.Update;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using PublicAddressBook.ActionFilters;

namespace PublicAddressBook.Controllers
{
    [Route("api/contacts/{contactId}/numbers")]
    [ApiController]
    public class TelephoneNumbersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TelephoneNumbersController(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager loggerManager)
        {
            _repository = repositoryManager;
            _mapper = mapper;
            _logger = loggerManager;
        }

        [HttpGet]
        [ServiceFilter(typeof(ContactForTelephoneExsistsFilter))]
        public async Task<IActionResult> GetTelephoneNumbers(Guid contactId)
        {
            var phoneNumbers = await _repository.TelephoneNumbers.GetTelephoneNumbers(contactId, false);

            var phoneNumbersDto = _mapper.Map<IEnumerable<TelephoneNumberDto>>(phoneNumbers);

            return Ok(phoneNumbersDto);
        }

        [HttpGet("{id}", Name = "ContactsNumber")]
        [ServiceFilter(typeof(TelephoneForContactExsistsFilter))]
        public IActionResult GetTelephoneNumber(Guid contactId, Guid id)
        {
            var phoneNumber = HttpContext.Items["number"] as TelephoneNumber;

            var phoneNumberDto = _mapper.Map<TelephoneNumberDto>(phoneNumber);

            return Ok(phoneNumberDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        [ServiceFilter(typeof(ContactForTelephoneExsistsFilter))]
        public async Task<IActionResult> CreateTelephoneNumber(Guid contactId, [FromBody] TelephoneNumberCreationDto telephoneNumber)
        {
            var phoneNumberForInsert = _mapper.Map<TelephoneNumber>(telephoneNumber);

            _repository.TelephoneNumbers.CreateTelephoneNumber(contactId, phoneNumberForInsert);
            await _repository.Save();

            var phoneNumberToReturn = _mapper.Map<TelephoneNumberDto>(phoneNumberForInsert);

            return CreatedAtRoute("ContactsNumber", new { contactId, id = phoneNumberToReturn.Id }, phoneNumberToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(TelephoneForContactExsistsFilter))]
        public async Task<IActionResult> DeleteTelephoneNumber(Guid contactId, Guid id)
        {
            var numberToDelete = HttpContext.Items["number"] as TelephoneNumber;

            _repository.TelephoneNumbers.DeleteTelephoneNumber(numberToDelete);
            await _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilter))]
        [ServiceFilter(typeof(TelephoneForContactExsistsFilter))]
        public async Task<IActionResult> UpdateTelephoneNumber(Guid contactId, Guid id, [FromBody] TelephoneNumberUpdateDto telephoneNumber)
        {
            var numerToUpdate = HttpContext.Items["number"] as TelephoneNumber;

            _mapper.Map(telephoneNumber, numerToUpdate);
            await _repository.Save();
            return NoContent();
        }
    }
}