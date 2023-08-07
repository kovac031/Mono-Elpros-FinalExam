using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.ModelDTO.Common
{
    public interface IReview
    {
        Guid Id { get; set; }
        string Title { get; set; }
        string Comment { get; set; }
        bool IsApproved { get; set; }
        int Rating { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        Guid CreatedByUserId { get; set; }
        Guid UpdatedByUserId { get; set; }
        bool IsActive { get; set; }
        Guid CoreUserId { get; set; }
        Guid BoardGameId { get; set; }
    }
}
