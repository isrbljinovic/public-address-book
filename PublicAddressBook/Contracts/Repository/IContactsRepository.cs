using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Helpers;
using Entities.Models;

namespace Contracts.Repository
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> GetAll(PagingInfo pagingInfo, bool trackChanges);

        Task<Contact> Get(Guid id, bool trackChanges);

        Task<bool> Contains(Guid id);

        void CreateContact(Contact contact);

        Task<IEnumerable<Contact>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteContact(Contact contact);
    }
}