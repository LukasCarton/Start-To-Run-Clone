using System;
using System.Collections.Generic;
using System.Text;

namespace IntervalRunning.Models
{
    public class Intervals
    {
        public int week { get; set; }
        public int day { get; set; }
        public List<Interval> intervals { get; set; }
    }
}
