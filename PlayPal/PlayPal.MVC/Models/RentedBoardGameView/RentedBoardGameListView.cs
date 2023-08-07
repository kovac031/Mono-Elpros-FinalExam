using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models.RentedBoardGameView
{
    public class RentedBoardGameListView
    {
        public Guid Id { get; set; }
        public string BoardGameTitle { get; set; }
        public string RentedByUsername { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateRented { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateReturned { get; set; }

        public bool Status { get; set; }
    }
}