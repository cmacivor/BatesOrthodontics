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
        public JsonResult CreateContactRequest(Contact request)
        {
            return Json("OK");
        }

        [HttpPost]
        public JsonResult CreateAppointmentRequest(AppointmentReq apptRequest)
        {
           if (apptRequest != null)
           {
               //var template = new RazorEngine.Templating.TemplateService();
              // var emailBody = template.Parse(System.IO.File.ReadAllText("~/EmailTemplates/AppointmentRequestEmail.cshtml"), apptRequest, null, null);
               //string path = @"C:\Users\Craig\Documents\Visual Studio 2013\Projects\BatesOrtho\BatesOrtho\EmailTemplates";
               string path = Server.MapPath("~/EmailTemplates/AppointmentRequestEmail.cshtml");
               string template = System.IO.File.OpenText(path).ReadToEnd();
               string message = Razor.Parse(template, apptRequest);
               
               SendGmail.GmailUsername = "cmacivor82@gmail.com";
               SendGmail.GmailPassword = "Caesar2810$";

               SendGmail mailer = new SendGmail();
               mailer.ToEmail = "cmacivor82@gmail.com";
               mailer.Subject = "Verify your email id";
               //mailer.Body = "Thanks for Registering your account.<br> please verify your email id by clicking the link <br> <a href='youraccount.com/verifycode=12323232'>verify</a>";
               mailer.Body = message;
               mailer.IsHtml = true;
               mailer.Send();
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