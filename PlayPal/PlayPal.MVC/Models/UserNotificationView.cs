using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models
{
    public class UserNotificationView
    {
        public Guid Id { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationMessage { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public bool IsRead { get; set; } 
    }
}