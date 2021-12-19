using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(c => new { c.Name, c.Address }).IsUnique();

            modelBuilder.ApplyConfiguration(new ContactsConfiguration());
            modelBuilder.ApplyConfiguration(new TelephoneNumbersConfiguration());
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TelephoneNumber> TelephoneNumbers { get; set; }
    }
}