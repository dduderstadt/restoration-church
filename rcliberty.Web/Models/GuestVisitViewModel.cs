using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rcliberty.Web.Models
{
    public class GuestVisitViewModel
    {
        [Required(ErrorMessage = "* Please enter a First Name")]
        [Display(Name = "First Name")]
        [StringLength(15, ErrorMessage = "* First Name cannot exceed 15 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "* Last Name cannot exceed 25 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* Please enter a valid Email address to send your message")]
        [StringLength(100, ErrorMessage = "* Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        [StringLength(12, ErrorMessage = "* Please enter Contact Number as ###-###-####")]
        public string PhoneNbr { get; set; }

        [Display(Name = "Are you bringing any children with you?")]
        public bool IsBringingKids { get; set; }

        [Display(Name = "Can we answer any questions for you before your visit?")]
        [UIHint("MultilineText")]
        public string AdditionalQuestions { get; set; }
    }
}