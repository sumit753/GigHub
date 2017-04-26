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
                Genres = _context.Genre.ToList(),
                Heading = "Add a Gig"

            };
            return View("GigForm", viewModel);
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
                return View("GigForm", GigViewModel);
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

            var Gigs = _context.Gig.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.isCanceled)
                    .Include(g => g.Genre).ToList();



            return View(Gigs);
        }

        [Authorize]
        public ActionResult Edit(int Id)
        {
            var userID = User.Identity.GetUserId();

            var gig = _context.Gig.Single(g => g.Id == Id && g.ArtistId == userID);

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Genres = _context.Genre.ToList(),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:MM"),
                Heading = "Edit Gig"

            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel GigViewModel)
        {
            var UserID = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                GigViewModel.Genres = _context.Genre.ToList();
                return View("GigForm", GigViewModel);
            }

            var gig = _context.Gig.Include(g => g.Attendances.Select(a => a.Attendee)).Single(g => g.Id == GigViewModel.Id
                          && g.ArtistId == UserID);

            //Modifying the gig
            gig.Modify(GigViewModel.Venue, GigViewModel.getDateTime(), GigViewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gig");
        }

    }
}