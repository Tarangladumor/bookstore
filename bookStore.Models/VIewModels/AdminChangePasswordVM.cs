using System.ComponentModel.DataAnnotations;

namespace bookStore.Models.ViewModels
{
    public class AdminChangePasswordVM
    {
        public string UserId { get; set; }
        [Display(Name = "User email")]

        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirm passowrd do not match.")]
        public string ConfirmPassword { get; set; }

    }
}