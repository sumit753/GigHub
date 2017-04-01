﻿using GigHub.Models;
using GigHub.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcommingGigs = _context.Gig
                                .Include(a => a.Artist)
                                .Include(a => a.Genre)
                                .Where(a => a.DateTime > DateTime.Now);
            var viewModel = new GigsViewModel
            {
                UpcommingGigs = upcommingGigs,
                showActions = User.Identity.IsAuthenticated,
                Heading = "Upcomming Gigs"
            };

            return View("Gigs", viewModel);
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
    }
}