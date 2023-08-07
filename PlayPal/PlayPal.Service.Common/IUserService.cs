using PagedList;
using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service.Common
{
    public interface IUserService
    {
        Task<IPagedList<CoreUserDTO>> FindAsync(string sortBy, string sortOrder, int? pageNumber, int? pageSize, string searchString = null);
        Task<CoreUserDTO> GetByIdAsync(Guid id);
        Task<bool> DeactivateAsync(Guid id);

        Task<bool> PutAsync(Guid id, CoreUserDTO user);

        Task<bool> UpdatePersonalInfoAsync(Guid id, UsersDetailDTO user);
    }
}
