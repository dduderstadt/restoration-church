using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rcliberty.Web.Models
{
    public class BandBattleViewModel
    {
        [Display(Name = "Band Name")]
        [Required(ErrorMessage = "* Band Name is required")]
        public string BandName { get; set; }

        public int NbrOfMembers { get; set; }

        [Display(Name = "Band Members")]
        [Required(ErrorMessage = "* Please list all band members")]
        public List<BandMemberViewModel> BandMembers { get; set; }

        [Display(Name = "Additional Info")]
        public string Details { get; set; }

        [Display(Name = "Contact Email Address")]
        [Required(ErrorMessage = "* You must enter a Contact Email Address")]
        [EmailAddress]
        public string ContactInfo { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}