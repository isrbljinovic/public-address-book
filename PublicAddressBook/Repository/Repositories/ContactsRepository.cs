using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Repository;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ContactsRepository : RepositoryBase<Contact>, IContactsRepository
    {
        public ContactsRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }

        public async Task<bool> Contains(Guid id)
        {
            return await FindByCondition(c => c.Id.Equals(id), false).AnyAsync();
        }

        public void CreateContact(Contact contact)
        {
            Create(contact);
        }

        public void DeleteContact(Contact contact)
        {
            Delete(contact);
        }

        public async Task<Contact> Get(Guid id, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(id), trackChanges).SingleAsync();
        }

        public async Task<IEnumerable<Contact>> GetAll(PagingInfo pagingInfo, bool trackChanges)
        {
            var countElements = await FindAll(trackChanges).CountAsync();

            var totalPages = (int)Math.Ceiling((decimal)countElements / pagingInfo.PageSize);

            if (pagingInfo.PageNumber > totalPages)
                return await FindAll(trackChanges)
                    .OrderBy(c => c.Name)
                    .Skip((totalPages - 1) * pagingInfo.PageSize)
                    .Take(pagingInfo.PageSize)
                    .ToListAsync();
            else
                return await FindAll(trackChanges)
                    .OrderBy(c => c.Name)
                    .Skip((pagingInfo.PageNumber - 1) * pagingInfo.PageSize)
                    .Take(pagingInfo.PageSize)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(c => ids.Contains(c.Id), trackChanges).ToListAsync();
        }
    }
}