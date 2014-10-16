using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BatesOrtho.Models;

namespace BatesOrtho.Controllers
{
    public class HomeController : Controller
    {
        //private BatesOrthoEntities db = new BatesOrthoEntities();

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

        public ActionResult AppointmentRequest()
        {
            ViewBag.Message = "appointment request";

            return View();
        }

        [HttpPost]
        public JsonResult CreateAppointmentRequest(AppointmentRequest apptRequest)
        {
            using (var ctx = new BatesOrthoEntities())
            {
                ctx.AppointmentRequests.Add(apptRequest);
                ctx.SaveChanges();
            }
            return Json("OK");
        }
    }
}