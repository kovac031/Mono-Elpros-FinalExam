using AutoMapper;
using PagedList;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;

namespace PlayPal.Repository
{
    public class BoardGameRepository : IBoardGameRepository
    {
        protected EFContext Context { get; set; }
        protected IMapper Mapper { get; set; }

        public BoardGameRepository(EFContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<IPagedList<BoardGameDTO>> GetBoardGames(string filterBy, string sortBy, string search, int pageNumber, int pageSize)
        {
            try
            {

                var query1 = Context.BoardGame
               .Include(bg => bg.Category)
               .Include(bg => bg.Review)
               .Where(bg => bg.IsActive)
               .Where(bg => bg.Category.Name.Contains(filterBy) || filterBy == null)
               .Where(bg => bg.Title.Contains(search) || search == null)
               .Select(bg => new
               {
                   Id = bg.Id,
                   Title = bg.Title,
                   Price = bg.Price,
                   NumberOfCopies = bg.NumberOfCopies,
                   CategoryName = bg.Category.Name,
                   Rating = bg.Review.Select(r => (double?)r.Rating).Average()
               });

                var query2 = Context.BoardGame
                        .Include(bg => bg.RentedBoardGame)
                        .Where(bg => bg.IsActive)
                        .Select(bg => new
                        {
                            bg.Id,
                            bg.Title,
                            RentedBoardGames = bg.RentedBoardGame.Count(rg => rg.IsActive),
                            bg.NumberOfCopies
                        });

                var query3 = query1
                    .GroupJoin(query2, q1 => q1.Id, q2 => q2.Id, (q1, q2) => new { q1, q2 })
                    .SelectMany(x => x.q2.DefaultIfEmpty(), (x, q2) => new BoardGameDTO
                    {
                        Id = x.q1.Id,
                        Title = x.q1.Title,
                        Price = x.q1.Price,
                        NumberOfCopies = x.q1.NumberOfCopies,
                        CategoryName = x.q1.CategoryName,
                        Rating = x.q1.Rating,
                        RentedGameNumber = q2.RentedBoardGames,
                        AvailableForRent = (x.q1.NumberOfCopies - q2.RentedBoardGames)
                    });

                switch (sortBy)
                {
                    case null:
                        query3 = query3.OrderBy(bg => bg.Title);
                        break;
                    case "Title desc":
                        query3 = query3.OrderByDescending(bg => bg.Title);
                        break;
                    case "Title":
                        query3 = query3.OrderBy(bg => bg.Title);
                        break;
                    case "Category desc":
                        query3 = query3.OrderByDescending(bg => bg.CategoryName);
                        break;
                    case "Category":
                        query3 = query3.OrderBy(bg => bg.CategoryName);
                        break;
                    case "Rating":
                        query3 = query3.OrderBy(bg => bg.Rating);
                        break;
                    case "Rating desc":
                        query3 = query3.OrderByDescending(bg => bg.Rating);
                        break;
                    case "AvailableForRent":
                        query3 = query3.OrderBy(bg => bg.AvailableForRent);
                        break;
                    case "AvailableForRent desc":
                        query3 = query3.OrderByDescending(bg => bg.AvailableForRent);
                        break;
                    case "Price":
                        query3 = query3.OrderBy(bg => bg.Price);
                        break;
                    case "Price desc":
                        query3 = query3.OrderByDescending(bg => bg.Price);
                        break;
                }

                var result = query3.ToPagedList(pageNumber, pageSize);

                List<BoardGameDTO> boardGameDTOs = result.
                    Select(bg => new BoardGameDTO()
                    {
                        Id = bg.Id,
                        Title = bg.Title,
                        Price = bg.Price,
                        CategoryName = bg.CategoryName,
                        Rating = bg.Rating,
                        AvailableForRent = bg.AvailableForRent,
                        RentedGameNumber = bg.RentedGameNumber,
                        NumberOfCopies = bg.NumberOfCopies
                    }).
                    ToList();

                var pagedList = new StaticPagedList<BoardGameDTO>(boardGameDTOs, pageNumber, pageSize, result.TotalItemCount);
                return await Task.FromResult(pagedList);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BoardGameDTO> GetBoardGame(Guid id)
        {
            try
            {
                var query = Context.BoardGame
               .Include(bg => bg.Category)
               .Include(bg => bg.Review)
               .Where(bg => bg.IsActive)
               .Where(bg => bg.Id == id)
               .Select(bg => new
               {
                   Id = bg.Id,
                   Title = bg.Title,
                   Price = bg.Price,
                   Description = bg.Description,
                   NumberOfCopies = bg.NumberOfCopies,
                   CategoryName = bg.Category.Name,
                   DateCreated = bg.DateCreated,
                   DateUpdated = bg.DateUpdated,
                   CreatedByUserId = bg.CreatedByUserId,
                   UpdatedByUserId = bg.UpdatedByUserId,
                   IsActive = bg.IsActive,
                   CategoryId = bg.CategoryId,
                   Rating = bg.Review.Average(r => (double)r.Rating)
               });

                BoardGameDTO boardGameDTO = await query.Select(bg => new BoardGameDTO()
                {
                    Id = id,
                    CategoryName = bg.CategoryName,
                    Description = bg.Description,
                    Title = bg.Title,
                    Price = bg.Price,
                    NumberOfCopies = bg.NumberOfCopies,
                    Rating = bg.Rating,
                    DateCreated = bg.DateCreated,
                    DateUpdated = bg.DateUpdated,
                    CreatedByUserId = bg.CreatedByUserId,
                    UpdatedByUserId = bg.UpdatedByUserId,
                    CategoryId = bg.CategoryId,
                    IsActive = bg.IsActive,

                }).FirstOrDefaultAsync();

                if (boardGameDTO == null)
                {
                    return null;
                }
                return boardGameDTO;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> EditBoardGame(Guid id, BoardGameDTO boardGameDto)
        {
            try
            {
                IQueryable<BoardGame> query = Context.BoardGame.AsQueryable();

                BoardGame boardGameDal = await query.FirstOrDefaultAsync(bg => bg.Id == id);
                if (boardGameDal != null)
                {
                    boardGameDal.Id = boardGameDto.Id;
                    boardGameDal.Title = boardGameDto.Title;
                    boardGameDal.Price = boardGameDto.Price;
                    boardGameDal.Description = boardGameDto.Description;
                    boardGameDal.NumberOfCopies = boardGameDto.NumberOfCopies;
                    boardGameDal.CategoryId = boardGameDto.CategoryId;
                    boardGameDal.DateCreated = boardGameDto.DateCreated;
                    boardGameDal.DateUpdated = boardGameDto.DateUpdated;
                    boardGameDal.UpdatedByUserId = boardGameDto.UpdatedByUserId;
                    boardGameDal.CreatedByUserId = boardGameDto.CreatedByUserId;
                    boardGameDal.IsActive = boardGameDto.IsActive;


                    await Context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateBoardGame(BoardGameDTO boardGameDTO)
        {
            try
            {
                if (boardGameDTO != null)
                {
                    BoardGame boardGame = new BoardGame
                    {
                        Id = boardGameDTO.Id,
                        Title = boardGameDTO.Title,
                        Price = boardGameDTO.Price,
                        Description = boardGameDTO.Description,
                        NumberOfCopies = boardGameDTO.NumberOfCopies,
                        CategoryId = boardGameDTO.CategoryId,
                        IsActive = boardGameDTO.IsActive,
                        CreatedByUserId = boardGameDTO.CreatedByUserId,
                        UpdatedByUserId = boardGameDTO.UpdatedByUserId,
                        DateCreated = boardGameDTO.DateCreated,
                        DateUpdated = boardGameDTO.DateUpdated,

                    };
                    Context.BoardGame.Add(boardGame);
                    await Context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DelteBoardGame(BoardGameDTO boardGameDTO)
        {
            try
            {
                BoardGameDTO boardGameDtocheck = await GetBoardGame(boardGameDTO.Id);

                if (boardGameDtocheck == null)
                {
                    return false;
                }
                BoardGame boardGame = await Context.BoardGame.Where(bg => bg.Id == boardGameDTO.Id).FirstOrDefaultAsync();
                boardGame.Id = boardGameDTO.Id;
                boardGame.Title = boardGameDTO.Title;
                boardGame.CategoryId = boardGameDTO.CategoryId;
                boardGame.Price = boardGameDTO.Price;
                boardGame.Description = boardGameDTO.Description;
                boardGame.NumberOfCopies = boardGameDTO.NumberOfCopies;
                boardGame.DateCreated = boardGameDTO.DateCreated;
                boardGame.DateUpdated = DateTime.Now;
                boardGame.UpdatedByUserId = boardGameDTO.UpdatedByUserId;
                boardGame.CreatedByUserId = boardGameDTO.CreatedByUserId;
                boardGame.IsActive = boardGameDTO.IsActive;

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
