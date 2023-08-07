using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository
{
    public class RoleRepository : IRoleRepository
    {
        protected EFContext EFContext;

        public RoleRepository(EFContext efContext)
        {
            EFContext = efContext;
        }
      
        public async Task<List<RoleDTO>> FindAsync()
        {
            var query = EFContext.Role.AsQueryable();
                       
            var roles = await query.ToListAsync();

            if (roles.Count == 0)
            {
                return null;
            }

            var roleDTOs = roles.Select(role => new RoleDTO
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();

            return roleDTOs;
        }
    }
}
