using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Helpers
{
    public class ContactsConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasData(
                new Contact
                {
                    Id = new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360"),
                    Name = "Ivan Srbljinovic",
                    DateOfBirth = DateTime.Parse("29/05/1997"),
                    Address = "Tina Ujevica 3, Krizevci"
                },
                new Contact
                {
                    Id = new Guid("c536dde5-4e5b-440c-9801-74d4a8fc7440"),
                    Name = "John Doe",
                    DateOfBirth = DateTime.Parse("01/01/1990"),
                    Address = "Trg bana Jelacica 1, Zagreb"
                },
                new Contact
                {
                    Id = new Guid("cd03283d-bc37-46fe-a974-915860680b5d"),
                    Name = "Jane Doe",
                    DateOfBirth = DateTime.Parse("01/01/1991"),
                    Address = "Trg bana Jelacica 1, Zagreb"
                }
                );
        }
    }
}