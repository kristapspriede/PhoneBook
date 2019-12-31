using System.Collections.Generic;

namespace PhoneBook.core.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string DateOfBirth { get; set; }
        public string Notes { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
        public List<PhoneNumberViewModel> PhoneNumbers { get; set; }
        public List<EmailViewModel> Emails { get; set; }
    }
}
