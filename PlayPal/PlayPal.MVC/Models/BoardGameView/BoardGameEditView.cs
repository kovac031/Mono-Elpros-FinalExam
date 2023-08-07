using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models.BoardGameView
{
    public class BoardGameEditView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid SelectedCategory { get; set; }
        public List<CategoryView> Categories { get; set; } 
    }
}