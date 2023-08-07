using PagedList;
using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository.Common
{
    public interface IRentedBoardGameRepository
    {
        Task<IPagedList<RentedBoardGameDTO>> GetAllRentedGames(Guid? id, string sortBy, string search, int pageNumber, int pageSize);    
        Task<bool> RentBoardGame(RentedBoardGameDTO rentedBoardGameDTO);
        Task<RentedBoardGameDTO> GetRentedBoardGameById(Guid id);
        Task<bool> MarkGameAsReturned(RentedBoardGameDTO rentedBoardGameDTO);
    }
}
