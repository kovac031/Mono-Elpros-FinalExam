using Microsoft.AspNet.Identity;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository
{
    public class EFCoreUserRepository : ICoreUserRepository
    {

        protected EFContext EFContext;

        public EFCoreUserRepository(EFContext efContext)
        {
            EFContext = efContext;
        }


        public async Task<CoreUserDTO> FindAsync(string username, string password)
        {
            string hashedInputPassword = PlayPal.Common.PasswordHasher.HashPassword(password);

            var query = await EFContext.CoreUser
                .Where(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == hashedInputPassword)
                .Select(u => new CoreUserDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email

                })
                .FirstOrDefaultAsync();

            return query;
        }

        public async Task<CoreUserDTO> PostAsync(CoreUserDTO coreUserDTO)
        {
            Role userRole = await EFContext.Role.FirstOrDefaultAsync(role => role.Name == "User");

            Guid roleId = userRole.Id;

            Guid newId = Guid.NewGuid();

            CoreUser newCoreUser = new CoreUser
            {
                Id = newId,
                Username = coreUserDTO.Username,
                Password = coreUserDTO.Password,
                Email = coreUserDTO.Email,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                CreatedByUserId = newId,
                UpdatedByUserId = newId,
                IsActive = true,
                RoleId = roleId
            };

            EFContext.CoreUser.Add(newCoreUser);

            UsersDetail newUserDetails = new UsersDetail
            {
                Id = newId,
                FirstName = coreUserDTO.UsersDetail.FirstName,
                LastName = coreUserDTO.UsersDetail.LastName,
                Address = coreUserDTO.UsersDetail.Address,
                DOB = coreUserDTO.UsersDetail.DOB,
                DateCreated = newCoreUser.DateCreated,
                DateUpdated = newCoreUser.DateUpdated,
                CreatedByUserId = newCoreUser.CreatedByUserId,
                UpdatedByUserId = newCoreUser.UpdatedByUserId,
                IsActive = true,

            };

            EFContext.UsersDetail.Add(newUserDetails);

            int rowsAffected = await EFContext.SaveChangesAsync();

            if (rowsAffected > 0)
            {
                coreUserDTO.Id = newCoreUser.Id;
                return coreUserDTO;
            }

            return null;

        }

        //public async Task<string[]> GetRolesForUserAsync(string username)
        //{
        //    var user = await EFContext.CoreUser.Include("Role").FirstOrDefaultAsync(u => u.Username == username);
        //    if (user != null)
        //    {
        //        return new string[] { user.Role.Name };
        //    }
        //    else
        //    {
        //        return new string[0];
        //    }
        //}


    }

}
