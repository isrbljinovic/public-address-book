using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    public class Contact
    {
        [Column("ContactId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Contact name is required.")]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Contact address is required.")]
        public string Address { get; set; }

        public ICollection<TelephoneNumber> TelephoneNumbers { get; set; }
    }
}