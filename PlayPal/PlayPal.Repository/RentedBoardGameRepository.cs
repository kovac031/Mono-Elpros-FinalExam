using AutoMapper;
using PagedList;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository
{
    public class RentedBoardGameRepository : IRentedBoardGameRepository
    {
        protected EFContext Context { get; set; }
        protected IMapper Mapper { get; set; }

        public RentedBoardGameRepository(EFContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IPagedList<RentedBoardGameDTO>> GetAllRentedGames(Guid? id, string sortBy, string search, int pageNumber, int pageSize)
        {
            try
            {
                var query = Context.RentedBoardGame.
                    Include(rbg => rbg.BoardGame).
                    Where(rbg => rbg.BoardGame.Title.Contains(search) || search == null).
                    Join(Context.CoreUser, rbg => rbg.CoreUserId, cu => cu.Id, (rbg, cu) => new { RentedBoardGame = rbg, CoreUser = cu }).
                    Where(rbg => id == null || rbg.CoreUser.Id == id).
                               Select(rbg => new
                               {
                                   Id = rbg.RentedBoardGame.Id,
                                   Username = rbg.CoreUser.Username,
                                   GameTitle = rbg.RentedBoardGame.BoardGame.Title,
                                   DateRented = rbg.RentedBoardGame.DateRented,
                                   DateReturned = rbg.RentedBoardGame.DateReturned,
                                   IsActive = rbg.RentedBoardGame.IsActive,
                                   DueDate = System.Data.Entity.DbFunctions.AddDays(rbg.RentedBoardGame.DateRented, 14)
                               });

                switch (sortBy)
                {
                    case null:
                        query = query.OrderBy(bg => bg.GameTitle);
                        break;
                    case "Game Title desc":
                        query = query.OrderByDescending(bg => bg.GameTitle);
                        break;
                    case "Game Title":
                        query = query.OrderBy(bg => bg.GameTitle);
                        break;
                    case "Username desc":
                        query = query.OrderByDescending(bg => bg.Username);
                        break;
                    case "Username":
                        query = query.OrderBy(bg => bg.Username);
                        break;
                    case "Date Rented":
                        query = query.OrderBy(bg => bg.DateRented);
                        break;
                    case "Date Rented desc":
                        query = query.OrderByDescending(bg => bg.DateRented);
                        break;
                    case "Due Date":
                        query = query.OrderBy(bg => bg.DueDate);
                        break;
                    case "Due Date desc":
                        query = query.OrderByDescending(bg => bg.DueDate);
                        break;
                    case "Date Returned":
                        query = query.OrderBy(bg => bg.DateReturned);
                        break;
                    case "Date Returned desc":
                        query = query.OrderByDescending(bg => bg.DateReturned);
                        break;
                    case "Status":
                        query = query.OrderBy(bg => bg.IsActive);
                        break;
                    case "Status desc":
                        query = query.OrderByDescending(bg => bg.IsActive);
                        break;

                }

                var result = query.ToPagedList(pageNumber, pageSize);

                List<RentedBoardGameDTO> rentedBoardGameDTOs = result.Select(rbg => new RentedBoardGameDTO()
                {
                    Id = rbg.Id,
                    Username = rbg.Username,
                    BoardGameTitle = rbg.GameTitle,
                    DateRented = rbg.DateRented,
                    DateReturned = (DateTime)rbg.DateReturned,
                    IsActive = rbg.IsActive,
                    DueDate = (DateTime)rbg.DueDate
                }).ToList();

                var pagedList = new StaticPagedList<RentedBoardGameDTO>(rentedBoardGameDTOs, pageNumber, pageSize, result.TotalItemCount);
                return await Task.FromResult(pagedList);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<RentedBoardGameDTO> GetRentedBoardGameById(Guid id)
        {
            try
            {
                RentedBoardGame rentedBoardGame = await Context.RentedBoardGame.Where(rbg => rbg.Id == id).
                    FirstOrDefaultAsync();
                if (rentedBoardGame == null)
                {
                    return null;
                }
                RentedBoardGameDTO rentedBoardGameDTO = new RentedBoardGameDTO
                {
                    BoardGameId = rentedBoardGame.BoardGameId,
                    Id = id,
                    CoreUserId = rentedBoardGame.CoreUserId,
                    CreatedByUserId = rentedBoardGame.CreatedByUserId,
                    UpdatedByUserId = rentedBoardGame.UpdatedByUserId,
                    DateCreated = rentedBoardGame.DateCreated,
                    DateUpdated = rentedBoardGame.DateUpdated,
                    DateRented = rentedBoardGame.DateRented,
                    DateReturned = (DateTime)rentedBoardGame.DateReturned,
                    IsActive = rentedBoardGame.IsActive
                };
                return rentedBoardGameDTO;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> MarkGameAsReturned(RentedBoardGameDTO rentedBoardGameDTO)
        {
            IQueryable<RentedBoardGame> query = Context.RentedBoardGame.AsQueryable();

            RentedBoardGame rentedBoardGameDal = await query.FirstOrDefaultAsync(rbg => rbg.Id == rentedBoardGameDTO.Id);
            if (rentedBoardGameDal != null)
            {
                rentedBoardGameDal.Id = rentedBoardGameDTO.Id;
                rentedBoardGameDal.BoardGameId = rentedBoardGameDTO.BoardGameId;
                rentedBoardGameDal.DateReturned = rentedBoardGameDTO.DateReturned;
                rentedBoardGameDal.DateRented = rentedBoardGameDTO.DateRented;
                rentedBoardGameDal.DateCreated = rentedBoardGameDTO.DateCreated;
                rentedBoardGameDal.DateUpdated = rentedBoardGameDTO.DateUpdated;
                rentedBoardGameDal.UpdatedByUserId = rentedBoardGameDTO.UpdatedByUserId;
                rentedBoardGameDal.CreatedByUserId = rentedBoardGameDTO.CreatedByUserId;
                rentedBoardGameDal.IsActive = rentedBoardGameDTO.IsActive;
                await Context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> RentBoardGame(RentedBoardGameDTO boardGameDTO)
        {
            try
            {
                if (boardGameDTO == null)
                {
                    return false;
                }
                RentedBoardGame rentedBoardGame = new RentedBoardGame
                {
                    Id = boardGameDTO.Id,
                    BoardGameId = boardGameDTO.BoardGameId,
                    CoreUserId = boardGameDTO.CoreUserId,
                    DateCreated = boardGameDTO.DateCreated,
                    DateUpdated = boardGameDTO.DateUpdated,
                    CreatedByUserId = boardGameDTO.CreatedByUserId,
                    UpdatedByUserId = boardGameDTO.UpdatedByUserId,
                    DateRented = boardGameDTO.DateRented,
                    DateReturned = boardGameDTO.DateReturned,
                    IsActive = boardGameDTO.IsActive

                };
                Context.RentedBoardGame.Add(rentedBoardGame);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
