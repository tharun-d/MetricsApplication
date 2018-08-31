using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetricsApplication.BussinessEntity;
using MetricsApplication.DataAccessLayer;

namespace MetricsApplication.Controllers
{
    public class HomeController : Controller
    {
        DataGetter DataGetter = new DataGetter();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDetails loginDetails)
        {
            string username = DataGetter.LoginValidator(loginDetails);
            if (username!=null)
            {
                Session["username"] = username;
                return RedirectToAction("MetricsData", "Metrics");
            }
            else
                return View();
        }
    }
}