using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace PlayPal.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        protected EFContext Context { get; set; }
        public ReviewRepository(EFContext context)
        {
            Context = context;
        }

        //---------------------------------------------------------------------
        //------- GET ALL -----
        public async Task<List<ReviewDTO>> GetAllReviewsAsync(Guid? id, Guid? filterByBoardGame)
        {
            List<ReviewDTO> list;

            var reviews = Context.Review.Where(review => review.IsActive == true);

            if (id.HasValue)
            {
                reviews = reviews.Where(review => review.CoreUserId == id.Value);
            }

            if(filterByBoardGame != null)
            {
                reviews = reviews.Where(review => review.BoardGameId == filterByBoardGame.Value);
            }

            list = await reviews.Select(review => new ReviewDTO()
            {
                Id = review.Id,
                Title = review.Title,
                Comment = review.Comment,
                IsApproved = review.IsApproved,
                Rating = review.Rating,
                DateCreated = review.DateCreated,
                DateUpdated = review.DateUpdated,
                CreatedByUserId = review.CreatedByUserId,
                UpdatedByUserId = review.UpdatedByUserId,
                IsActive = review.IsActive,
                CoreUserId = review.CoreUserId,
                BoardGameId = review.BoardGameId
            }).ToListAsync<ReviewDTO>();

            return list;
        }

        //-----------------------------------------------------------------------
        //----- GET BY ID ------
        public async Task<ReviewDTO> GetOneReviewAsync(Guid id)
        {
            ReviewDTO review;

            review = await Context.Review.Where(r => r.Id == id && r.IsActive == true).Select(r => new ReviewDTO()
            {
                Id = r.Id,
                Title = r.Title,
                Comment = r.Comment,
                IsApproved = r.IsApproved,
                Rating = r.Rating,
                DateCreated = r.DateCreated,
                DateUpdated = r.DateUpdated,
                CreatedByUserId = r.CreatedByUserId,
                UpdatedByUserId = r.UpdatedByUserId,
                IsActive = r.IsActive,
                CoreUserId = r.CoreUserId,
                BoardGameId = r.BoardGameId
            }).FirstOrDefaultAsync<ReviewDTO>();

            return review;
        }

        //-----------------------------------------------------------------------
        //----- EDIT REVIEW ------
        public async Task<ReviewDTO> EditReviewAsync(Guid id, ReviewDTO reviewDTO)
        {
            Review review = await Context.Review.Where(r => r.Id == id && r.IsActive == true).FirstOrDefaultAsync();
            if (review != null)
            {
                review.Id = reviewDTO.Id;
                review.Title = reviewDTO.Title;
                review.Comment = reviewDTO.Comment;
                review.Rating = reviewDTO.Rating;
                review.DateUpdated = reviewDTO.DateUpdated;
                review.UpdatedByUserId = reviewDTO.UpdatedByUserId;

                Context.SaveChanges();
            }
            else
            {
                return (null);
            }

            return reviewDTO;
        }

        //------------------------------------------------------------------
        //------- APPROVE --------

        public async Task<ReviewDTO> ApproveReviewAsync(Guid id, ReviewDTO reviewDTO)
        {
            Review review = await Context.Review.Where(r => r.Id == id && r.IsActive == true).FirstOrDefaultAsync();
            if (review != null)
            {
                review.Id = reviewDTO.Id;
                review.IsApproved = reviewDTO.IsApproved;
                review.DateUpdated = reviewDTO.DateUpdated;                
                review.UpdatedByUserId = reviewDTO.UpdatedByUserId;
                // kod rebase dovoljna ova cetri

                Context.SaveChanges();
            }
            else
            {
                return (null);
            }

            return reviewDTO;
        }

        //------------------------------------------------------------------
        //------- HIDE "DELETE" --------

        public async Task<ReviewDTO> HideDeleteReviewAsync(Guid id, ReviewDTO reviewDTO)
        {
            Review review = await Context.Review.Where(r => r.Id == id && r.IsActive == true).FirstOrDefaultAsync();

            review.Id = reviewDTO.Id;
            review.IsActive = reviewDTO.IsActive;
            // kod rebase dovoljna ova dva

            await Context.SaveChangesAsync();

            return reviewDTO;
        }

        //--------------------------------------------------------------------
        //----------- ADD NEW REVIEW ------

        public async Task<ReviewDTO> AddNewReviewAsync(Guid id, ReviewDTO reviewDTO)
        {

            Context.Review.Add(new Review()
            {

                Id = reviewDTO.Id,
                Title = reviewDTO.Title,
                Comment = reviewDTO.Comment,
                IsApproved = reviewDTO.IsApproved,
                Rating = reviewDTO.Rating,
                DateCreated = reviewDTO.DateCreated,
                DateUpdated = reviewDTO.DateUpdated,
                CreatedByUserId = reviewDTO.CreatedByUserId,
                UpdatedByUserId = reviewDTO.UpdatedByUserId,
                IsActive = reviewDTO.IsActive,
                CoreUserId = reviewDTO.CoreUserId,
                BoardGameId = reviewDTO.BoardGameId
            }) ; 

            await Context.SaveChangesAsync();
            return reviewDTO;
        }

    }
}
