using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service
{
    public class ReviewService : IReviewService
    {
        protected IReviewRepository Repository { get; set; }
        public ReviewService(IReviewRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<ReviewDTO>> GetAllReviewsAsync(Guid? id, Guid? filterByBoardGame)
        {
            List<ReviewDTO> list = await Repository.GetAllReviewsAsync(id, filterByBoardGame);
            return list;
        }

        public async Task<ReviewDTO> GetOneReviewAsync(Guid id)
        {
            ReviewDTO review = await Repository.GetOneReviewAsync(id);
            return review;
        }

        public async Task<ReviewDTO> EditReviewAsync(Guid id, ReviewDTO reviewDTO)
        {
            ReviewDTO review = await Repository.EditReviewAsync(id, reviewDTO);
            return review;
        }

        public async Task<ReviewDTO> ApproveReviewAsync(Guid id, ReviewDTO reviewDTO)
        {
            ReviewDTO review = await Repository.ApproveReviewAsync(id, reviewDTO);
            return review;
        }

        public async Task<ReviewDTO> HideDeleteReviewAsync(Guid id, ReviewDTO reviewDTO)
        {
            ReviewDTO review = await Repository.HideDeleteReviewAsync(id, reviewDTO);
            return review;
        }

        public async Task<ReviewDTO> AddNewReviewAsync(Guid id, ReviewDTO reviewDTO)
        {
            ReviewDTO review = await Repository.AddNewReviewAsync(id, reviewDTO);
            return review;
        }
    }
}
