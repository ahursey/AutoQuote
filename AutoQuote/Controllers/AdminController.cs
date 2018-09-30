using AutoQuote.Models;
using AutoQuote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AutoQuote.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (AutoQuotesEntities db = new AutoQuotesEntities())
            {
                var quotes = db.QuoteLists.ToList();
                var quotesVms = new List<QuoteVM>();
                foreach (var quote in quotes)
                {
                    var quoteVm = new QuoteVM();
                    quoteVm.firstName = quote.firstName;
                    quoteVm.lastName = quote.lastName;
                    quoteVm.emailAddress = quote.emailAddress;
                    quoteVm.completedQuote = quote.completedQuote;
                    quotesVms.Add(quoteVm);
                }
                return View(quotesVms);
            }
        }



    }
}
