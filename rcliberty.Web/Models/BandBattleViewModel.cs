using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rcliberty.Web.Models
{
    public class BandBattleViewModel
    {
        public string BandName { get; set; }
        public int NbrOfMembers { get; set; }
        public List<BandMemberViewModel> BandMembers { get; set; }
        public string Details { get; set; }
        public decimal Payment { get; set; }
        public bool HasPaid { get; set; }
    }
}