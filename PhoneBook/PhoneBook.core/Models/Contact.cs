using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PhoneBook.core.Models
{
    public class Contact
    {
        
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Company { get; set; }
        public string DateOfBirth { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public Contact()
        {
            PhoneNumbers = new List<PhoneNumber>();
            Emails = new List<Email>();
            Addresses = new List<Address>();
        }
    }
}
