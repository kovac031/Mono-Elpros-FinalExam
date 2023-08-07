using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models
{
    public class BoardGameListView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public Guid SelectedCategory { get; set; }
        public List<CategoryView> Categories { get; set; }
        public string Rating { get; set; }
        public string Availability { get; set; }
        public decimal Price { get; set; }
    }
}