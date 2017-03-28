using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
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

            return RedirectToAction("Index", "Home");
        }
    }
}