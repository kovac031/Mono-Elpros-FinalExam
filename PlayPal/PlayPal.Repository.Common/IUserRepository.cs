using PagedList;
using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;

namespace PlayPal.Repository.Common
{
    public interface IUserRepository
    {

        Task<IPagedList<CoreUserDTO>> FindAsync(string sortBy, string sortOrder, int? pageNumber, int? pageSize, string searchString = null);
        Task<CoreUserDTO> GetByIdAsync(Guid id);
        Task<bool> DeactivateAsync(Guid id);
        Task<bool> PutAsync(Guid id, CoreUserDTO user);
        Task<bool> UpdatePersonalInfoAsync(Guid id, UsersDetailDTO user);
        
    }
}
