using PlayPal.ModelDTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.ModelDTO
{
    public class ReviewDTO : IReview
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public bool IsActive { get; set; }
        public Guid CoreUserId { get; set; }
        public Guid BoardGameId { get; set; }
    }
}
