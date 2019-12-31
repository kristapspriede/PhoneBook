using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.core.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

    }
}
