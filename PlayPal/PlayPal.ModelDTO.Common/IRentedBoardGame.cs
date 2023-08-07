using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.ModelDTO.Common
{
    public interface IRentedBoardGame
    {
        Guid Id { get; set; }
        Guid CoreUserId { get; set; }
        Guid BoardGameId { get; set; }
        Guid CreatedByUserId { get; set; }
        Guid UpdatedByUserId { get; set; }
        DateTime DateRented { get; set; }
        DateTime DateReturned { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        bool IsActive { get; set; }
        string BoardGameTitle { get; set; }
        string Username { get; set; }
    }
}
