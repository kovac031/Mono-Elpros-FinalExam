using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service.Common
{
    public interface IReviewService
    {
        Task<List<ReviewDTO>> GetAllReviewsAsync(Guid? id, Guid? filterByBoardGame);
        Task<ReviewDTO> GetOneReviewAsync(Guid id);
        Task<ReviewDTO> EditReviewAsync(Guid id, ReviewDTO review);
        Task<ReviewDTO> ApproveReviewAsync(Guid id, ReviewDTO reviewDTO);
        Task<ReviewDTO> HideDeleteReviewAsync(Guid id, ReviewDTO reviewDTO);
        Task<ReviewDTO> AddNewReviewAsync(Guid id, ReviewDTO reviewDTO);
    }
}
