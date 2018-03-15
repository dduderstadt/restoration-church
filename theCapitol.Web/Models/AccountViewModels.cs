using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace theCapitol.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "* email is required")]
        [Display(Name = "email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "* password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [Display(Name = "remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "* email is required")]
        [EmailAddress]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* password is required")]
        [StringLength(100, ErrorMessage = "the {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirm password")]
        [Compare("Password", ErrorMessage = "* password & confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /*added for Connections*/
        [Display(Name = "first name")]
        [Required(ErrorMessage = "* first name is required")]
        [StringLength(25, ErrorMessage = "* first name cannot exceed 25 characters")]
        public string FirstName { get; set; }

        [Display(Name = "last name")]
        [Required(ErrorMessage = "* last name is required")]
        [StringLength(25, ErrorMessage = "* last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Display(Name = "invitation code")]
        [Required(ErrorMessage = "* invitation code is required")]
        [StringLength(5, ErrorMessage = "* invitation code invalid")]
        public string InvitationCode { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
