﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificatinType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        public Notification()
        {

        }
        private Notification(Gig gig, NotificatinType type)
        {
            if (gig == null)
                throw new ArgumentNullException("Gig");

            DateTime = DateTime.Now;

            Gig = gig;

            Type = type;


        }

        //factory methods

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificatinType.GigCreated);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {

            var notification = new Notification(newGig, NotificatinType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;
            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificatinType.GigCanceled);

        }
    }
}