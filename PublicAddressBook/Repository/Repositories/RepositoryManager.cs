using System.Threading.Tasks;
using Contracts.Repository;
using Entities;

namespace Repository.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private DataBaseContext _context;
        private IContactsRepository _contactsRepository;
        private ITelephoneNumbersRepository _telephoneNumbersRepository;

        public IContactsRepository Contacts => (_contactsRepository is null) ?
            new ContactsRepository(_context) : _contactsRepository;

        public ITelephoneNumbersRepository TelephoneNumbers => _telephoneNumbersRepository is null ?
            new TelephoneNumbersRepository(_context) : _telephoneNumbersRepository;

        public RepositoryManager(DataBaseContext dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}