using MetricsApplication.BussinessEntity;
using MetricsApplication.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace MetricsApplication.Controllers
{
    public class AnalysisController : Controller
    {
        DataGetter DataGetter = new DataGetter();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllMetrics()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                List<MetricsDataDetails> metricsDataDetails = DataGetter.AllMetricsGetter();
                if (metricsDataDetails.Count == 0)
                {
                    ViewBag.noData = "No Data";
                }
                ViewBag.metricsDataDetails = metricsDataDetails;
                return View(ViewBag.metricsDataDetails);
            }
        }
        [HttpPost]
        public ActionResult AllMetrics(string a)
        {
            List<MetricsDataDetails> metricsDataDetails = DataGetter.AllMetricsGetter();
            DataTable dt = new DataTable("Metrics");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Application"),
                                            new DataColumn("Task Description"),
                                            new DataColumn("Task Classification"),
                                            new DataColumn("Assigned Date"),
                                            new DataColumn("End Date"),
                                            new DataColumn("Effort Hours"),
                                            new DataColumn("Status"),
                                            new DataColumn("Assigned To"),
                                            new DataColumn("Secondary Index")});

            foreach (var item in metricsDataDetails)
            {
                if (item.taskClassification.ToUpper() == "APPLICATION TRAINING")
                    item.secondaryIndex = "FUNCTIONAL SUPPORT";
                if (item.taskClassification.ToUpper() == "BATCH JOB SHEDULEING")
                    item.secondaryIndex = "PROCESSING SUPPORT";
                if (item.taskClassification.ToUpper() == "DATABASE UPDATES")
                    item.secondaryIndex = "PROCESSING SUPPORT";
                if (item.taskClassification.ToUpper() == "DBA TASK")
                    item.secondaryIndex = "PROCESSING SUPPORT";
                if (item.taskClassification.ToUpper() == "DOCUMENTATION")
                    item.secondaryIndex = "FUNCTIONAL SUPPORT";
                if (item.taskClassification.ToUpper() == "IMPLEMENTATION TASK")
                    item.secondaryIndex = "PROCESSING SUPPORT";
                if (item.taskClassification.ToUpper() == "ONCALL ABEND RESOLUTION")
                    item.secondaryIndex = "FUNCTIONAL SUPPORT";
                if (item.taskClassification.ToUpper() == "REGULAR MAINTAINENCE")
                    item.secondaryIndex = "PROCESSING SUPPORT";
                if (item.taskClassification.ToUpper() == "RESEARCH WORK")
                    item.secondaryIndex = "FUNCTIONAL SUPPORT";
                if (item.taskClassification.ToUpper() == "SERVER SETUP/MAINTAINCE")
                    item.secondaryIndex = "PROCESSING SUPPORT";
                if (item.taskClassification.ToUpper() == "VERIFICATION")
                    item.secondaryIndex = "FUNCTIONAL SUPPORT";

                dt.Rows.Add(item.applicationName,item.taskDescription,item.taskClassification,item.startDate,item.endDate,item.effortHours,"Closed",item.userName,item.secondaryIndex);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllMetrics.xlsx");
                }
            }

        }
        public ActionResult AllStatusSheet()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                List<StatusSheet> statusDetails = DataGetter.AllstatusSheet();
                if (statusDetails.Count == 0)
                {
                    ViewBag.noData = "No Data";
                }
                ViewBag.statusDetails = statusDetails;
                return View(ViewBag.statusDetails);
            }
        }
        [HttpPost]
        public ActionResult AllStatusSheet(string a)
        {
            List<StatusSheet> statusDetails = DataGetter.AllstatusSheet();
            DataTable dt = new DataTable("StatusSheet");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Employee Id"),
                                            new DataColumn("User Name"),
                                            new DataColumn("Application Name"),
                                            new DataColumn("Total Hours")});
            foreach (var item in statusDetails)
            {
                dt.Rows.Add(item.employeeId, item.userName, item.applicationName, item.totalhours);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllStatusSheet.xlsx");
                }
            }
        }
        public ActionResult AllLeaveDetails()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                List<LeaveDetails> leaveDetails = DataGetter.AllLeaveDetailsGetter();
                if (leaveDetails.Count == 0)
                {
                    ViewBag.noData = "No Data";
                }
                ViewBag.leaveDetails = leaveDetails;
                return View(ViewBag.leaveDetails);
            }
        }
        [HttpPost]
        public ActionResult AllLeaveDetails(string a)
        {
            List<LeaveDetails> metricsDataDetails = DataGetter.AllLeaveDetailsGetter();
            DataTable dt = new DataTable("LeaveDetails");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Application"),
                                            new DataColumn("User Name"),
                                            new DataColumn("From Date"),
                                            new DataColumn("To Date"),
                                            new DataColumn("Number Of Days"),
                                            new DataColumn("Vacation Type"),
                                            new DataColumn("Comment") });
            foreach (var item in metricsDataDetails)
            {
                dt.Rows.Add(item.applicationName, item.userName, item.startDate, item.endDate, item.noOfDays, item.vacationType, item.comment);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllLeaves.xlsx");
                }
            }
        }
        public ActionResult NotFilled()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                return View(DataGetter.NotFilledGuys());
            }
        }
        public ActionResult GoBack()
        {
            return RedirectToAction("MetricsData","Admin");
        }
    }
}