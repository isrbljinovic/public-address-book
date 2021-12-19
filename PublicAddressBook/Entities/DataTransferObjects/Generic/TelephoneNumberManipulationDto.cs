using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Generic
{
    public class TelephoneNumberManipulationDto
    {
        [Required(ErrorMessage = "Telephone number is required.")]
        [MaxLength(20, ErrorMessage = "Number can't have more than 20 characters.")]
        public string Number { get; set; }
    }
}