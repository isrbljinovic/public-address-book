using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Repository;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Creation;
using Entities.DataTransferObjects.Update;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PublicAddressBook.ActionFilters;
using PublicAddressBook.Hubs;
using PublicAddressBook.ModelBinders;

namespace PublicAddressBook.Controllers
{
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IHubContext<ContactsHub> _hubContext;

        public ContactsController(IRepositoryManager repositoryManager, IMapper mapper,
                                    ILoggerManager loggerManager, IHubContext<ContactsHub> hubContext)
        {
            _repository = repositoryManager;
            _mapper = mapper;
            _logger = loggerManager;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts([FromQuery] PagingInfo pagingInfo)
        {
            var contacts = await _repository.Contacts.GetAll(pagingInfo, false);

            var contactsDto = _mapper.Map<IEnumerable<ContactDto>>(contacts);

            return Ok(contactsDto);
        }

        [HttpGet("{id}", Name = "ContactById")]
        [ServiceFilter(typeof(ContactExsistsFilter))]
        public IActionResult GetContact(Guid id)
        {
            var contact = HttpContext.Items["contact"] as Contact;

            var contactDto = _mapper.Map<ContactDto>(contact);

            return Ok(contactDto);
        }

        [HttpGet("collection/({ids})", Name = "ContactsCollection")]
        public async Task<IActionResult> GetContacts([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Sent guids are null");
                return BadRequest("Sent guids are null");
            }

            var contacts = await _repository.Contacts.GetByIds(ids, false);

            if (contacts.Count() != ids.Count())
            {
                _logger.LogError("Some guids are not valid.");
                return NotFound("Some guids are not valid");
            }

            var contactsToReturn = _mapper.Map<IEnumerable<ContactDto>>(contacts);

            return Ok(contactsToReturn);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> CreateContact([FromBody] ContactCreationDto contact)
        {
            var contactForInsert = _mapper.Map<Contact>(contact);

            _repository.Contacts.CreateContact(contactForInsert);
            await _repository.Save();

            var contactToReturn = _mapper.Map<ContactDto>(contactForInsert);

            await _hubContext.Clients.All.SendAsync("CreatedOne", contactToReturn);

            return CreatedAtRoute("ContactById", new { id = contactToReturn.Id }, contactToReturn);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> CreateContacts([FromBody] IEnumerable<ContactCreationDto> contacts)
        {
            var contactsToInsert = _mapper.Map<IEnumerable<Contact>>(contacts);

            foreach (var contact in contactsToInsert)
            {
                _repository.Contacts.CreateContact(contact);
            }

            await _repository.Save();

            var contactsToReturn = _mapper.Map<IEnumerable<ContactDto>>(contactsToInsert);

            var ids = string.Join(",", contactsToReturn.Select(c => c.Id));

            await _hubContext.Clients.All.SendAsync("CreatedMultiple", contactsToReturn);

            return CreatedAtRoute("ContactsCollection", new { ids }, contactsToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ContactExsistsFilter))]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contactToDelete = HttpContext.Items["contact"] as Contact;

            _repository.Contacts.DeleteContact(contactToDelete);
            await _repository.Save();

            await _hubContext.Clients.All.SendAsync("DeletedOne", contactToDelete.Name);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilter))]
        [ServiceFilter(typeof(ContactExsistsFilter))]
        public async Task<IActionResult> UpdateContact(Guid id, [FromBody] ContactUpdateDto contact)
        {
            var contactToUpdate = HttpContext.Items["contact"] as Contact;

            _mapper.Map(contact, contactToUpdate);
            await _repository.Save();

            await _hubContext.Clients.All.SendAsync("UpdatedContact", contact);
            return NoContent();
        }
    }
}