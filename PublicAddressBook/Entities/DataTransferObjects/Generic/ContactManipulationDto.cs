using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.DataTransferObjects.Creation;

namespace Entities.DataTransferObjects.Generic
{
    public class ContactManipulationDto
    {
        [Required(ErrorMessage = "Contact name is required.")]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Contact address is required.")]
        [MinLength(5, ErrorMessage = "Contact address must be at least five characters.")]
        public string Address { get; set; }

        public IEnumerable<TelephoneNumberCreationDto> TelephoneNumbers { get; set; }
    }
}