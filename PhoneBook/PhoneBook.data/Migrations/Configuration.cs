namespace PhoneBook.data.Migrations
{
    using PhoneBook.core.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhoneBook.data.PhoneBookDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PhoneBook.data.PhoneBookDbContext context)
        {
            context.ContactTypes.AddOrUpdate(x => x.Id,
                new ContactType() { Id = 1, Type = "Mobile" },
                new ContactType() { Id = 2, Type = "Home" },
                new ContactType() { Id = 3, Type = "Work" },
                new ContactType() { Id = 4, Type = "Other" }
        );
        }
    }
}
