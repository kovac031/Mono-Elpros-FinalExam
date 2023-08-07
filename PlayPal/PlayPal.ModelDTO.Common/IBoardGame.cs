using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.ModelDTO.Common
{
    public interface IBoardGame
    {
        string Title { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        int NumberOfCopies { get; set; }
        string CategoryName { get; set; }
        bool IsActive { get; set; }
        double? Rating { get; set; }
        Guid CategoryId { get; set; }
        int RentedGameNumber { get; set; }
        int AvailableForRent { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        Guid CreatedByUserId { get; set; }
        Guid UpdatedByUserId { get; set; }
    }
}
