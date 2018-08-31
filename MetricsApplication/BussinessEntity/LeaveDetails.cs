using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetricsApplication.BussinessEntity
{
    public class LeaveDetails
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string vacationType { get; set; }
        public string comment { get; set; }
    }
}