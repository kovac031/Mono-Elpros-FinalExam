using PlayPal.ModelDTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.ModelDTO
{
    public class BoardGameDTO : IBoardGame
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double? Rating { get; set; }
        public int NumberOfCopies { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int RentedGameNumber { get; set; }
        public int AvailableForRent { get; set; }
        public bool IsActive { get; set; }
        public DateTime  DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
    }
}
