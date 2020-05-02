using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rcliberty.Web.Models
{
    public class BandViewModel
    {
        [Display(Name = "Band Name")]
        [Required(ErrorMessage = "* Band Name is required")]
        public string BandName { get; set; }

        [Display(Name = "# of Band Members")]
        [Required(ErrorMessage = "* Please provide the number of members in your group")]
        public int NbrOfMembers { get; set; }

        [Display(Name = "Rhythm Guitar")]
        public bool RhythmGuitar { get; set; }

        [Display(Name = "Lead Guitar")]
        public bool LeadGuitar { get; set; }

        [Display(Name = "Bass Guitar")]
        public bool BassGuitar { get; set; }

        [Display(Name = "Drums")]
        public bool Drums { get; set; }

        [Display(Name = "# of Vocals")]
        [Required(ErrorMessage = "* Please enter a valid # of vocalists")]
        public int NbrOfVocals { get; set; }

        [Display(Name = "Other Instruments")]
        public bool Other { get; set; }

        [Display(Name = "Other Instrument Details")]
        [UIHint("MultilineText")]
        public string OtherInstrumentDetails { get; set; }

        [Display(Name = "Additional Info & Gear")]
        [UIHint("MultilineText")]
        public string Details { get; set; }

        [Display(Name = "Point of Contact")]
        [Required(ErrorMessage = "* Please provide the name of who we should contact")]
        public string ContactName { get; set; }

        [Display(Name = "Contact Email Address")]
        [Required(ErrorMessage = "* You must enter a Contact Email Address")]
        [EmailAddress]
        public string ContactInfo { get; set; }
    }
}