using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BatesOrtho.Models
{
    public class DoctorRef
    {
        public int DoctorReferralID { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string PracticeName { get; set; }
        public string DoctorEmail { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientEmailAddress { get; set; }
        public string RadiographsSent { get; set; }
        public string Comments { get; set; }
    }
}