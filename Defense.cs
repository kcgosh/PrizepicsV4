using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizepicsV1
{
    public class Defense
    {
        //strings
        public String Team { get; set; }

        //doubles
        public double? PointAllow { get; set; }
        public double? RebAllow { get; set; }
        public double? OffRebAllow { get; set; }
        public double? DefRebAllow { get; set; }
        public double? AssAllow { get; set; }
        public double? FgAllow { get; set; }
        public double? ThreeAllow { get; set; }
        public double? Blk { get; set; }
        public double? OppFgp { get; set; }
        public double? Oppthreep { get; set; }
        public double? Rtg { get; set; }
    }
}
