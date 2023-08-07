using PagedList;
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
    public class BoardGameService : IBoardGameService
    {
        protected IBoardGameRepository BoardGameRepository { get; set; }

        public BoardGameService(IBoardGameRepository boardGameRepository) 
        {
            BoardGameRepository = boardGameRepository;
        }

        public async Task<IPagedList<BoardGameDTO>> GetBoardGames(string filterBy, string sortBy, string search, int pageNumber, int pageSize)
        {
            IPagedList<BoardGameDTO> boardGames = await BoardGameRepository.GetBoardGames(filterBy, sortBy, search, pageNumber, pageSize);
            return boardGames;
        }

        public async Task<BoardGameDTO> GetBoardGame(Guid id)
        {
            BoardGameDTO boardGameDTO = await BoardGameRepository.GetBoardGame(id);
            return boardGameDTO;
        }

        public async Task<bool> EditBoardGame(Guid id, BoardGameDTO boardGameDto)
        {
            BoardGameDTO boardGameCheck = await BoardGameRepository.GetBoardGame(id);
            if (boardGameCheck == null)
            {
                return false;
            }
            BoardGameDTO boardGameToEdit = new BoardGameDTO
            {
                Id = id,
                Title = boardGameDto.Title == default ? boardGameCheck.Title : boardGameDto.Title,
                CategoryId = boardGameDto.CategoryId == default ? boardGameCheck.CategoryId : boardGameDto.CategoryId,
                Price = boardGameDto.Price == default ? boardGameCheck.Price : boardGameDto.Price,
                Description = boardGameDto.Description == default ? boardGameCheck.Description : boardGameDto.Description,
                NumberOfCopies = boardGameDto.NumberOfCopies == default ? boardGameCheck.NumberOfCopies : boardGameDto.NumberOfCopies,
                DateCreated = boardGameDto.DateCreated == default ? boardGameCheck.DateCreated : boardGameDto.DateCreated,
                DateUpdated = boardGameDto.DateUpdated == default ? boardGameCheck.DateUpdated : boardGameDto.DateUpdated,
                CreatedByUserId = boardGameDto.CreatedByUserId == default ? boardGameCheck.CreatedByUserId : boardGameDto.CreatedByUserId,
                UpdatedByUserId = boardGameDto.UpdatedByUserId == default ? boardGameCheck.UpdatedByUserId : boardGameDto.UpdatedByUserId,
                IsActive = boardGameDto.IsActive == default ? boardGameCheck.IsActive : boardGameDto.IsActive
            };
            bool isEdited = await BoardGameRepository.EditBoardGame(id, boardGameToEdit);
            return isEdited;
        }

        public async Task<bool> CreateBoardGame(BoardGameDTO boardGameDTO)
        {
            bool isAdded = await BoardGameRepository.CreateBoardGame(boardGameDTO);
            return isAdded;
        }

        public async Task<bool> DelteBoardGame(BoardGameDTO boardGameDto)
        {
            BoardGameDTO boardGameCheck = await BoardGameRepository.GetBoardGame(boardGameDto.Id);
            if (boardGameCheck == null)
            {
                return false;
            }
            BoardGameDTO boardGameToDelete = new BoardGameDTO
            {
                Id = boardGameDto.Id,
                Title = boardGameDto.Title == default ? boardGameCheck.Title : boardGameDto.Title,
                CategoryId = boardGameDto.CategoryId == default ? boardGameCheck.CategoryId : boardGameDto.CategoryId,
                Price = boardGameDto.Price == default ? boardGameCheck.Price : boardGameDto.Price,
                Description = boardGameDto.Description == default ? boardGameCheck.Description : boardGameDto.Description,
                NumberOfCopies = boardGameDto.NumberOfCopies == default ? boardGameCheck.NumberOfCopies : boardGameDto.NumberOfCopies,
                DateCreated = boardGameDto.DateCreated == default ? boardGameCheck.DateCreated : boardGameDto.DateCreated,
                DateUpdated = boardGameDto.DateUpdated == default ? boardGameCheck.DateUpdated : boardGameDto.DateUpdated,
                CreatedByUserId = boardGameDto.CreatedByUserId == default ? boardGameCheck.CreatedByUserId : boardGameDto.CreatedByUserId,
                UpdatedByUserId = boardGameDto.UpdatedByUserId == default ? boardGameCheck.UpdatedByUserId : boardGameDto.UpdatedByUserId,
                IsActive = false
            };

            bool isDeleted = await BoardGameRepository.DelteBoardGame(boardGameToDelete);
            return isDeleted;
        }
    }
}
