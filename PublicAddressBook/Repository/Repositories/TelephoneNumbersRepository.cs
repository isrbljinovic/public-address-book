using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class TelephoneNumbersRepository : RepositoryBase<TelephoneNumber>, ITelephoneNumbersRepository
    {
        public TelephoneNumbersRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }

        public void CreateTelephoneNumber(Guid contactId, TelephoneNumber telephoneNumber)
        {
            telephoneNumber.ContactId = contactId;
            Create(telephoneNumber);
        }

        public void DeleteTelephoneNumber(TelephoneNumber telephoneNumber)
        {
            Delete(telephoneNumber);
        }

        public async Task<TelephoneNumber> GetTelephoneNumber(Guid contactId, Guid phoneNumberId, bool trackChanges)
        {
            return await FindByCondition(c => c.ContactId.Equals(contactId) && c.Id.Equals(phoneNumberId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TelephoneNumber>> GetTelephoneNumbers(Guid contactId, bool trackChanges)
        {
            return await FindByCondition(c => c.ContactId.Equals(contactId), trackChanges).ToListAsync();
        }
    }
}