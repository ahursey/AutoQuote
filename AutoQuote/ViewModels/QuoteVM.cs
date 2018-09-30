using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoQuote.ViewModels
{
    public class QuoteVM
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public DateTime DOB { get; set; }
        public string carYear { get; set; }
        public string carMake { get; set; }
        public string carModel { get; set; }
        public int speedTickets { get; set; }
        public bool DUI { get; set; }
        public bool fullCoverage { get; set; }
        public decimal completedQuote { get; set; }
    }
}