﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string userID { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }
        public bool isRead { get; private set; }

        public UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("User");

            if (notification == null)
                throw new ArgumentNullException("Notification");

            User = user;
            Notification = notification;

        }

        public void Read()
        {
            isRead = true;
        }
    }
}