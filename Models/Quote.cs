using System;
using System.ComponentModel.DataAnnotations;

namespace dowd.Models
{
    public class Quote
    {
        public Guid QuoteId { get; set; }
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
        [Required(ErrorMessage = "The Product is required")]
        public string Product { get; set; }
        [Display(Name = "Width (mm)")]
        [Range(0, 9999999)]
        public int Width { get; set; }
        [Display(Name = "Height (mm)")]
        [Range(0, 9999999)]
        public int Height { get; set; }
        [Display(Name = "Installation Space Width (mm)")]
        [Range(0, 9999999)]
        public int InstallationSpaceWidth { get; set; }
        [Display(Name = "Installation Space Height (mm)")]
        public int InstallationSpaceHeight { get; set; }
        [Range(1, 9999999)]
        [Required(ErrorMessage = "The Quantity is required.")]
        public int Quantity { get; set; } = 1;
        public string Material { get; set; }
        [Display(Name = "PEGS")]
        public bool AttachPegs { get; set; }
        [Display(Name = "ADHESIVE")]
        public bool AttachAdhesive { get; set; }
        [Display(Name = "HOLES")]
        public bool AttachHoles { get; set; }
        [Display(Name = "SPACERS")]
        public bool AttachSpacers { get; set; }
        [Display(Name = "LABEL PINS")]
        public bool AttachLabelPins { get; set; }
        [Display(Name = "MAGNETIC STRIP")]
        public bool AttachMagneticStrip { get; set; }
        [Display(Name = "MOUNT")]
        public bool AttachMount { get; set; }
        [Display(Name = "OTHER")]
        public string AttachOther { get; set; }
        public bool InstallationRequired { get; set; } = true;
        public string HowDidYouFindUs { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public string[] Products = new[] { "CUT-OUT LETTERS", "NAME PLATE", "SIGN BOARD", "SAFETY SIGN", "INTERNATIONAL", "STICKER", "VEHICLE MARKING", "INDICATOR BOARD", "DESK STAND", "BADGE", "SERIAL PLATE", "PUNCH", "LABEL", "KEYRING", "MEDAL", "TROPHY", "CREST", "STAMP", "WINDOW DECAL", "EMBOSSING DIE", "BRANDING IRON", "OTHER" };
        public string[] Hows = new[] { "GOOGLE", "TRADE SHOW", "WORD OF MOUTH", "MAGAZINE/NEWSPAPER", "YELLOW PAGES", "ADVERTISMENT" };
    }
}