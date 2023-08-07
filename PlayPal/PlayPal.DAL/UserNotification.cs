namespace PlayPal.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserNotification")]
    public partial class UserNotification
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }

        public bool IsActive { get; set; }

        public bool IsRead { get; set; }

        public Guid CoreUserId { get; set; }

        public Guid NotificationId { get; set; }

        public virtual CoreUser CoreUser { get; set; }

        public virtual CoreUser CoreUser1 { get; set; }

        public virtual CoreUser CoreUser2 { get; set; }

        public virtual Notification Notification { get; set; }
    }
}
