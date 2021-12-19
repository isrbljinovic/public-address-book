using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts.Repository
{
    public interface ITelephoneNumbersRepository
    {
        Task<IEnumerable<TelephoneNumber>> GetTelephoneNumbers(Guid contactId, bool trackChanges);

        Task<TelephoneNumber> GetTelephoneNumber(Guid contactId, Guid phoneNumberId, bool trackChanges);

        void CreateTelephoneNumber(Guid contactId, TelephoneNumber telephoneNumber);

        void DeleteTelephoneNumber(TelephoneNumber telephoneNumber);
    }
}