using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models.BoardGameView
{
    public class BoardGameDeleteView
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public bool IsActive { get; set; }
        public Guid UpdatedByUserId { get; set; }
    }
}