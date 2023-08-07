using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository.Common
{
    public interface IRoleRepository
    {
        //Task<string[]> FindAsync();

        Task<List<RoleDTO>> FindAsync();
    }
}
