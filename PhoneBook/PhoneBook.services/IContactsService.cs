using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneBook.core.Models;

namespace PhoneBook.services
{
    public interface IContactsService
    {
        Task<ICollection<Contact>> GetContacts();
        Task<ICollection<Address>> GetAddresses();
        Task<Contact> GetContactById(int id);
        Task<List<Contact>> GetContacts(string search);
        Task<Contact> AddContact(Contact contact);
        Task<Address> AddAddress(int id, Address address);
        Task<PhoneNumber> AddPhoneNumber(int id, PhoneNumber number);
        Task<Email> AddEmail(int id, Email email);
        Task<Contact> UpdateContact(int id, Contact contact);
        Task<Address> UpdateAddress(int contactId, int id, Address address);
        Task<Email> UpdateEmail(int contactId, int id, Email email);
        Task<PhoneNumber> UpdatePhoneNumber(int contactId, int id, PhoneNumber number);
        Task<List<ContactType>> GetAllTypes();
        Task<bool> DeleteContactById(int id);
        Task<bool> DeleteAddressById(int id);
        Task<bool> DeleteEmailById(int id);
        Task<bool> DeletePhoneNumberById(int id);
        Task<List<Address>> GetAddress(int id);
        Task<List<PhoneNumber>> GetPhoneNumber(int id);
        Task<List<Email>> GetEmail(int id);
        bool Exists(int id);
    }
}
