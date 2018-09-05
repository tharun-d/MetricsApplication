using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetricsApplication.BussinessEntity
{
    public class StatusSheet
    {
        public string employeeId { get; set; }
        public string userName { get; set; }
        public string applicationName { get; set; }
        public int totalhours { get; set; }
    }
}