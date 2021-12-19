using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class TelephoneNumber
    {
        [Column("TelephoneNumberId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Telephone number is required.")]
        public string Number { get; set; }

        [ForeignKey(nameof(Contact))]
        public Guid ContactId { get; set; }

        public Contact Contact { get; set; }
    }
}