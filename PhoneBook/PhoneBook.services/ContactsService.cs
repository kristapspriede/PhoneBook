using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.core.Models;
using PhoneBook.data;

namespace PhoneBook.services
{
    public class ContactsService : IContactsService
    {
        public async Task<ICollection<Contact>> GetContacts()
        {
            using (var context = new PhoneBookDbContext())
            {
                return await context.Contacts
                    .Include(c => c.PhoneNumbers)
                    .Include(c => c.Emails)
                    .Include(c => c.Addresses)
                    .ToListAsync();
            }
        }
        public async Task<ICollection<Address>> GetAddresses()
        {
            using (var context = new PhoneBookDbContext())
            {
                return await context.Addresses.ToListAsync();
            }
        }
        public async Task<List<Address>> GetAddress(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                return await context.Addresses.Where(c => c.ContactId == id).ToListAsync<Address>();
            }
        }
        public async Task<List<PhoneNumber>> GetPhoneNumber(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                return await context.PhoneNumbers.Where(c => c.ContactId == id).ToListAsync<PhoneNumber>();
            }
        }
        public async Task<List<Email>> GetEmail(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                return await context.Emails.Where(c => c.ContactId == id).ToListAsync<Email>();
            }
        }
        public async Task<Contact> GetContactById(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                return await context.Contacts
                        .Include(p => p.Emails)
                        .Include(p => p.PhoneNumbers)
                        .Include(c => c.Addresses)
                        .SingleOrDefaultAsync(p => p.Id == id);
                    
            }
        }
        public async Task<List<Contact>> GetContacts(string search)
        {
            using (var context = new PhoneBookDbContext())
            {
                search = search.ToLowerInvariant().Trim();
                
                var query = context.Contacts.Where(p => p.FirstName.ToLower().Contains(search.ToLower()) || 
                                                   p.LastName.ToLower().Contains(search.ToLower()) ||
                                                   p.Emails.Any(e => e.EmailAddress.ToLower().Contains(search.ToLower())) ||
                                                   p.PhoneNumbers.Any(ph => ph.Number.ToLower().Contains(search.ToLower()))).Include(c => c.PhoneNumbers)
                    .Include(c => c.Emails)
                    .Include(c => c.Addresses);
                return await query.ToListAsync();
            }
        }
        public async Task<Contact> AddContact(Contact contact)
        {
            using (var context = new PhoneBookDbContext())
            {
                contact = context.Contacts.Add(contact);
                await context.SaveChangesAsync();
                return contact;
            }
        }
        public async Task<Address> AddAddress(int id, Address address)
        {
            using (var context = new PhoneBookDbContext())
            {
                var contact = await context.Contacts.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (contact != null)
                {
                    address.ContactId = contact.Id;
                    context.Addresses.Add(address);
                    await context.SaveChangesAsync();
                    return address;
                }
                return address;
            }
        }
        public async Task<PhoneNumber> AddPhoneNumber(int id, PhoneNumber number)
        {
            using (var context = new PhoneBookDbContext())
            {
                var contact = await context.Contacts.Where(c => c.Id == id).SingleOrDefaultAsync();
                if (contact != null)
                {
                    number.ContactId = contact.Id;
                    context.PhoneNumbers.Add(number);
                    await context.SaveChangesAsync();
                    
                    return number;
                }
                return number;
            }
        }
        public async Task<Email> AddEmail(int id, Email email)
        {
            using (var context = new PhoneBookDbContext())
            {
                email = context.Emails.Add(email);
                await context.SaveChangesAsync();
                return email;
            }
        }
        public async Task<Contact> UpdateContact(int id, Contact contact)
        {
           
            using (var context = new PhoneBookDbContext())
            {
                var existingContact = await context.Contacts.Where(c => c.Id == id)
                    .FirstOrDefaultAsync<Contact>();
                if (existingContact != null)
                {
                    existingContact.FirstName = contact.FirstName;
                    existingContact.LastName = contact.LastName;
                    existingContact.Company = contact.Company;
                    existingContact.DateOfBirth = contact.DateOfBirth;
                    existingContact.Notes = contact.Notes;
                    await context.SaveChangesAsync();
                    return contact;
                }
                return contact;
            }
        }
        public async Task<Address> UpdateAddress(int contactId, int id, Address address)
        {

            using (var context = new PhoneBookDbContext())
            {
                var existingAddress = await context.Addresses.Where(c => c.ContactId == contactId && c.Id == id)
                    .FirstOrDefaultAsync<Address>();
                if (existingAddress != null)
                {
                    existingAddress.City = address.City;
                    existingAddress.StreetName = address.StreetName;
                    await context.SaveChangesAsync();
                    return address;
                }
                return address;
                
            }
        }
        public async Task<Email> UpdateEmail(int contactId, int id, Email email)
        {
            using (var context = new PhoneBookDbContext())
            {
                var existingEmail = await context.Emails.Where(c => c.ContactId == contactId && c.Id == id)
                    .FirstOrDefaultAsync<Email>();
                if (existingEmail != null)
                {
                    existingEmail.EmailAddress = email.EmailAddress;
                    existingEmail.ContactTypeId = email.ContactTypeId;
                    await context.SaveChangesAsync();
                    return email;
                }
                return email;
            }
        }
        public async Task<PhoneNumber> UpdatePhoneNumber(int contactId, int id, PhoneNumber number)
        {

            using (var context = new PhoneBookDbContext())
            {
                var existingPhoneNumber = await context.PhoneNumbers.Where(c => c.ContactId == contactId && c.Id == id)
                    .FirstOrDefaultAsync<PhoneNumber>();
                if (existingPhoneNumber != null)
                {
                    existingPhoneNumber.Number = number.Number;
                    existingPhoneNumber.ContactTypeId = number.ContactTypeId;
                    await context.SaveChangesAsync();
                    
                    return number;
                }
                return number;
            }
        }
        public async Task<List<ContactType>> GetAllTypes()
        {
            using (var context = new PhoneBookDbContext())
            {
                var types = await context.ContactTypes.ToListAsync();
                return types;
            }
        }
        public async Task<bool> DeleteContactById(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                var contact = await context.Contacts.SingleOrDefaultAsync(c => c.Id == id);
                if (contact != null)
                {
                    context.Contacts.Remove(contact);
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
        public async Task<bool> DeleteAddressById(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                var address = await context.Addresses.SingleOrDefaultAsync(c => c.Id == id);
                if (address != null)
                {
                    context.Addresses.Remove(address);
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
        public async Task<bool> DeleteEmailById(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                var email = await context.Emails.SingleOrDefaultAsync(c => c.Id == id);
                if (email != null)
                {
                    context.Emails.Remove(email);
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
        public async Task<bool> DeletePhoneNumberById(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                var number = await context.PhoneNumbers.SingleOrDefaultAsync(c => c.Id == id);
                if (number != null)
                {
                    context.PhoneNumbers.Remove(number);
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
        public static async Task<bool> DeleteContacts()
        {
            using (var context = new PhoneBookDbContext())
            {
                context.Contacts.RemoveRange(context.Contacts);
                context.Addresses.RemoveRange(context.Addresses);
                context.Emails.RemoveRange(context.Emails);
                context.PhoneNumbers.RemoveRange(context.PhoneNumbers);

                await context.SaveChangesAsync();
                return true;
            }
        }
        public bool Exists(int id)
        {
            using (var context = new PhoneBookDbContext())
            {
                return context.Contacts.Count(c => c.Id == id) > 0;
            }
        }
    }
}
