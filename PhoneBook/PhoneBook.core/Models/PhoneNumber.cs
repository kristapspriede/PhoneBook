using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.core.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public int ContactTypeId { get; set; }


    }
}
