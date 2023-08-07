using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.ModelDTO.Common;
using PlayPal.MVC.Models;
using PlayPal.Service;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace PlayPal.MVC.Controllers
{
    public class ReviewController : Controller
    {
        protected IReviewService Service { get; set; }
        protected IBoardGameService BoardGameService { get; set; }
        public ReviewController(IReviewService service, IBoardGameService boardGameService)
        {
            Service = service;
            BoardGameService = boardGameService;
        }

        //--------------------------------------------------------------------
        // api/review/getallasync --- GET ALL REVIEWS ------
        public async Task<ActionResult> GetAllReviewsAsync(Guid? id)
        {
            List<ReviewDTO> listDTO = await Service.GetAllReviewsAsync(id, null);
            List<ReviewVIEW> listVIEW = new List<ReviewVIEW>();
            foreach (ReviewDTO reviewDTO in listDTO)
            {
                ReviewVIEW reviewVIEW = new ReviewVIEW();
                reviewVIEW.Id = reviewDTO.Id;
                reviewVIEW.Title = reviewDTO.Title;
                reviewVIEW.Comment = reviewDTO.Comment;
                reviewVIEW.IsApproved = reviewDTO.IsApproved;
                reviewVIEW.Rating = reviewDTO.Rating;
                reviewVIEW.DateCreated = reviewDTO.DateCreated;
                reviewVIEW.DateUpdated = reviewDTO.DateUpdated;
                reviewVIEW.CreatedByUserId = reviewDTO.CreatedByUserId;
                reviewVIEW.UpdatedByUserId = reviewDTO.UpdatedByUserId;
                reviewVIEW.IsActive = reviewDTO.IsActive;
                reviewVIEW.CoreUserId = reviewDTO.CoreUserId;
                reviewVIEW.BoardGameId = reviewDTO.BoardGameId;

                listVIEW.Add(reviewVIEW);
            }
            return View(listVIEW);
        }

        //public async Task<ActionResult> GetAllReviewsPartialAsync(Guid? id)
        //{
        //    List<ReviewDTO> listDTO = await Service.GetAllReviewsAsync(id);
        //    List<ReviewVIEW> listVIEW = new List<ReviewVIEW>();
        //    foreach (ReviewDTO reviewDTO in listDTO)
        //    {
        //        ReviewVIEW reviewVIEW = new ReviewVIEW();
        //        reviewVIEW.Id = reviewDTO.Id;
        //        reviewVIEW.Title = reviewDTO.Title;
        //        reviewVIEW.Comment = reviewDTO.Comment;
        //        reviewVIEW.IsApproved = reviewDTO.IsApproved;
        //        reviewVIEW.Rating = reviewDTO.Rating;
        //        reviewVIEW.DateCreated = reviewDTO.DateCreated;
        //        reviewVIEW.DateUpdated = reviewDTO.DateUpdated;
        //        reviewVIEW.CreatedByUserId = reviewDTO.CreatedByUserId;
        //        reviewVIEW.UpdatedByUserId = reviewDTO.UpdatedByUserId;
        //        reviewVIEW.IsActive = reviewDTO.IsActive;
        //        reviewVIEW.CoreUserId = reviewDTO.CoreUserId;
        //        reviewVIEW.BoardGameId = reviewDTO.BoardGameId;

        //        listVIEW.Add(reviewVIEW);
        //    }
        //    return PartialView("GetAllReviewsAsyncPARTIAL", listVIEW);
        //}

        //--------------------------------------------------------------------
        //----- GET ONE REVIEW BY ID --------------
        public async Task<ActionResult> GetOneReviewAsync(Guid id)
        {
            ReviewDTO reviewDTO = await Service.GetOneReviewAsync(id);
            ReviewVIEW reviewVIEW = new ReviewVIEW();
            reviewVIEW.Id = reviewDTO.Id;
            reviewVIEW.Title = reviewDTO.Title;
            reviewVIEW.Comment = reviewDTO.Comment;
            reviewVIEW.IsApproved = reviewDTO.IsApproved;
            reviewVIEW.Rating = reviewDTO.Rating;
            reviewVIEW.DateCreated = reviewDTO.DateCreated;
            reviewVIEW.DateUpdated = reviewDTO.DateUpdated;
            reviewVIEW.CreatedByUserId = reviewDTO.CreatedByUserId;
            reviewVIEW.UpdatedByUserId = reviewDTO.UpdatedByUserId;
            reviewVIEW.IsActive = reviewDTO.IsActive;
            reviewVIEW.CoreUserId = reviewDTO.CoreUserId;
            reviewVIEW.BoardGameId = reviewDTO.BoardGameId;

            return View(reviewVIEW);
        }

        //-----------------------------------------------------------------
        //------------- EDIT REVIEW ------------
        public async Task<ActionResult> EditReviewAsync(Guid id)
        {
            ReviewDTO reviewDTO = await Service.GetOneReviewAsync(id);
            ReviewVIEW reviewVIEW = new ReviewVIEW();

            reviewVIEW.Id = reviewDTO.Id;
            reviewVIEW.Title = reviewDTO.Title;
            reviewVIEW.Comment = reviewDTO.Comment;
            reviewVIEW.Rating = reviewDTO.Rating;
            reviewVIEW.DateUpdated = reviewDTO.DateUpdated;
            reviewVIEW.UpdatedByUserId = reviewDTO.UpdatedByUserId;

            return View(reviewVIEW);
        }
        [HttpPost]
        public async Task<ActionResult> EditReviewAsync(ReviewVIEW reviewVIEW)
        {
            var userId = "";
            if (User.Identity is FormsIdentity identity)
            {
                userId = identity.Ticket.UserData;
            }

            ReviewDTO reviewDTO = new ReviewDTO();

            reviewDTO.Id = reviewVIEW.Id;
            reviewDTO.Title = reviewVIEW.Title;
            reviewDTO.Comment = reviewVIEW.Comment;
            reviewDTO.Rating = reviewVIEW.Rating;
            reviewDTO.DateUpdated = reviewVIEW.DateUpdated = DateTime.Now;
            reviewDTO.UpdatedByUserId = reviewVIEW.UpdatedByUserId = new Guid(userId);

            await Service.EditReviewAsync(reviewDTO.Id, reviewDTO);

            return RedirectToAction("GetAllReviewsAsync"); // rijesi gdje da vraca kad budemo poslozili sve
        }

        //-----------------------------------------------------------------
        //------------- APPROVE REVIEW ------------
        public async Task<ActionResult> ApproveReviewAsync(Guid id)
        {
            ReviewDTO reviewDTO = await Service.GetOneReviewAsync(id);
            ReviewVIEW reviewVIEW = new ReviewVIEW();
            reviewVIEW.Id = reviewDTO.Id;
            reviewVIEW.IsApproved = reviewDTO.IsApproved;
            reviewVIEW.DateUpdated = reviewDTO.DateUpdated;
            reviewVIEW.UpdatedByUserId = reviewDTO.UpdatedByUserId;

            return View(reviewVIEW);
        }
        [HttpPost]
        public async Task<ActionResult> ApproveReviewAsync(ReviewVIEW reviewVIEW)
        {
            var userId = "";
            if (User.Identity is FormsIdentity identity)
            {
                userId = identity.Ticket.UserData;
            }

            ReviewDTO reviewDTO = new ReviewDTO();

            reviewDTO.Id = reviewVIEW.Id;
            reviewDTO.IsApproved = reviewVIEW.IsApproved;
            reviewDTO.DateUpdated = reviewVIEW.DateUpdated = DateTime.Now;
            reviewDTO.UpdatedByUserId = reviewVIEW.UpdatedByUserId = new Guid(userId);

            await Service.ApproveReviewAsync(reviewDTO.Id, reviewDTO);

            return View();
        }

        //---------------------------------------------------------
        //---------- HIDE "DELETE" REVIEW -------------
        public async Task<ActionResult> HideDeleteReviewAsync(Guid id)
        {
            ReviewDTO reviewDTO = await Service.GetOneReviewAsync(id);
            ReviewVIEW reviewVIEW = new ReviewVIEW();
            reviewVIEW.Id = reviewDTO.Id;
            reviewVIEW.IsActive = reviewDTO.IsActive;

            return View(reviewVIEW);
        }
        [HttpPost]
        public async Task<ActionResult> HideDeleteReviewAsync(ReviewVIEW reviewVIEW)
        {
            ReviewDTO reviewDTO = new ReviewDTO();

            reviewDTO.Id = reviewVIEW.Id;
            reviewDTO.IsActive = reviewVIEW.IsActive;

            await Service.HideDeleteReviewAsync(reviewDTO.Id, reviewDTO);

            return View();
        }

        //----------------------------------------------------------------------
        // ------------ ADD NEW REVIEW ---------------
        [HttpGet]
        public async Task<ActionResult> AddNewReviewAsync(Guid id) 
        {
            
            BoardGameDTO boardGameDTO = await BoardGameService.GetBoardGame(id);
            
            ReviewVIEW reviewVIEW = new ReviewVIEW();

            ReviewDTO reviewDTO = new ReviewDTO();
            reviewVIEW.Id = reviewDTO.Id;
            reviewVIEW.Title = reviewDTO.Title;
            reviewVIEW.Comment = reviewDTO.Comment;
            reviewVIEW.IsApproved = reviewDTO.IsApproved;
            reviewVIEW.Rating = reviewDTO.Rating;
            reviewVIEW.DateCreated = reviewDTO.DateCreated;
            reviewVIEW.DateUpdated = reviewDTO.DateUpdated;
            reviewVIEW.CreatedByUserId = reviewDTO.CreatedByUserId;
            reviewVIEW.UpdatedByUserId = reviewDTO.UpdatedByUserId;
            reviewVIEW.IsActive = reviewDTO.IsActive;
            reviewVIEW.CoreUserId = reviewDTO.CoreUserId;
            reviewVIEW.BoardGameId = boardGameDTO.Id;

            return PartialView("AddNewReviewAsyncPARTIAL", reviewVIEW);
        }
        [HttpPost, ActionName("Create")]
        public async Task<ActionResult> AddNewReviewAsync(ReviewVIEW reviewVIEW, Guid id)
        {
            var userId = "";
            if (User.Identity is FormsIdentity identity)
            {
                userId = identity.Ticket.UserData;
            }
            BoardGameDTO boardGameDTO = await BoardGameService.GetBoardGame(id);
            ReviewDTO reviewDTO = new ReviewDTO();

            reviewDTO.Id = reviewVIEW.Id = Guid.NewGuid();
            reviewDTO.Title = reviewVIEW.Title;
            reviewDTO.Comment = reviewVIEW.Comment;
            reviewDTO.IsApproved = reviewVIEW.IsApproved = false;
            reviewDTO.Rating = reviewVIEW.Rating;
            reviewDTO.DateCreated = reviewVIEW.DateCreated = DateTime.Now;
            reviewDTO.DateUpdated = reviewVIEW.DateUpdated = DateTime.Now;
            reviewDTO.CreatedByUserId = reviewVIEW.CreatedByUserId = new Guid(userId);
            reviewDTO.UpdatedByUserId = reviewVIEW.UpdatedByUserId = new Guid(userId);
            reviewDTO.IsActive = reviewVIEW.IsActive = true;
            reviewDTO.CoreUserId = reviewVIEW.CoreUserId = new Guid(userId);
            reviewDTO.BoardGameId = reviewVIEW.BoardGameId = boardGameDTO.Id;

            await Service.AddNewReviewAsync(reviewDTO.Id, reviewDTO);
            return RedirectToAction("BoardGameDetail", "BoardGame", new { id = id });
            // radi, ali vraca na odakle je poslan, nema confirm poruka nikakva
        }


    }
}
