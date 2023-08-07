using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models.RentedBoardGameView
{
    public class RentedBoardGameReturnView
    {
        public Guid RentId { get; set; }
        public bool Returned { get; set; }
    }
}