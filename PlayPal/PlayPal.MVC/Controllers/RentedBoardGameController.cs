using AutoMapper;
using PagedList;
using PlayPal.ModelDTO;
using PlayPal.MVC.Models;
using PlayPal.MVC.Models.BoardGameView;
using PlayPal.MVC.Models.RentedBoardGameView;
using PlayPal.Service;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlayPal.MVC.Controllers
{
    public class RentedBoardGameController : Controller
    {
        protected IMapper Mapper;
        protected IRentedBoardGameService RentedBoardGameService { get; set; }


        public RentedBoardGameController(IRentedBoardGameService rentedboardGameService, IMapper mapper)
        {
            Mapper = mapper;
            RentedBoardGameService = rentedboardGameService;
        }

        [HttpGet]
        public async Task<ActionResult> RentedBoardGameList(Guid? id,string sortBy, string search, int? pageNumber, int? pageSize)
        {
            try
            {
                ViewBag.SortByTitle = string.IsNullOrEmpty(sortBy) ? "Game Title desc" : "";
                ViewBag.SortByUsername = sortBy == "Username" ? "Username desc" : "Username";
                ViewBag.SortByDateRented = sortBy == "Date Rented" ? "Date Rented desc" : "Date Rented";
                ViewBag.SortByDueDate = sortBy == "Due Date" ? "Due Date desc" : "Due Date";
                ViewBag.SortByDateReturned = sortBy == "Date Returned" ? "Date Returned desc" : "Date Returned";
                ViewBag.SortByStatus = sortBy == "Status" ? "Status desc" : "Status";

                IPagedList<RentedBoardGameDTO> rentedBoardGameDTOs = await RentedBoardGameService.GetAllRentedGames(id, sortBy, search, pageNumber ?? 1, pageSize ?? 10);

                if (rentedBoardGameDTOs == null)
                {
                    return View("Error");
                }

                List<RentedBoardGameListView> rentedBoardGameListView = rentedBoardGameDTOs.
                    Select(rbg => new RentedBoardGameListView()
                    {
                        Id = rbg.Id,
                        BoardGameTitle = rbg.BoardGameTitle,
                        RentedByUsername = rbg.Username,
                        DateRented = rbg.DateRented,
                        DateReturned = rbg.DateReturned,
                        DueDate = rbg.DueDate,
                        Status = rbg.IsActive                      
                    }).
                    ToList();

                var pagedList = new StaticPagedList<RentedBoardGameListView>(rentedBoardGameListView, pageNumber ?? 1, pageSize ?? 10, rentedBoardGameDTOs.TotalItemCount);

                return await Task.FromResult(View(pagedList));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Return(Guid id)
        {
            try
            {
                RentedBoardGameDTO rentedBoardGameDto = await RentedBoardGameService.GetRentedBoardGameById(id);
                if (rentedBoardGameDto == null)
                {
                    return View("Error");
                }
                RentedBoardGameReturnView returnBoardGameView = new RentedBoardGameReturnView
                {
                    RentId = id,
                    Returned = rentedBoardGameDto.IsActive == true ? false : true,
                };
                return View(returnBoardGameView);
            }
            catch (Exception)
            {
                return View("Error");
            }
         }
        [HttpPost, ActionName("Return")]
        public async Task<ActionResult> Return(RentedBoardGameReturnView returnBoardGameView)
        {
            try
            {
                var userId = "";
                if (User.Identity is FormsIdentity identity)
                {
                    userId = identity.Ticket.UserData;
                }
                RentedBoardGameDTO rentedBoardGameDTO = new RentedBoardGameDTO
                {
                   Id = returnBoardGameView.RentId,
                DateReturned = DateTime.Now,
                DateUpdated = DateTime.Now,
                UpdatedByUserId = new Guid(userId),
                IsActive = false,
            };


                bool isReturned = await RentedBoardGameService.MarkGameAsReturned(rentedBoardGameDTO);
                if (isReturned)
                {
                    return RedirectToAction("RentedBoardGameList");
                }
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

    }
}