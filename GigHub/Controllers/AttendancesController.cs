﻿using GigHub.DTO;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendance.Any(a => a.AttendeeId == userId && a.GigId == dto.gigID))
            {
                return BadRequest("The Attendance Already Exist");
            }
            var attendance = new Attendance
            {
                GigId = dto.gigID,
                AttendeeId = userId
            };
            _context.Attendance.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
