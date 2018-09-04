using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetricsApplication.BussinessEntity
{
    public class MetricsDataDetails
    {
        public int id { get; set; }
        public string applicationName { get; set; }
        public string taskDescription { get; set; }
        public string taskClassification { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public double effortHours { get; set; }
    }
}