//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BatesOrtho
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppointmentRequest
    {
        public int AppointmentRequestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DOB { get; set; }
        public string ResponsiblePartyFirstName { get; set; }
        public string ResponsiblePartyLastName { get; set; }
        public bool IsNewPatient { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PreferredModeOfContact { get; set; }
        public string ConvenientTimes { get; set; }
        public string HowDidYouHear { get; set; }
        public string GeneralDentistName { get; set; }
        public string AdditionalComments { get; set; }
    }
}
