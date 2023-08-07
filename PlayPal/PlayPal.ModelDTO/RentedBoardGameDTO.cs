using PlayPal.ModelDTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.ModelDTO
{
    public class RentedBoardGameDTO : IRentedBoardGame
    {
        public Guid Id { get; set ; }
        public Guid CoreUserId { get; set ; }
        public Guid BoardGameId { get; set ; }
        public Guid CreatedByUserId { get; set ; }
        public Guid UpdatedByUserId { get; set ; }
        public DateTime DateRented { get; set ; }
        public DateTime DateReturned { get; set ; }
        public DateTime DateCreated { get; set ; }
        public DateTime DateUpdated { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsActive { get; set; }
        public string BoardGameTitle { get; set; }
        public string Username { get ; set; }
    }
}
