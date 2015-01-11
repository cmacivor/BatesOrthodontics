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
using System.Text.RegularExpressions;
using System.Text;

namespace BatesOrtho.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            System.Net.WebClient client = new System.Net.WebClient();
            //string downloadString = client.DownloadString("http://blog.bates-orthodontics.com/");
            string downloadString = Download("http://blog.bates-orthodontics.com/");

            var doc = new HtmlAgilityPack.HtmlDocument();
            //doc.Load(downloadString);
            doc.LoadHtml(downloadString);

            var content = doc.DocumentNode.SelectNodes("//div[contains(@class,'entry-content')]");
            
            string t = HtmlRemoval.StripTagsRegex(content.FirstOrDefault().InnerHtml);
            byte[] bytes = System.Text.Encoding.Default.GetBytes(t);
            string s = System.Text.Encoding.UTF8.GetString(bytes);
            Regex trimmer = new Regex(@"\s\s+");
            s = trimmer.Replace(s, "  ");
            string trimmed = "";
            if (s.Length >= 176)
            {
                trimmed = s.Substring(0, 176) + "...";
                ViewBag.Content = trimmed;
            }
            else
            {
                ViewBag.Content = s;
            }
            //ViewBag.Content = s.Substring(0, 176) + "...";
            //ViewBag.Content = trimmed;
            string year = DateTime.Now.ToString("yyyy");
            string link = "//a[contains(@href, " + "'http://blog.bates-orthodontics.com/" + year + "')]";
            var anchor = doc.DocumentNode.SelectNodes(link);
            if (anchor == null)
            {
                year = Convert.ToString(Convert.ToInt32(year) - 1);
                link = "//a[contains(@href, " + "'http://blog.bates-orthodontics.com/" + year + "')]";
                anchor = doc.DocumentNode.SelectNodes(link);
            }

            var date = (from d in anchor
                        where d.InnerHtml.Contains("datetime")
                        select d).FirstOrDefault();
            ViewBag.Date = date.InnerHtml;


            var filtered = from f in anchor
                            where f.InnerText.Contains("Comment") ||
                            f.InnerText.Contains("Leave a reply")
                            select f;
            var nonReplyLinks = anchor.Except(filtered);

            var datedLinks = from d in nonReplyLinks
                                where d.InnerText.Contains(year)
                                select d;
            var noDates = nonReplyLinks.Except(datedLinks).FirstOrDefault();

            ViewBag.BlogTitle = noDates.OuterHtml;

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
            if (!String.IsNullOrEmpty(request.Date) && !String.IsNullOrEmpty(request.FirstName) && !String.IsNullOrEmpty(request.LastName)
                && !String.IsNullOrEmpty(request.PhoneNumber) && !String.IsNullOrEmpty(request.Address) && !String.IsNullOrEmpty(request.City) 
                && !String.IsNullOrEmpty(request.Zip) && !String.IsNullOrEmpty(request.PatientTreatmentStatus) && !String.IsNullOrEmpty(request.Organization)
                && !String.IsNullOrEmpty(request.AdSize) && !String.IsNullOrEmpty(request.AdCost) && !String.IsNullOrEmpty(request.ArtworkEmailedTo)
                && !String.IsNullOrEmpty(request.CheckPayableTo))
            {
                string path = Server.MapPath("~/EmailTemplates/SponsorshipRequestEmail.cshtml");
                string template = System.IO.File.OpenText(path).ReadToEnd();
                string message = Razor.Parse(template, request);
                SendGmail mailer = new SendGmail();
                mailer.Subject = "New SponsorshipRequest";
                mailer.Body = message;
                mailer.Send(mailer.Subject, message);
                return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
            }
            return Json(new { result = "Redirect", url = Url.Action("AppointmentRequest", "Home") });            
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
                return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
            }
            return Json(new { result = "Redirect", url = Url.Action("AppointmentRequest", "Home") });
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
                return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
            }
            return Json(new { result = "Redirect", url = Url.Action("Contact", "Home") });
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
               return Json(new { result = "Redirect", url = Url.Action("ThankYou", "Home") });
           }
           return Json(new { result = "Redirect", url = Url.Action("AppointmentRequest", "Home") });
        }

        private static String Download(String url)
        {
            String str = String.Empty;

            try
            {
                System.Net.WebRequest request = System.Net.WebRequest.Create(url);
                System.Net.WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
                str = reader.ReadToEnd();
                reader.Close();
                stream.Flush();
                stream.Close();
                response.Close();
            }
            catch { }

            return str;
        }

        //handle exceptions
        protected override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                Exception ex = filterContext.Exception;


                //do something with these details here
                //need to have separate email utilities for sending myself error emails and legit emails to Sheldon
                SendGmail mailer = new SendGmail();
                mailer.Subject = "New Contact Request";
                mailer.Body = ex.Message + " " + ex.InnerException;
                mailer.Send(mailer.Subject, ex.Message + " " + ex.InnerException);

                RedirectToAction("Error", "Home");
            }
        }

    }

    /// <summary>
    /// Methods to remove HTML from strings.
    /// </summary>
    public static class HtmlRemoval
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
   
}