using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigController : ApiController
    {
        private ApplicationDbContext _context;

        public GigController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gig.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.isCanceled)
                return NotFound();

            gig.isCanceled = true;
            _context.SaveChanges();
            return Ok();
        }
    }
}
