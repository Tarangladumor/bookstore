using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookStore.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Street Address")]

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [Display(Name = "Phone Nmber")]
        public string? PhoneNumber { get; set; }


        public string? Role { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}