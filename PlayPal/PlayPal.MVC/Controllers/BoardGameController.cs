using AutoMapper;
using PagedList;
using PlayPal.ModelDTO;
using PlayPal.MVC.Models;
using PlayPal.MVC.Models.BoardGameView;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace PlayPal.MVC.Controllers
{
    public class BoardGameController : Controller
    {
        protected IMapper Mapper;
        protected IBoardGameService BoardGameService { get; set; }
        protected ICategoryService CategoryService { get; set; }
        protected IRentedBoardGameService RentedBoardGameService { get; set; }
        protected IReviewService ReviewService { get; set; }


        public BoardGameController(IReviewService reviewService, IRentedBoardGameService rentedBoardGameService, IBoardGameService boardGameService, ICategoryService categoryService, IMapper mapper)
        {
            Mapper = mapper;
            BoardGameService = boardGameService;
            CategoryService = categoryService;
            RentedBoardGameService = rentedBoardGameService;
            ReviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult> BoardGameList(string sortBy, string search, string filterBy, int? pageNumber, int? pageSize)
        {
            try
            {
                ViewBag.SortByTitle = string.IsNullOrEmpty(sortBy) ? "Title desc" : "";
                ViewBag.SortByCategory = sortBy == "Category" ? "Category desc" : "Category";
                ViewBag.SortByRating = sortBy == "Rating" ? "Rating desc" : "Rating";
                ViewBag.SortByAvailability = sortBy == "AvailableForRent" ? "AvailableForRent desc" : "AvailableForRent";
                ViewBag.SortByPrice = sortBy == "Price" ? "Price desc" : "Price";

                List<CategoryDTO> categories = await CategoryService.GetAllCategoriesAsync();
                List<CategoryView> categoryViews = categories.Where(c => c.IsActive == true).Select(c => new CategoryView()
                {
                    Name = c.Name,
                }).ToList();

                ViewBag.SelectedCategory = filterBy;
                ViewBag.Categories = new SelectList(categoryViews, "Name", "Name");

                IPagedList<BoardGameDTO> boardGameDTOs = await BoardGameService.GetBoardGames(filterBy,sortBy, search, pageNumber ?? 1, pageSize ?? 10);

                if (boardGameDTOs == null)
                {
                    return View("Error");
                }

                List<BoardGameListView> boardGameListView = boardGameDTOs.
                    Select(s => new BoardGameListView()
                    {
                        Id = s.Id,
                        Availability = s.AvailableForRent.ToString(),
                        CategoryName = s.CategoryName,
                        Title = s.Title,
                        Rating = s.Rating == null ? $"Not rated Yet" : $"{s.Rating:0.00}",
                        Price = s.Price
                    }).
                    ToList();

                var pagedList = new StaticPagedList<BoardGameListView>(boardGameListView, pageNumber ?? 1, pageSize ?? 10, boardGameDTOs.TotalItemCount);

                return await Task.FromResult(View(pagedList));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        [HttpGet]
        public async Task<ActionResult> BoardGameDetail(Guid id)
        {
            try
            {
                BoardGameDTO boardGameDto = await BoardGameService.GetBoardGame(id);
                if (boardGameDto == null)
                {
                    return View("Error");
                }
                BoardGameDetailView boardGameView = new BoardGameDetailView
                {
                    CategoryName = boardGameDto.CategoryName,
                    Id = id,
                    Description = boardGameDto.Description,
                    Price = boardGameDto.Price.ToString(),
                    Rating = boardGameDto.Rating == null ? $"Not rated Yet" : $"{boardGameDto.Rating:0.00}",
                    Title = boardGameDto.Title,
                };
                List<ReviewDTO> listDTO = await ReviewService.GetAllReviewsAsync(null, id);
                boardGameView.Reviews = new List<ReviewVIEW>();
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

                    boardGameView.Reviews.Add(reviewVIEW);
                }

                return View(boardGameView);

            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                BoardGameDTO boardGameDto = await BoardGameService.GetBoardGame(id);
                if (boardGameDto == null)
                {
                    return View("Error");
                }
                List<CategoryDTO> categories = await CategoryService.GetAllCategoriesAsync();
                List<CategoryView> categoryViews = categories.Select(c => new CategoryView
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList();

                BoardGameEditView boardGameEditView = new BoardGameEditView
                {
                    Id=id,
                    Categories = categoryViews,
                    Description = boardGameDto.Description,
                    Price = boardGameDto.Price,
                    Title=boardGameDto.Title,
                };

                return View(boardGameEditView);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(BoardGameEditView boardGameEditView)
        {
            try
            {
                var userId = "";
                if (User.Identity is FormsIdentity identity)
                {
                    userId = identity.Ticket.UserData;
                }

                var boardGameDto = new BoardGameDTO
                {
                    Id = boardGameEditView.Id,
                    Title = boardGameEditView.Title,
                    Price = boardGameEditView.Price,
                    Description = boardGameEditView.Description,
                    CategoryId = boardGameEditView.SelectedCategory,
                    DateUpdated = DateTime.Now,
                    UpdatedByUserId = new Guid(userId)
                    
                };               

                bool isEdited = await BoardGameService.EditBoardGame(boardGameDto.Id, boardGameDto);
                if (!isEdited)
                {
                    return View("Error");
                }
                return RedirectToAction("BoardGameList");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            List<CategoryDTO> categories = await CategoryService.GetAllCategoriesAsync();
            List<CategoryView> categoryViews = categories.Where(c => c.IsActive == true).Select(c => new CategoryView
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            BoardGameCreateView boardGameCreateView = new BoardGameCreateView
            {
                AllCategories = categoryViews
            };

            return await Task.FromResult(View("Create",boardGameCreateView));
        }
        [HttpPost, ActionName("Create")]
        public async Task<ActionResult> Create(BoardGameCreateView boardGameCreateView)
        {
            try
            {
                var userId = "";
                if(User.Identity is FormsIdentity identity)
                {
                    userId = identity.Ticket.UserData;
                }

                BoardGameDTO boardGameDto = new BoardGameDTO
                {
                    Id = Guid.NewGuid(),
                    Title = boardGameCreateView.Title,
                    Price = boardGameCreateView.Price,  
                    Description = boardGameCreateView.Description,
                    NumberOfCopies = boardGameCreateView.NumberOfCopies,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedByUserId = new Guid(userId),
                    UpdatedByUserId = new Guid(userId),
                    IsActive = boardGameCreateView.IsActive,
                    CategoryId = boardGameCreateView.SelectedCategory
                };

                bool isAdded = await BoardGameService.CreateBoardGame(boardGameDto);
                if (!isAdded)
                {
                    return View("Error");
                }
                return RedirectToAction("BoardGameList");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Rent(Guid id)
        {
            try
            {
                BoardGameDTO boardGameDto = await BoardGameService.GetBoardGame(id);
                if (boardGameDto == null)
                {
                    return View("Error");
                }
                BoardGameRentView boardGameView = new BoardGameRentView
                {
                    CategoryName = boardGameDto.CategoryName,
                    BoardGameId = id,
                    Description = boardGameDto.Description,
                    Rating = boardGameDto.Rating == null ? $"Not rated Yet" : $"{boardGameDto.Rating:0.00}",
                    Title = boardGameDto.Title,
                    Price = $"{boardGameDto.Price:C}",
                    IsAvailable = boardGameDto.IsActive
                    
                };
                return View(boardGameView);
            }
            catch (Exception)
            {
                return View("Error");
            }  
        }
        [HttpPost, ActionName("Rent")]
        public async Task<ActionResult> Rent(BoardGameRentView boardGameRentView)
        {
            try
            {
                var userId = "";
                if (User.Identity is FormsIdentity identity)
                {
                    userId = identity.Ticket.UserData;
                }

                RentedBoardGameDTO rentDTO = new RentedBoardGameDTO
                {
                    BoardGameId = boardGameRentView.BoardGameId,
                    Id = Guid.NewGuid(),
                    CoreUserId = new Guid(userId),
                    CreatedByUserId = new Guid(userId),
                    UpdatedByUserId = new Guid(userId),
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    DateRented = DateTime.Now,
                    DateReturned = DateTime.Now,
                    IsActive = true
                };
                bool isAdded = await RentedBoardGameService.RentBoardGame(rentDTO);
                if (!isAdded)
                {
                    return View("Error");
                }
                return RedirectToAction("BoardGameList");

            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                BoardGameDTO boardGameDto = await BoardGameService.GetBoardGame(id);
                if (boardGameDto == null)
                {
                    return View("Error");
                }
                BoardGameDeleteView boardGameView = new BoardGameDeleteView
                {
                    Id = boardGameDto.Id,
                    Title = boardGameDto.Title,
                    IsActive = boardGameDto.IsActive,                  
                };
                return View(boardGameView);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteOnConfirmed(BoardGameDeleteView boardGameDeleteView)
        {
            try
            {
                var userId = "";
                if (User.Identity is FormsIdentity identity)
                {
                    userId = identity.Ticket.UserData;
                }
                BoardGameDTO boardGameDTO = new BoardGameDTO
                {
                    Id = boardGameDeleteView.Id,
                    Title = boardGameDeleteView.Title,
                    IsActive = false,
                    UpdatedByUserId = new Guid(userId)
                };

                bool isDeleted = await BoardGameService.DelteBoardGame(boardGameDTO);
                if (!isDeleted)
                {
                    return View("Error");
                }
                return RedirectToAction("BoardGameList");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


    }
}