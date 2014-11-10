using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BatesOrtho.Models;
using System.IO;
using RazorEngine;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using BatesOrtho;

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

        public ActionResult ThankYou()
        {
            return View("ThankYou");
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
        public JsonResult CreateSponsorshipRequest(Sponsorship request)
        {
            
            return Json("OK");
        }


        [HttpPost]
        public JsonResult CreateDoctorReferral(DoctorRef referral)
        {
            //using (var ctx = new BatesOrthoEntities())
            //{
            //   ctx.DoctorReferrals.Add(referral);
            //   ctx.SaveChanges();
            //}
            return Json("OK");
        }

        [HttpPost]
        public ActionResult CreateContactRequest(Contact request)
        {
            string path = Server.MapPath("~/EmailTemplates/ContactEmail.cshtml");
            
            return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
        }

        [HttpPost]
        public JsonResult CreateAppointmentRequest(AppointmentReq apptRequest)
        {
           if (!String.IsNullOrEmpty(apptRequest.FirstName) && !String.IsNullOrEmpty(apptRequest.LastName) &&
               !String.IsNullOrEmpty(apptRequest.ResponsiblePartyFirstName) && !String.IsNullOrEmpty(apptRequest.ResponsiblePartyLastName)
               && !String.IsNullOrEmpty(apptRequest.Phone) && !String.IsNullOrEmpty(apptRequest.Email))
           {
               
               string path = Server.MapPath("~/EmailTemplates/AppointmentRequestEmail.cshtml");
               DateTime dateOnly = apptRequest.DOB.Date;
               apptRequest.DOB = dateOnly;
               string template = System.IO.File.OpenText(path).ReadToEnd();
               string message = Razor.Parse(template, apptRequest);
               SendGmail mailer = new SendGmail();
               mailer.Subject = "New Appointment Request";
               mailer.Body = message;
               mailer.Send(mailer.Subject, message);
           }

            return Json("OK");
        }

        //[HttpPost]
        //public JsonResult CreatePreferredAppointmentDay(string[] checkedValues)
        //{
        //    using (var ctx = new BatesOrthoEntities())
        //    {
        //        var justEntered = (from a in ctx.AppointmentRequests
        //                           orderby a.AppointmentRequestID descending
        //                           select a).FirstOrDefault();

        //        foreach (var day in checkedValues)
        //        {
        //            PreferredAppointmentDay preferredDay = new PreferredAppointmentDay();
        //            preferredDay.AppointmentRequestDay = day;
        //            preferredDay.AppointmentRequestID = justEntered.AppointmentRequestID;
        //            ctx.PreferredAppointmentDays.Add(preferredDay);
                    
        //        }
        //        ctx.SaveChanges();
        //    }

        //    return Json("OK");
        //}


    }

   
}