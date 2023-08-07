using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service.Common
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> FindAsync();
    }
}
