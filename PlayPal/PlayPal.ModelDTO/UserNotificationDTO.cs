using PlayPal.ModelDTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.ModelDTO
{
    public class UserNotificationDTO : IUserNotification
    {
        public Guid Id { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationContent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsRead { get; set; }
        public Guid CoreUserId { get; set; }
        public Guid NotificationId { get; set; }
    }
}
