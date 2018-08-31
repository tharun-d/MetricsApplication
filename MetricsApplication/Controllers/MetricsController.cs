using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetricsApplication.BussinessEntity;
using MetricsApplication.DataAccessLayer;

namespace MetricsApplication.Controllers
{
    public class MetricsController : Controller
    {
        // GET: Metrics
        DataGetter DataGetter = new DataGetter();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MetricsData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MetricsData(MetricsData metricsData)
        {
            DataGetter.MetricsFiller(metricsData);
            return View();
        }
        public ActionResult LeaveDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LeaveDetails(LeaveDetails leaveDetails)
        {
            DataGetter.LeaveDetailsFiller(leaveDetails);
            return View();
        }
        public ActionResult CATWhours()
        {     
            return View();
        }
        [HttpPost]
        public ActionResult CATWhours(int hours)
        {
            DataGetter.CATWfiller(hours);
            return View();
        }
    }
}