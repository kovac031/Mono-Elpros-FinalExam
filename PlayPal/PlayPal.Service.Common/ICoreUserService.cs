using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service.Common
{
    public interface ICoreUserService
    {
        Task<CoreUserDTO> FindAsync(string username, string password);
        Task<CoreUserDTO> PostAsync(CoreUserDTO coreUserDTO);

        //Task<string[]> GetRolesForUserAsync(string username);
    }

    
}
