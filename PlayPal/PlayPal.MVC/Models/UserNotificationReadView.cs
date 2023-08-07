using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models
{
    public class UserNotificationReadView
    {
        public Guid UserNotificationId { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}