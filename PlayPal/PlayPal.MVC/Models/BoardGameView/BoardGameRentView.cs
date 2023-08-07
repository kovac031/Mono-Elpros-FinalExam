using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models.BoardGameView
{
    public class BoardGameRentView
    {
        public Guid BoardGameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }
        public string Price { get; set; }
        public string Rating { get; set; }
    }
}