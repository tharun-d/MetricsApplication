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
            if (Session["userName"]==null)            
                return RedirectToAction("Login", "Home");
            else
            {
                List<MetricsDataDetails> metricsDataDetails = DataGetter.AllMetricsGetterbyUserName(Session["userName"].ToString());
                if (metricsDataDetails.Count==0)
                {
                    ViewBag.noData = "No Data";
                }
                ViewBag.metricsDataDetails = metricsDataDetails;
                return View(ViewBag.metricsDataDetails);
            }
            
        }
        [HttpPost]
        public ActionResult MetricsData(MetricsDataDetails metricsData)
        {
            DataGetter.MetricsFiller(metricsData,Session["userName"].ToString());
            return RedirectToAction("MetricsData", "Metrics"); ;
        }
        public ActionResult LeaveDetails()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                List<LeaveDetails> leaveDetails = DataGetter.AllLeaveDetailsbyUserName(Session["userName"].ToString());
                if (leaveDetails.Count == 0)
                {
                    ViewBag.noData = "No Data";
                }
                ViewBag.leaveDetails = leaveDetails;
                return View(ViewBag.leaveDetails);
            }
            
        }
        [HttpPost]
        public ActionResult LeaveDetails(LeaveDetails leaveDetails)
        {
            if (leaveDetails.vacationType==null)
                leaveDetails.vacationType = "Not mentioned";

            if (leaveDetails.comment == null)
                leaveDetails.comment = "Not mentioned";

            DataGetter.LeaveDetailsFiller(leaveDetails, Session["userName"].ToString());
            return RedirectToAction("LeaveDetails", "Metrics");
        }
        public ActionResult CATWhours()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                ViewBag.Hours = DataGetter.GetCatwHoursByUserName(Session["userName"].ToString());
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult CATWhours(int hours)
        {
            DataGetter.CATWfiller(hours,Session["userName"].ToString());
            return RedirectToAction("CATWhours", "Metrics");
        }
        public ActionResult LogOff()
        {
            Session["useName"]= null;
            return RedirectToAction("Login", "Home");
        }
        public ActionResult EditCatw(int id)
        {
            return View();
        }
        public ActionResult DeleteCatw(int id)
        {
            DataGetter.MetricsDeleteByid(id);
            return RedirectToAction("MetricsData", "Metrics");
        }
        public ActionResult DeleteLeaveDetails(int id)
        {
            DataGetter.DeleteLeaveDetailsByid(id);
            return RedirectToAction("LeaveDetails", "Metrics");
        }
        public ActionResult hhh()
        {
            return View();
        }
    }
}