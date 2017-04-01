using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
namespace GigHub.Controllers
{
    public class GigController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GigController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genre.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendance
                                    .Where(a => a.AttendeeId == userId)
                                    .Select(a => a.Gig)
                                    .Include(a => a.Genre)
                                    .Include(a => a.Artist);

            var viewModel = new GigsViewModel
            {
                UpcommingGigs = gigs,
                showActions = false,
                Heading = "Attending"

            };
            return View("Gigs", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel GigViewModel)
        {
            if (!ModelState.IsValid)
            {
                GigViewModel.Genres = _context.Genre.ToList();
                return View("Create", GigViewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = GigViewModel.getDateTime(),
                Venue = GigViewModel.Venue,
                GenreId = GigViewModel.Genre
            };
            _context.Gig.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gig");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var Gigs = _context.Gig.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now).Include(g => g.Genre).ToList();



            return View(Gigs);
        }
    }
}