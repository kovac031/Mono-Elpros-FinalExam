using PagedList;
using PlayPal.ModelDTO;
using PlayPal.Repository;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service
{
    public class RentedBoardGameService : IRentedBoardGameService
    {
        protected IRentedBoardGameRepository RentedBoardGameRepository { get; set; }

        public RentedBoardGameService(IRentedBoardGameRepository rentedBoardGameRepository)
        {
            RentedBoardGameRepository = rentedBoardGameRepository;
        }

        public async Task<IPagedList<RentedBoardGameDTO>> GetAllRentedGames(Guid? id, string sortBy, string search, int pageNumber, int pageSize)
        {
            IPagedList<RentedBoardGameDTO> listOfRentedBoardGames = await RentedBoardGameRepository.GetAllRentedGames(id, sortBy, search, pageNumber, pageSize);
            return listOfRentedBoardGames;
        }

        public async Task<RentedBoardGameDTO> GetRentedBoardGameById(Guid id)
        {
            RentedBoardGameDTO rentedBoardGameDTO = await RentedBoardGameRepository.GetRentedBoardGameById(id);
            return rentedBoardGameDTO;
        }

        public async Task<bool> MarkGameAsReturned(RentedBoardGameDTO rentedBoardGameDTO)
        {
            RentedBoardGameDTO checkRentedBoardGameDTO = await RentedBoardGameRepository.GetRentedBoardGameById(rentedBoardGameDTO.Id);
            if(checkRentedBoardGameDTO == null)
            {
                return false;
            }

            RentedBoardGameDTO rentedBoardGameToMarkReturn = new RentedBoardGameDTO
            {
                BoardGameId = rentedBoardGameDTO.BoardGameId == default ? checkRentedBoardGameDTO.BoardGameId : rentedBoardGameDTO.BoardGameId,
                Id = rentedBoardGameDTO.Id == default ? checkRentedBoardGameDTO.Id : rentedBoardGameDTO.Id,
                CoreUserId = rentedBoardGameDTO.CoreUserId == default ? checkRentedBoardGameDTO.CoreUserId : rentedBoardGameDTO.CoreUserId,
                CreatedByUserId = rentedBoardGameDTO.CoreUserId == default ? checkRentedBoardGameDTO.CoreUserId : rentedBoardGameDTO.CoreUserId,
                UpdatedByUserId = rentedBoardGameDTO.CoreUserId == default ? checkRentedBoardGameDTO.CoreUserId : rentedBoardGameDTO.CoreUserId,
                DateCreated = rentedBoardGameDTO.DateCreated == default ? checkRentedBoardGameDTO.DateCreated : rentedBoardGameDTO.DateCreated,
                DateUpdated = rentedBoardGameDTO.DateUpdated == default ? checkRentedBoardGameDTO.DateUpdated : rentedBoardGameDTO.DateUpdated,
                DateRented = rentedBoardGameDTO.DateRented == default ? checkRentedBoardGameDTO.DateRented : rentedBoardGameDTO.DateRented,
                DateReturned = rentedBoardGameDTO.DateReturned == default ? checkRentedBoardGameDTO.DateReturned : rentedBoardGameDTO.DateReturned,
                IsActive = rentedBoardGameDTO.IsActive,
            };

            bool isReturned = await RentedBoardGameRepository.MarkGameAsReturned(rentedBoardGameToMarkReturn);
            return isReturned;
        }

        public async Task<bool> RentBoardGame(RentedBoardGameDTO boardGameDTO)
        {
            bool isAdded = await RentedBoardGameRepository.RentBoardGame(boardGameDTO);
            return isAdded;
        }
    }
}
