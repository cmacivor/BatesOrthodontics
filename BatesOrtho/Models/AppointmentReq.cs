using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BatesOrtho.Models
{
    public class AppointmentReq
    {
        public int AppointmentRequestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DOB { get; set; }
        public string ResponsiblePartyFirstName { get; set; }
        public string ResponsiblePartyLastName { get; set; }
        public string IsNewPatient { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PreferredModeOfContact { get; set; }
        public string ConvenientTimes { get; set; }
        public string HowDidYouHear { get; set; }
        public string GeneralDentistName { get; set; }
        public string AdditionalComments { get; set; }
        public string[] PreferredDays { get; set; }
    }
}