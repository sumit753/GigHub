using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModel
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcommingGigs { get; set; }
        public bool showActions { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
    }
}