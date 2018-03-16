using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace theCapitol.Web.Models
{
    public class ContactViewModel
    {
        [Display(Name = "name")]
        [StringLength(50, ErrorMessage = "* name cannot exceed 50 characters")]
        [Required(ErrorMessage = "* name is required")]
        public string Name { get; set; }

        [Display(Name = "email")]
        [StringLength(100, ErrorMessage = "* email cannot exceed 100 characters")]
        [Required(ErrorMessage = "* email is required")]
        [EmailAddress(ErrorMessage = "* you must enter a valid email address")]
        public string Email { get; set; }

        [Display(Name = "subject")]
        [StringLength(25, ErrorMessage = "* subject cannot exceed 25 characters")]
        [DisplayFormat(NullDisplayText = "email from contact form")]
        public string Subject { get; set; }

        [Display(Name = "message")]
        [Required(ErrorMessage = "* message is required")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}