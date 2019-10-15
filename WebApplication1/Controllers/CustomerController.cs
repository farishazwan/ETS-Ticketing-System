using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Process;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult About()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult FAQ()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }



        public ActionResult PurchaseTicket()
        {

            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }


        [HttpPost]
        public ActionResult PurchaseTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                IDictionary<int, string> Location = new Dictionary<int, string>() {
                { 0, "Sungai Buloh"},
                { 1, "Kampung Selamat"},
                { 2, "Kwasa Damansara"},
                { 3, "Kwasa Sentral" },
                { 4, "Kota Damansara" },
                { 5, "Surian"},
                { 6, "Mutiara Damansara"},
                { 7, "Bandar Utama" },
                { 8, "Taman Tun Dr Ismail" },
                { 9, "Phileo Damansara"},
                { 10, "Pusat Bandar Damansara"},
                { 11, "Semantan"},
                { 12, "Muzium Negara" },
                { 13, "Pasar Seni" },
                { 14, "Merdeka"},
                { 15, "Bukit Bintang"},
                { 16, "Tun Razak Exchange"},
                { 17, "Cochrane" },
                { 18, "Maluri" },
                { 19, "Taman Pertama"},
                { 20, "Taman Midah"},
                { 21, "Taman Mutiara"},
                { 22, "Taman Connaught" },
                { 23, "Taman Suntex" },
                { 24, "Sri Raya"},
                { 25, "Bandar Tun Hussein Onn"},
                { 26, "Batu Sebelas Cheras"},
                { 27, "Bukit Dukung" },
                { 28, "Sungai Jernih" },
                { 29, "Stadium Kajang" },
                { 30, "Kajang"}
                };

                double[,] fare =
                {
                { 0.80, 1.20, 1.80, 2.00, 2.60, 2.70, 3.10, 3.30, 3.20, 3.50, 3.30, 3.40, 3.10, 3.20, 3.30, 3.40, 3.5, 3.60, 3.70, 3.90, 4.00, 4.10, 4.30, 4.5, 4.60, 4.80, 4.80, 5.00, 5.3, 5.4, 5.5 },
                { 1.20,0.80,1.5,1.80,2.30,2.70,2.80,3.10,3.40,3.30,3.70,3.30,3.70,3.80,3.20,3.30,3.40,4,3.60,3.80,3.90,4.00,4.20,4.40,4.50,4.60,4.70,4.90,5.2,5.2,5.4},
                { 1.80,1.5,0.80,1.1,1.8,2.10,2.60,2.60,3.00,3.20,3.30,3.5,3.40,3.5,3.60,3.70,3.20,3.30,3.40,3.5,3.60,3.80,3.90,4.10,4.30,4.40,4.5,4.60,4.90,5,5.1},
                { 2.00,1.80,1.1,0.80,1.60,1.9,2.30,2.60,2.80,3.00,3.10,3.30,3.80,3.40,3.40,3.60,3.80,3.20,3.30,3.40,3.5,3.70,3.80,4.00,4.10,4.30,4.40,4.5,4.80,4.90,5},
                { 2.60,2.30,1.80,1.60,0.80,1.30,1.80,2.00,2.40,2.80,3.00,3.20,3.30,3.5,3.60,3.20,3.40,3.60,3.70,3.20,3.20,3.40,3.5,3.70,3.90,4.00,4.10,4.30,4.60,4.60,4.80},
                { 2.70,2.70,2.10,1.90,1.30,0.80,1.30,1.70,2.00,2.40,2.70,2.90,3.10,3.30,3.40,3.60,3.80,3.40,3.5,3.70,3.80,3.20,3.40,3.60,3.70,3.90,4.00,4.10,4.40,4.5,4.60},
                { 3.10,2.80,2.60,2.30,1.80,1.30,0.80,1.30,1.70,2.00,2.60,2.80,3.20,3.40,3.10,3.30,3.5,3.70,3.20,3.50,3.60,3.80,3.20,3.40,3.60,3.70,3.80,3.90,4.20,4.30,4.40},
                { 3.30,3.1,2.60,2.60,2.00,1.70,1.30,0.80,1.30,1.70,2.20,2.5,2.90,3.10,3.20,3.40,3.20,3.40,3.60,3.30,3.40,3.60,3.80,3.30,3.40,3.60,3.60,3.80,4.10,4.20,4.30},
                { 3.20,3.40,3.00,2.80,2.40,2.00,1.70,1.30,0.80,1.20,1.80,2.10,2.80,2.80,2.90,3.10,3.40,3.10,3.30,3.60,3.70,3.40,3.60,3.80,3.20,3.40,3.5,3.60,3.90,4.00,4.10},
                { 3.5,3.30,3.20,3.00,2.80,2.40,2.00,1.70,1.20,0.80,1.60,1.80,2.5,2.70,2.60,2.80,3.10,3.30,3.10,3.30,3.5,3.70,3.40,3.60,3.80,3.20,3.30,3.5,3.80,3.90,4.00},
                { 3.30,3.70,3.30,3.10,3.00,2.70,2.60,2.20,1.80,1.60,0.80,1.10,1.80,2.10,2.20,2.50,2.80,2.80,3.00,3.30,3.5,3.30,3.5,3.30,3.50,3.70,3.80,3.20,3.50,3.60,3.70},
                { 3.40,3.30,3.5,3.30,3.20,2.90,2.80,2.5,2.10,1.80,1.10,0.80,1.70,1.90,2.00,2.30,2.60,2.60,2.80,3.10,3.30,3.10,3.40,3.70,3.30,3.5,3.60,3.10,3.40,3.50,3.60},
                { 3.10,3.70,3.40,3.80,3.30,3.10,3.20,2.90,2.80,2.50,1.80,1.70,0.80,1.20,1.30,1.60,1.90,2.10,2.30,2.70,2.70,3.00,3.30,3.20,3.40,3.70,3.20,3.5,3.10,3.20,3.30},
                { 3.20,3.80,3.5,3.40,3.50,3.30,3.40,3.10,2.80,2.70,2.10,1.90,1.20,0.80,1.00,1.30,1.70,1.80,2.10,2.5,2.70,2.80,3.00,3.5,3.30,3.5,3.60,3.30,3.70,3.80,3.20},
                { 3.30,3.20,3.60,3.40,3.60,3.40,3.10,3.20,2.90,2.60,2.20,2.00,1.30,1.00,0.80,1.10,1.5,1.80,1.90,2.30,2.50,2.60,2.90,3.30,3.20,3.40,3.5,3.80,3.60,3.70,3.20},
                { 3.40,3.30,3.70,3.60,3.20,3.60,3.30,3.40,3.10,2.80,2.5,2.30,1.60,1.30,1.10,0.80,1.20,1.5,1.80,2.10,2.30,2.60,2.70,3.10,3.40,3.20,3.30,3.60,3.5,3.60,3.80},
                { 3.50,3.40,3.20,3.80,3.40,3.80,3.50,3.20,3.40,3.10,2.80,2.60,1.90,1.70,1.5,1.20,0.80,1.10,1.40,1.8,1.90,2.30,2.70,2.90,3.10,3.40,3.10,3.40,3.30,3.40,3.60},
                { 3.60,3.5,3.30,3.20,3.60,3.40,3.70,3.40,3.10,3.30,2.80,2.60,2.10,1.5,1.80,1.5,1.10,0.80,1.10,1.5,1.80,2.10,2.40,2.60,2.90,3.20,3.30,3.20,3.70,3.30,3.40},
                { 3.70,3.60,3.40,3.30,3.70,3.5,3.20,3.60,3.30,3.10,3.00,2.80,2.30,2.10,1.90,1.80,1.40,1.10,0.80,1.30,1.5,1.80,2.20,2.70,2.70,3.00,3.20,3.10,3.60,3.70,3.30},
                { 3.90,3.80,3.5,3.40,3.20,3.70,3.50,3.30,3.60,3.30,3.30,3.10,2.70,2.5,2.30,2.10,1.8,1.5,1.30,0.80,1.10,1.5,1.8,2.30,2.60,2.70,2.80,3.20,3.30,3.40,3.60},
                { 4.00,3.90,3.60,3.5,3.20,3.80,3.60,3.40,3.70,3.50,3.5,3.30,2.70,2.70,2.50,2.30,1.90,1.80,1.5,1.10,0.80,1.30,1.70,2.10,2.40,2.80,2.70,3.00,3.10,3.30,3.5},
                { 4.10,4.00,3.80,3.70,3.40,3.20,3.80,3.60,3.40,3.70,3.30,3.10,3.00,2.80,2.60,2.60,2.30,2.10,1.80,1.5,1.30,0.80,1.20,1.80,2.00,2.40,2.60,2.70,3.30,3.40,3.20},
                { 4.30,4.20,3.90,3.80,3.50,3.40,3.20,3.80,3.60,3.40,3.5,3.40,3.30,3.00,2.90,2.70,2.70,2.40,2.20,1.80,1.70,1.20,0.80,1.40,1.80,2.00,2.20,2.60,3.00,3.10,3.40},
                { 4.5,4.40,4.10,4.00,3.70,3.60,3.40,3.30,3.80,3.60,3.30,3.70,3.20,3.5,3.30,3.10,2.90,2.60,2.70,2.30,2.10,1.80,1.40,0.80,1.20,1.60,1.80,2.10,2.60,2.70,3.00},
                { 4.60, 4.5,4.30,4.10,3.90,3.70,3.60,3.40,3.20,3.80,3.5,3.30,3.40,3.30,3.20,3.40,3.10,2.90,2.70,2.60,2.40,2.00,1.80,1.20,0.80,1.30,1.5,1.80,2.5,2.70,2.70},
                { 4.80,4.60,4.40,4.30,4.00,3.90,3.70,3.60,3.40,3.20,3.70,3.5,3.70,3.50,3.40,3.20,3.40,3.20,3.00,2.70,2.80,2.40,2.00,1.60,1.30,0.80,1.10,1.5,2.20,2.30,2.70},
                { 4.80,4.70,4.5,4.40,4.10,4.00,3.80,3.60,3.5,3.30,3.80,3.60,3.20,3.60,3.5,3.30,3.10,3.30,3.20,2.80,2.70,2.60,2.20,1.80,1.5,1.10,0.80,1.30,2.00,2.20,2.5},
                { 5,4.90,4.60,4.5,4.30,4.10,3.90,3.80,3.60,3.5,3.20,3.10,3.5,3.30,3.80,3.60,3.40,3.20,3.10,3.20,3.00,2.70,2.60,2.10,1.80,1.5,1.30,0.80,1.70,1.80,2.10},
                { 5.3,5.2,4.90,4.80,4.60,4.40,4.20,4.10,3.90,3.80,3.5,3.40,3.10,3.70,3.60,3.5,3.30,3.70,3.60,3.30,3.10,3.30,3.00,2.60,2.5,2.20,2.00,1.70,0.80,1.10,1.40},
                { 5.4,5.2,5,4.90,5,4.5,4.30,4.20,4.00,3.90,3.60,3.5,3.20,3.80,3.70,3.60,3.40,3.30,3.70,3.40,3.30,3.40,3.10,2.70,2.70,2.30,2.20,1.80,1.10,0.80,1.20},
                { 5.5,5.4,5.10,5,4.80,4.60,4.40,4.30,4.10,4.00,3.70,3.60,3.30,3.20,3.20,3.80,3.60,3.40,3.30,3.60,3.5,3.20,3.40,3.00,2.70,2.70,2.5,2.10,1.40,1.20,0.80}
                };

                int FromIndex = int.Parse(ticket.FromDestination);
                int ToIndex = int.Parse(ticket.ToDestination);

                ViewBag.FromDestination = Location[FromIndex];
                ViewBag.ToDestination = Location[ToIndex];

                IDictionary<int, string> TypeOfUser = new Dictionary<int, string>()
                {
                    { 1, "Non-privileged"},
                    { 2, "Senior citizen"},
                    { 3, "Student"},
                    { 4, "Disabled"}
                };

                IDictionary<int, string> TypeOfTrip = new Dictionary<int, string>()
                {
                    { 1, "One-Way Trip"},
                    { 2, "Return Trip"}
                };

                double charge = fare[FromIndex, ToIndex];

                int TypeOfUserIndex = int.Parse(ticket.TypeOfUser);
                ViewBag.TypeOfUser = TypeOfUser[TypeOfUserIndex];

                int TypeOfTripIndex = int.Parse(ticket.NumOfWay);
                ViewBag.TypeOfTrip = TypeOfTrip[TypeOfTripIndex];

                int NumOfTicket = ticket.NumOfTicket;

                double totalCharge;

                if (TypeOfUserIndex == 1)
                {
                    if (TypeOfTripIndex == 1)
                    {
                        totalCharge = charge * 1 * NumOfTicket;
                        ticket.TotalPrice = totalCharge;
                    }
                    else
                    {
                        totalCharge = charge * 1 * NumOfTicket * 2;
                        ticket.TotalPrice = totalCharge;
                    }

                }
                else if (TypeOfUserIndex == 2)
                {
                    if (TypeOfTripIndex == 1)
                    {
                        totalCharge = charge * 0.5 * NumOfTicket;
                        ticket.TotalPrice = totalCharge;
                    }
                    else
                    {
                        totalCharge = charge * 0.5 * NumOfTicket * 2;
                        ticket.TotalPrice = totalCharge;
                    }
                }
                else if (TypeOfUserIndex == 3)
                {
                    if (TypeOfTripIndex == 1)
                    {
                        totalCharge = charge * 0.6 * NumOfTicket;
                        ticket.TotalPrice = totalCharge;
                    }
                    else
                    {
                        totalCharge = charge * 0.6 * NumOfTicket * 2;
                        ticket.TotalPrice = totalCharge;
                    }
                }
                else if (TypeOfUserIndex == 4)
                {
                    if (TypeOfTripIndex == 1)
                    {
                        totalCharge = charge * 0.4 * NumOfTicket;
                        ticket.TotalPrice = totalCharge;
                    }
                    else
                    {
                        totalCharge = charge * 0.4 * NumOfTicket * 2;
                        ticket.TotalPrice = totalCharge;
                    }
                }
                else
                {
                    ViewBag.MessageError("Please select user type");
                }

                return View("Confirmation", ticket);
            }
            else
            {
                return View();
            }

        }

        public ActionResult Confirmation()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        public ActionResult Confirmation(Ticket ticket)
        {

            return View("Payment", ticket);
        }

        public ActionResult Payment()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Payment(Ticket ticket)
        {
            IDictionary<int, string> Location = new Dictionary<int, string>() {
                { 0, "Sungai Buloh"},
                { 1, "Kampung Selamat"},
                { 2, "Kwasa Damansara"},
                { 3, "Kwasa Sentral" },
                { 4, "Kota Damansara" },
                { 5, "Surian"},
                { 6, "Mutiara Damansara"},
                { 7, "Bandar Utama" },
                { 8, "Taman Tun Dr Ismail" },
                { 9, "Phileo Damansara"},
                { 10, "Pusat Bandar Damansara"},
                { 11, "Semantan"},
                { 12, "Muzium Negara" },
                { 13, "Pasar Seni" },
                { 14, "Merdeka"},
                { 15, "Bukit Bintang"},
                { 16, "Tun Razak Exchange"},
                { 17, "Cochrane" },
                { 18, "Maluri" },
                { 19, "Taman Pertama"},
                { 20, "Taman Midah"},
                { 21, "Taman Mutiara"},
                { 22, "Taman Connaught" },
                { 23, "Taman Suntex" },
                { 24, "Sri Raya"},
                { 25, "Bandar Tun Hussein Onn"},
                { 26, "Batu Sebelas Cheras"},
                { 27, "Bukit Dukung" },
                { 28, "Sungai Jernih" },
                { 29, "Stadium Kajang" },
                { 30, "Kajang"}
                };

            double[,] fare =
            {
                { 0.80, 1.20, 1.80, 2.00, 2.60, 2.70, 3.10, 3.30, 3.20, 3.50, 3.30, 3.40, 3.10, 3.20, 3.30, 3.40, 3.5, 3.60, 3.70, 3.90, 4.00, 4.10, 4.30, 4.5, 4.60, 4.80, 4.80, 5.00, 5.3, 5.4, 5.5 },
                { 1.20,0.80,1.5,1.80,2.30,2.70,2.80,3.10,3.40,3.30,3.70,3.30,3.70,3.80,3.20,3.30,3.40,4,3.60,3.80,3.90,4.00,4.20,4.40,4.50,4.60,4.70,4.90,5.2,5.2,5.4},
                { 1.80,1.5,0.80,1.1,1.8,2.10,2.60,2.60,3.00,3.20,3.30,3.5,3.40,3.5,3.60,3.70,3.20,3.30,3.40,3.5,3.60,3.80,3.90,4.10,4.30,4.40,4.5,4.60,4.90,5,5.1},
                { 2.00,1.80,1.1,0.80,1.60,1.9,2.30,2.60,2.80,3.00,3.10,3.30,3.80,3.40,3.40,3.60,3.80,3.20,3.30,3.40,3.5,3.70,3.80,4.00,4.10,4.30,4.40,4.5,4.80,4.90,5},
                { 2.60,2.30,1.80,1.60,0.80,1.30,1.80,2.00,2.40,2.80,3.00,3.20,3.30,3.5,3.60,3.20,3.40,3.60,3.70,3.20,3.20,3.40,3.5,3.70,3.90,4.00,4.10,4.30,4.60,4.60,4.80},
                { 2.70,2.70,2.10,1.90,1.30,0.80,1.30,1.70,2.00,2.40,2.70,2.90,3.10,3.30,3.40,3.60,3.80,3.40,3.5,3.70,3.80,3.20,3.40,3.60,3.70,3.90,4.00,4.10,4.40,4.5,4.60},
                { 3.10,2.80,2.60,2.30,1.80,1.30,0.80,1.30,1.70,2.00,2.60,2.80,3.20,3.40,3.10,3.30,3.5,3.70,3.20,3.50,3.60,3.80,3.20,3.40,3.60,3.70,3.80,3.90,4.20,4.30,4.40},
                { 3.30,3.1,2.60,2.60,2.00,1.70,1.30,0.80,1.30,1.70,2.20,2.5,2.90,3.10,3.20,3.40,3.20,3.40,3.60,3.30,3.40,3.60,3.80,3.30,3.40,3.60,3.60,3.80,4.10,4.20,4.30},
                { 3.20,3.40,3.00,2.80,2.40,2.00,1.70,1.30,0.80,1.20,1.80,2.10,2.80,2.80,2.90,3.10,3.40,3.10,3.30,3.60,3.70,3.40,3.60,3.80,3.20,3.40,3.5,3.60,3.90,4.00,4.10},
                { 3.5,3.30,3.20,3.00,2.80,2.40,2.00,1.70,1.20,0.80,1.60,1.80,2.5,2.70,2.60,2.80,3.10,3.30,3.10,3.30,3.5,3.70,3.40,3.60,3.80,3.20,3.30,3.5,3.80,3.90,4.00},
                { 3.30,3.70,3.30,3.10,3.00,2.70,2.60,2.20,1.80,1.60,0.80,1.10,1.80,2.10,2.20,2.50,2.80,2.80,3.00,3.30,3.5,3.30,3.5,3.30,3.50,3.70,3.80,3.20,3.50,3.60,3.70},
                { 3.40,3.30,3.5,3.30,3.20,2.90,2.80,2.5,2.10,1.80,1.10,0.80,1.70,1.90,2.00,2.30,2.60,2.60,2.80,3.10,3.30,3.10,3.40,3.70,3.30,3.5,3.60,3.10,3.40,3.50,3.60},
                { 3.10,3.70,3.40,3.80,3.30,3.10,3.20,2.90,2.80,2.50,1.80,1.70,0.80,1.20,1.30,1.60,1.90,2.10,2.30,2.70,2.70,3.00,3.30,3.20,3.40,3.70,3.20,3.5,3.10,3.20,3.30},
                { 3.20,3.80,3.5,3.40,3.50,3.30,3.40,3.10,2.80,2.70,2.10,1.90,1.20,0.80,1.00,1.30,1.70,1.80,2.10,2.5,2.70,2.80,3.00,3.5,3.30,3.5,3.60,3.30,3.70,3.80,3.20},
                { 3.30,3.20,3.60,3.40,3.60,3.40,3.10,3.20,2.90,2.60,2.20,2.00,1.30,1.00,0.80,1.10,1.5,1.80,1.90,2.30,2.50,2.60,2.90,3.30,3.20,3.40,3.5,3.80,3.60,3.70,3.20},
                { 3.40,3.30,3.70,3.60,3.20,3.60,3.30,3.40,3.10,2.80,2.5,2.30,1.60,1.30,1.10,0.80,1.20,1.5,1.80,2.10,2.30,2.60,2.70,3.10,3.40,3.20,3.30,3.60,3.5,3.60,3.80},
                { 3.50,3.40,3.20,3.80,3.40,3.80,3.50,3.20,3.40,3.10,2.80,2.60,1.90,1.70,1.5,1.20,0.80,1.10,1.40,1.8,1.90,2.30,2.70,2.90,3.10,3.40,3.10,3.40,3.30,3.40,3.60},
                { 3.60,3.5,3.30,3.20,3.60,3.40,3.70,3.40,3.10,3.30,2.80,2.60,2.10,1.5,1.80,1.5,1.10,0.80,1.10,1.5,1.80,2.10,2.40,2.60,2.90,3.20,3.30,3.20,3.70,3.30,3.40},
                { 3.70,3.60,3.40,3.30,3.70,3.5,3.20,3.60,3.30,3.10,3.00,2.80,2.30,2.10,1.90,1.80,1.40,1.10,0.80,1.30,1.5,1.80,2.20,2.70,2.70,3.00,3.20,3.10,3.60,3.70,3.30},
                { 3.90,3.80,3.5,3.40,3.20,3.70,3.50,3.30,3.60,3.30,3.30,3.10,2.70,2.5,2.30,2.10,1.8,1.5,1.30,0.80,1.10,1.5,1.8,2.30,2.60,2.70,2.80,3.20,3.30,3.40,3.60},
                { 4.00,3.90,3.60,3.5,3.20,3.80,3.60,3.40,3.70,3.50,3.5,3.30,2.70,2.70,2.50,2.30,1.90,1.80,1.5,1.10,0.80,1.30,1.70,2.10,2.40,2.80,2.70,3.00,3.10,3.30,3.5},
                { 4.10,4.00,3.80,3.70,3.40,3.20,3.80,3.60,3.40,3.70,3.30,3.10,3.00,2.80,2.60,2.60,2.30,2.10,1.80,1.5,1.30,0.80,1.20,1.80,2.00,2.40,2.60,2.70,3.30,3.40,3.20},
                { 4.30,4.20,3.90,3.80,3.50,3.40,3.20,3.80,3.60,3.40,3.5,3.40,3.30,3.00,2.90,2.70,2.70,2.40,2.20,1.80,1.70,1.20,0.80,1.40,1.80,2.00,2.20,2.60,3.00,3.10,3.40},
                { 4.5,4.40,4.10,4.00,3.70,3.60,3.40,3.30,3.80,3.60,3.30,3.70,3.20,3.5,3.30,3.10,2.90,2.60,2.70,2.30,2.10,1.80,1.40,0.80,1.20,1.60,1.80,2.10,2.60,2.70,3.00},
                { 4.60, 4.5,4.30,4.10,3.90,3.70,3.60,3.40,3.20,3.80,3.5,3.30,3.40,3.30,3.20,3.40,3.10,2.90,2.70,2.60,2.40,2.00,1.80,1.20,0.80,1.30,1.5,1.80,2.5,2.70,2.70},
                { 4.80,4.60,4.40,4.30,4.00,3.90,3.70,3.60,3.40,3.20,3.70,3.5,3.70,3.50,3.40,3.20,3.40,3.20,3.00,2.70,2.80,2.40,2.00,1.60,1.30,0.80,1.10,1.5,2.20,2.30,2.70},
                { 4.80,4.70,4.5,4.40,4.10,4.00,3.80,3.60,3.5,3.30,3.80,3.60,3.20,3.60,3.5,3.30,3.10,3.30,3.20,2.80,2.70,2.60,2.20,1.80,1.5,1.10,0.80,1.30,2.00,2.20,2.5},
                { 5,4.90,4.60,4.5,4.30,4.10,3.90,3.80,3.60,3.5,3.20,3.10,3.5,3.30,3.80,3.60,3.40,3.20,3.10,3.20,3.00,2.70,2.60,2.10,1.80,1.5,1.30,0.80,1.70,1.80,2.10},
                { 5.3,5.2,4.90,4.80,4.60,4.40,4.20,4.10,3.90,3.80,3.5,3.40,3.10,3.70,3.60,3.5,3.30,3.70,3.60,3.30,3.10,3.30,3.00,2.60,2.5,2.20,2.00,1.70,0.80,1.10,1.40},
                { 5.4,5.2,5,4.90,5,4.5,4.30,4.20,4.00,3.90,3.60,3.5,3.20,3.80,3.70,3.60,3.40,3.30,3.70,3.40,3.30,3.40,3.10,2.70,2.70,2.30,2.20,1.80,1.10,0.80,1.20},
                { 5.5,5.4,5.10,5,4.80,4.60,4.40,4.30,4.10,4.00,3.70,3.60,3.30,3.20,3.20,3.80,3.60,3.40,3.30,3.60,3.5,3.20,3.40,3.00,2.70,2.70,2.5,2.10,1.40,1.20,0.80}
                };

            int FromIndex = int.Parse(ticket.FromDestination);
            int ToIndex = int.Parse(ticket.ToDestination);

            ViewBag.FromDestination = Location[FromIndex];
            ViewBag.ToDestination = Location[ToIndex];

            IDictionary<int, string> TypeOfUser = new Dictionary<int, string>()
                {
                    { 1, "Non-privileged"},
                    { 2, "Senior citizen"},
                    { 3, "Student"},
                    { 4, "Disabled"}
                };

            IDictionary<int, string> TypeOfTrip = new Dictionary<int, string>()
                {
                    { 1, "One-Way Trip"},
                    { 2, "Return Trip"}
                };

            double charge = fare[FromIndex, ToIndex];

            int TypeOfUserIndex = int.Parse(ticket.TypeOfUser);
            ViewBag.TypeOfUser = TypeOfUser[TypeOfUserIndex];

            int TypeOfTripIndex = int.Parse(ticket.NumOfWay);
            ViewBag.TypeOfTrip = TypeOfTrip[TypeOfTripIndex];

            ticket.DatePurchased = DateTime.Now;


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("spInsertTicketDetail", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd2 = new SqlCommand("SELECT Id from UserAccounts WHERE UserAccounts.EmailAddress='" + System.Web.HttpContext.Current.Session["username"] + "'  ", conn);
            // string userID=ConvertCommandParamatersToLiteralValues(cmd2);
            Int32 ticketUses = ((Int32?)cmd2.ExecuteScalar()) ?? 0;
            ticket.UserId = ticketUses;

            conn.Close();

            cmd.Parameters.AddWithValue("@Name", ticket.Name);
            cmd.Parameters.AddWithValue("@ICNum", ticket.ICNum);
            cmd.Parameters.AddWithValue("@EmailAddress", ticket.EmailAddress);
            cmd.Parameters.AddWithValue("@Origin", ViewBag.FromDestination);
            cmd.Parameters.AddWithValue("@Destination", ViewBag.ToDestination);
            cmd.Parameters.AddWithValue("@TypeOfUser", ViewBag.TypeOfUser);
            cmd.Parameters.AddWithValue("@NumOfTicket", ticket.NumOfTicket);
            cmd.Parameters.AddWithValue("@NumOfWay", ViewBag.TypeOfTrip);
            cmd.Parameters.AddWithValue("@TotalPrice", ticket.TotalPrice);
            cmd.Parameters.AddWithValue("@DatePurchased", ticket.DatePurchased);
            cmd.Parameters.AddWithValue("@UserId", ticket.UserId);


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                ViewBag.MessageError(e.Message.ToString(), "Error Message");
                return View("Index");
            }
            finally
            {
                conn.Close();
            }
            DbAccess db = new DbAccess();
            IEnumerable<Ticket> dbItem = db.GetDbTickets();
            return View(ticket);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult History(UserAccount user)
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {

                DbAccess db = new DbAccess();
                IEnumerable<Ticket> dbItem = db.GetDbTickets();
                return View(dbItem);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }


        public ActionResult ViewProfile()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);

                IList<User> dbUser = new List<User>();

                conn.Open();

                SqlCommand cmd2 = new SqlCommand("SELECT Id from UserAccounts WHERE UserAccounts.EmailAddress='" + System.Web.HttpContext.Current.Session["username"] + "'  ", conn);
                // string userID=ConvertCommandParamatersToLiteralValues(cmd2);
                Int32 ticketUses = ((Int32?)cmd2.ExecuteScalar()) ?? 0;

                string sql = @"SELECT * FROM Users WHERE Users.UserId='" + ticketUses + "'";
                SqlCommand cmd1 = new SqlCommand(sql, conn);

                Int32 result = ((Int32?)cmd1.ExecuteScalar()) ?? 0;

                if (result == 0)
                {
                    return RedirectToAction("CreateProfile", "Customer");
                }
                else
                {
                    DbAccessUsers db = new DbAccessUsers();
                    IEnumerable<User> dbItem = db.GetDbUser();

                    return View(dbItem);
                }

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult CreateProfile()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreateProfile(User user)
        {
            if (ModelState.IsValid)
            {

                if (HttpContext.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Request.Files[0];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var fileSavePath = (HttpContext.Server.MapPath("~/ProfileImg") + httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(@"\")));

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);

                        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStrMrtTicketing"].ConnectionString);
                        SqlCommand cmd = new SqlCommand("spInsertUserDetail", conn);

                        SqlCommand cmd2 = new SqlCommand("SELECT Id from UserAccounts WHERE UserAccounts.EmailAddress='" + System.Web.HttpContext.Current.Session["username"] + "'  ", conn);
                        // string userID=ConvertCommandParamatersToLiteralValues(cmd2);
                        conn.Open();
                        Int32 ticketUses = ((Int32?)cmd2.ExecuteScalar()) ?? 0;

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@ICNum", user.ICNum);
                        cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
                        cmd.Parameters.AddWithValue("@Address", user.Address);
                        cmd.Parameters.AddWithValue("@profileImg", httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(@"\")));
                        cmd.Parameters.AddWithValue("@UserId", ticketUses);
                        conn.Close();

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch(SqlException ex)
                        {
                            ViewBag.MessageError(ex.Message.ToString(), "Error Message");
                            return View();
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                return RedirectToAction("ViewProfile","Customer");
            }

            //Model state
            else
            {
                return View();
            }
        }


        public ActionResult EditProfile()
        {
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(int id, User user)
        {

            if (ModelState.IsValid)
            {

                /* if (imageName != null)
                 {
                     string path = Path.Combine(Server.MapPath("~/ProfileImg"), Path.GetFileName(imageName.FileName));
                     imageName.SaveAs(path);

                 }*/

                if (HttpContext.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Request.Files[0];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var fileSavePath = (HttpContext.Server.MapPath("~/ProfileImg") + httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(@"\")));

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);

                        SqlConnection conn = new SqlConnection(ConfigurationManager.
                        ConnectionStrings["connStrMrtTicketing"].ConnectionString);
                        SqlCommand cmd = new SqlCommand("spUpdateUserDetail", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProfileId", id);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@ICNum", user.ICNum);
                        cmd.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
                        cmd.Parameters.AddWithValue("@Address", user.Address);
                        cmd.Parameters.AddWithValue("@profileImg", httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(@"\")));

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            ViewBag.MessageError(ex.Message.ToString(), "Error Message");
                            return View();
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                }
                return RedirectToAction("ViewProfile", "Customer");
            }
            else
            {
                return View();
            }

        }


        //end of controller
    }
}






