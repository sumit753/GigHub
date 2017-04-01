using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Followees
        [Authorize]
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();

            var artist = _context.Following
                        .Where(a => a.FollowerID == userID)
                        .Select(a => a.Followee).ToList();



            return View(artist);
        }
    }
}