using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models
{
    public class CategoryView
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public System.Guid CreatedByUserId { get; set; }
        public System.Guid UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }
    }
}   