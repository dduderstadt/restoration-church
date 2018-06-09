using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rcliberty.Web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Please enter a First Name")]
        [Display(Name = "First Name")]
        [StringLength(15, ErrorMessage = "* First Name cannot exceed 15 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "* Last Name cannot exceed 25 characters")]
        public string LastName { get; set; }

        [StringLength(15, ErrorMessage = "* Subject cannot exceed 15 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "* Please enter a valid Email to send your message")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "* Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Please enter your Message")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}