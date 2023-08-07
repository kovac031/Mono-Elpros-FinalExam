using PagedList;
using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service.Common
{
    public interface IBoardGameService
    {
        Task<IPagedList<BoardGameDTO>> GetBoardGames(string filterBy, string sortBy, string search, int pageNumber, int pageSize);
        Task<BoardGameDTO> GetBoardGame(Guid id);
        Task<bool> EditBoardGame(Guid id, BoardGameDTO boardGameDTO);
        Task<bool> CreateBoardGame(BoardGameDTO boardGameDTO);    
        Task<bool> DelteBoardGame(BoardGameDTO boardGameDTO);
    }
}
