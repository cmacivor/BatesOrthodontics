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
            //Console.WriteLine(s);

            ViewBag.Content = s.Substring(0, 176) + "...";


            //ViewBag.Test = "Woooo";
            //string content = GetBlogContentString();

            //WebClient client = new WebClient();
            //string downloadString = client.DownloadString("http://blog.bates-orthodontics.com/");

            var anchor = doc.DocumentNode.SelectNodes("//a[contains(@href, 'http://blog.bates-orthodontics.com/2014')]");

            var filtered = from f in anchor
                           where f.InnerText.Contains("Comment") ||
                           f.InnerText.Contains("Leave a reply")
                           select f;
            var nonReplyLinks = anchor.Except(filtered);

            var datedLinks = from d in nonReplyLinks
                             where d.InnerText.Contains("2014")
                             select d;
            var noDates = nonReplyLinks.Except(datedLinks).FirstOrDefault();
            //StringBuilder sb = new StringBuilder();
            //sb.Append(noDates.OuterHtml);
            ViewBag.BlogTitle = noDates.OuterHtml;
            //string content = noDates.ParentNode.ParentNode.ParentNode.OuterHtml;
            //var content = noDates.ParentNode.ParentNode.ParentNode.ParentNode.OuterHtml;
            
            
            //need to get the content and cut it off at 180 characters

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

        public ActionResult Blog()
        {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString("http://blog.bates-orthodontics.com/");

            var doc = new HtmlAgilityPack.HtmlDocument();
            //doc.Load(downloadString);
            doc.LoadHtml(downloadString);
            var anchor = doc.DocumentNode.SelectNodes("//a[contains(@href, 'http://blog.bates-orthodontics.com/2014')]");

            var filtered = from f in anchor
                           where f.InnerText.Contains("Comment") ||
                           f.InnerText.Contains("Leave a reply")
                           select f;
            var nonReplyLinks = anchor.Except(filtered);

            var datedLinks = from d in nonReplyLinks
                             where d.InnerText.Contains("2014")
                             select d;
            var noDates = nonReplyLinks.Except(datedLinks);
            StringBuilder sb = new StringBuilder();
            
            foreach (var item in noDates)
            {
                //Console.WriteLine(item.ParentNode.InnerHtml);
                //Console.WriteLine(item.OuterHtml);
                //string test = item.OuterHtml;
                sb.Append(item.OuterHtml);
                sb.Append("</br>");
            }

            //ViewBag.Message = "This is a test";
            //return View();
            //return sb.ToString();
            return Content(sb.ToString());
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

        //[HttpGet]
        //public string GetBlogPost()
        //{
        //   //string content 
        //}

        public string GetBlogContentString()
        {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString("http://blog.bates-orthodontics.com/");

            var doc = new HtmlAgilityPack.HtmlDocument();
            //doc.Load(downloadString);
            doc.LoadHtml(downloadString);
            var anchor = doc.DocumentNode.SelectNodes("//a[contains(@href, 'http://blog.bates-orthodontics.com/2014')]");

            var filtered = from f in anchor
                           where f.InnerText.Contains("Comment") ||
                           f.InnerText.Contains("Leave a reply")
                           select f;
            var nonReplyLinks = anchor.Except(filtered);

            var datedLinks = from d in nonReplyLinks
                             where d.InnerText.Contains("2014")
                             select d;
            var noDates = nonReplyLinks.Except(datedLinks).FirstOrDefault();
            StringBuilder sb = new StringBuilder();
            sb.Append(noDates.OuterHtml);
            //foreach (var item in noDates)
            //{
            //    //Console.WriteLine(item.ParentNode.InnerHtml);
            //    //Console.WriteLine(item.OuterHtml);
            //    //string test = item.OuterHtml;
            //    sb.Append(item.OuterHtml);
            //    sb.Append("</br>");
            //}
            return sb.ToString();
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


        //string ReadTextFromUrl(string url)
        //{
        //    // WebClient is still convenient
        //    // Assume UTF8, but detect BOM - could also honor response charset I suppose
        //    using (var client = new WebClient())
        //    using (var stream = client.OpenRead(url))
        //    using (var textReader = new StreamReader(stream, Encoding., true))
        //    {
        //        return textReader.ReadToEnd();
        //    }
        //}

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