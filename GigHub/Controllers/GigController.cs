using GigHub.Models;
using GigHub.ViewModel;
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

        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genre.ToList()
            };
            return View(viewModel);
        }
    }
}