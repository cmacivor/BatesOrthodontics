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
using System.Threading.Tasks;

namespace BatesOrtho.Controllers
{
    public class HomeController : Controller
    {

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

        public ActionResult SponsorshipRequest()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateSponsorshipRequest(Sponsorship request)
        {
            
            //return Json("OK");
            return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
        }


        [HttpPost]
        public JsonResult CreateDoctorReferral(DoctorRef referral)
        {
            if (!String.IsNullOrEmpty(referral.DoctorFirstName) && !String.IsNullOrEmpty(referral.DoctorLastName) 
                && !String.IsNullOrEmpty(referral.PracticeName) && !String.IsNullOrEmpty(referral.DoctorEmail) &&
                !String.IsNullOrEmpty(referral.PatientFirstName) && !String.IsNullOrEmpty(referral.PatientLastName)
                && !String.IsNullOrEmpty(referral.PatientPhoneNumber) && !String.IsNullOrEmpty(referral.PatientEmailAddress))
            {
                string path = Server.MapPath("~/EmailTemplates/DoctorReferralEmail.cshtml");
                string template = System.IO.File.OpenText(path).ReadToEnd();
                string message = Razor.Parse(template, referral);
                SendGmail mailer = new SendGmail();
                mailer.Subject = "New Doctor Referral";
                mailer.Body = message;
                mailer.Send(mailer.Subject, message);
            }
            
            //return Json("OK");
            return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
        }

        [HttpPost]
        public  ActionResult CreateContactRequest(Contact request)
        {
            if (!String.IsNullOrEmpty(request.FirstName) && !String.IsNullOrEmpty(request.Email))
            {
                string path = Server.MapPath("~/EmailTemplates/ContactEmail.cshtml");
                string template = System.IO.File.OpenText(path).ReadToEnd();
                string message = Razor.Parse(template, request);
                SendGmail mailer = new SendGmail();
                mailer.Subject = "New Contact Request";
                mailer.Body = message;
                mailer.Send(mailer.Subject, message);
                
  
            }
            return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateContactRequest(Contact request)
        //{
        //    if (!String.IsNullOrEmpty(request.FirstName) && !String.IsNullOrEmpty(request.Email))
        //    {
        //        string path = Server.MapPath("~/EmailTemplates/ContactEmail.cshtml");
        //        string template = System.IO.File.OpenText(path).ReadToEnd();
        //        string message = Razor.Parse(template, request);
        //        SendGmail mailer = new SendGmail();
        //        mailer.Subject = "New Contact Request";
        //        mailer.Body = message;
        //        await mailer.Send(mailer.Subject, message);


        //    }
        //    return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
        //}

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

            //return Json("OK");
           return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
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