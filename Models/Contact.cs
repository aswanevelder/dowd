using System;
using System.ComponentModel.DataAnnotations;

namespace dowd.Models
{
    public class Contact
    {
        public Guid ContactId { get; set; }
        [Required(ErrorMessage = "The Company is required")]
        public string Company { get; set; }
        [Display(Name = "Contact Person")]
        [Required(ErrorMessage = "The Contact Person is required")]
        public string ContactPerson { get; set; }
        [Required(ErrorMessage = "The Email is required.")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Email is incorrect")]
        public string Email { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter as 0213456789, 021-345-6789, (021)-345-6789")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "The Message is required")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}