using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BatesOrtho
{
    public class Sponsorship
    {
        public int SponsorshipRequestID { get; set; }
        public System.DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PatientTreatmentStatus { get; set; }
        public string Organization { get; set; }
        public string CheckPayableTo { get; set; }
        public string SendCheckToAddress { get; set; }
        public string SendCheckToAddress2 { get; set; }
        public string SendCheckToCity { get; set; }
        public string SendCheckToState { get; set; }
        public string SendCheckToZip { get; set; }
        public string Comments { get; set; }
        public string[] AdTypes { get; set; }
    }
}