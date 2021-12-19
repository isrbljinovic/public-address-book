using System.Threading.Tasks;

namespace Contracts.Repository
{
    public interface IRepositoryManager
    {
        public IContactsRepository Contacts { get; }
        public ITelephoneNumbersRepository TelephoneNumbers { get; }

        Task Save();
    }
}