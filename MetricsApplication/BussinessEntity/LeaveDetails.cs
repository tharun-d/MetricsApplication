using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetricsApplication.BussinessEntity
{
    public class LeaveDetails
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string vacationType { get; set; }
        public string comment { get; set; }
    }
}