using GigHub.DTO;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDTO Dto)
        {
            var userID = User.Identity.GetUserId();

            if (_context.Following.Any(f => f.FollowerID == userID && f.FolloweeID == Dto.FolloweeID))
                return BadRequest("Already Following");

            var follwing = new Following
            {
                FolloweeID = Dto.FolloweeID,
                FollowerID = userID
            };

            _context.Following.Add(follwing);
            _context.SaveChanges();

            return Ok();
        }
    }
}
