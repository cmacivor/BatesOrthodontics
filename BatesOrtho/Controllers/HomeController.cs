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

        public ActionResult Patients()
        {
            ViewBag.Message = "patients";

            return View();
        }

        public ActionResult AppointmentRequest()
        {
            ViewBag.Message = "appointment request";

            return View();
        }

        public ActionResult DoctorReferral()
        {
            return View();
        }

        public ActionResult SponsorshipRequest()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateSponsorshipRequest(SponsorshipRequest request)
        {
            return Json("OK");
        }

        [HttpPost]
        public JsonResult CreateDoctorReferral(DoctorReferral referral)
        {
            using (var ctx = new BatesOrthoEntities())
            {
               ctx.DoctorReferrals.Add(referral);
               ctx.SaveChanges();
            }
            return Json("OK");
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

        [HttpPost]
        public JsonResult CreatePreferredAppointmentDay(string[] checkedValues)
        {
            using (var ctx = new BatesOrthoEntities())
            {
                var justEntered = (from a in ctx.AppointmentRequests
                                   orderby a.AppointmentRequestID descending
                                   select a).FirstOrDefault();

                foreach (var day in checkedValues)
                {
                    PreferredAppointmentDay preferredDay = new PreferredAppointmentDay();
                    preferredDay.AppointmentRequestDay = day;
                    preferredDay.AppointmentRequestID = justEntered.AppointmentRequestID;
                    ctx.PreferredAppointmentDays.Add(preferredDay);
                    
                }
                ctx.SaveChanges();
            }

            return Json("OK");
        }


    }
}