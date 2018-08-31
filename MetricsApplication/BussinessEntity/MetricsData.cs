using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetricsApplication.BussinessEntity
{
    public class MetricsData
    {
        public string applicationName { get; set; }
        public string taskDescription { get; set; }
        public string taskClassification { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int effortHours { get; set; }
    }
}