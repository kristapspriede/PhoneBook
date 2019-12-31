using PhoneBook.core.Models;
using System.Data.Entity;
using PhoneBook.data.Migrations;

namespace PhoneBook.data
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext() : base("PhoneBookDbContext")
        {
            Database.SetInitializer<PhoneBookDbContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhoneBookDbContext, Configuration>());
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }


    }

}
