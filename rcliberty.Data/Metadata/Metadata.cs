using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rcliberty.Data
{
    public class SeriesMetadata
    {
        [Display(Name = "Series Title")]
        [Required(ErrorMessage = "* Series Title is required")]
        [StringLength(100, ErrorMessage = "* Series Title cannot exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "* Image cannot exceed 100 characters")]
        public string Image { get; set; }

        [Display(Name = "Details")]
        [StringLength(250, ErrorMessage = "* Details cannot exceed 250 characters")]
        [UIHint("MultilineText")]
        public string Description { get; set; }
    }

    [MetadataType(typeof(SeriesMetadata))]
    public partial class Series { }
}
