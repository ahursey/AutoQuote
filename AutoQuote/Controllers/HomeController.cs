using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoQuote.Models;
using AutoQuote.ViewModels;
using System.Collections;


namespace AutoQuote.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Quote(string firstName, string lastName, string emailAddress, DateTime DOB, string carYear, string carMake, string carModel, bool DUI, int speedTickets, bool fullCoverage, decimal completedQuote=50)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(carYear) || string.IsNullOrEmpty(carModel) || speedTickets < 0)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (AutoQuotesEntities db = new AutoQuotesEntities())
                {
                    var quote = new QuoteList();

                    quote.firstName = firstName;
                    quote.lastName = lastName;
                    quote.emailAddress = emailAddress;
                    quote.DOB = DOB;
                    quote.carYear = Convert.ToInt16(carYear);
                    quote.carMake = carMake;
                    quote.carModel = carModel;
                    quote.DUI = DUI;
                    quote.speedTickets = Convert.ToInt16(speedTickets);
                    quote.fullCoverage = fullCoverage;

                    int age = DOB.Year;
                    int month = DOB.Month;
                    int day = DOB.Day;

                    if (DateTime.Now.Month > month)
                    {
                        age = age + 1;
                    }
                    else if (DateTime.Now.Month == month && DateTime.Now.Day >= day)
                    {
                        age = age + 1;
                    }

                    if (DateTime.Now.Year - age < 18)
                    {
                        completedQuote = completedQuote + 100;
                    }
                    else if (DateTime.Now.Year - age < 25)
                    {
                        completedQuote = completedQuote + 25;
                    }
                    else if (DateTime.Now.Year - age > 100)
                    {
                        completedQuote = completedQuote + 25;
                    }

                    int year = Convert.ToInt16(carYear);
                    if (year < 2000)
                    {
                        completedQuote = completedQuote + 25;
                    }
                    else if (year > 2015)
                    {
                        completedQuote = completedQuote + 25;
                    }

                    carMake = carMake.ToLower();
                    carModel = carModel.ToLower();
                    if (carMake == "porsche")
                    {
                        completedQuote = completedQuote + 25;
                    }
                    if (carMake == "porsche" && carModel == "911 carrera")
                    {
                        completedQuote = completedQuote + 25;
                    }

                    completedQuote = completedQuote + 10 * speedTickets;

                    if (DUI == true)
                    {
                        completedQuote = (completedQuote * 5) / 4;
                    }

                    if (fullCoverage == true)
                    {
                        completedQuote = (completedQuote * 3) / 2;
                    }

                    quote.completedQuote = Convert.ToInt16(completedQuote);

                    db.QuoteLists.Add(quote);
                    db.SaveChanges();
                }
                return View("Success");
            }

        }
    }
}