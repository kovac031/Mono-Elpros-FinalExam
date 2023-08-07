using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlayPal.Service
{
    public class RoleService : IRoleService
    {

        protected IRoleRepository RoleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            RoleRepository = roleRepository;
        }
        public async Task<List<RoleDTO>> FindAsync()
        {

            List<RoleDTO> roles = await RoleRepository.FindAsync();

            return roles;
        }
    }
}
