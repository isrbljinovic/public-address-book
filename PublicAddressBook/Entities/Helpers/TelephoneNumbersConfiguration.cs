using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Helpers
{
    public class TelephoneNumbersConfiguration : IEntityTypeConfiguration<TelephoneNumber>
    {
        public void Configure(EntityTypeBuilder<TelephoneNumber> builder)
        {
            builder.HasData(
                new TelephoneNumber
                {
                    Id = new Guid("1296ee0a-d753-4cb4-924b-25b32ed86506"),
                    Number = "+3850901234567",
                    ContactId = new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360")
                },
                new TelephoneNumber
                {
                    Id = new Guid("99713f98-b250-4706-93b8-4fdaeb10e082"),
                    Number = "+3850907654321",
                    ContactId = new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360")
                },
                new TelephoneNumber
                {
                    Id = new Guid("709d116e-8341-410f-8078-19562ecdfb3d"),
                    Number = "+3850912345678",
                    ContactId = new Guid("cd03283d-bc37-46fe-a974-915860680b5d")
                }
                );
        }
    }
}