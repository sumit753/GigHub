using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System;
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
        [HttpPost]
        public ActionResult Create(GigFormViewModel GigViewModel)
        {
            var ArtistId = User.Identity.GetUserId();
            var Artist = _context.Users.Single(u => u.Id == ArtistId);
            var genre = _context.Genre.Single(m => m.Id == GigViewModel.Genre);
            var gig = new Gig
            {
                Artist = Artist,
                DateTime = DateTime.Parse(string.Format("{0} {1}", GigViewModel.Date, GigViewModel.Time)),
                Venue = GigViewModel.Venue,
                Genre = genre
            };
            _context.Gig.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}