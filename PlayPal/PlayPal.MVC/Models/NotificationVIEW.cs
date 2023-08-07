using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models
{
    public class NotificationVIEW
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateUpdated { get; set; }

        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }
        public Guid SelectedRole { get; set; }
        public List<RoleView> AllRoles { get; set; }

     
    }
}