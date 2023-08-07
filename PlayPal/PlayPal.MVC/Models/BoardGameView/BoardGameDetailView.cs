using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PlayPal.MVC.Models
{
    public class BoardGameDetailView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }     
        public bool IsAvailable { get; set; }
        public string Price { get; set; }
        public string Rating { get; set; }
        public List<ReviewVIEW> Reviews { get; set; }
    }
}