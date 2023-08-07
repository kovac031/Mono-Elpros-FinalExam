using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.ModelDTO.Common;
using PlayPal.Repository.Common;


namespace PlayPal.Repository
{
    public class UserRepository : IUserRepository
    {

        protected EFContext EFContext;

        public UserRepository(EFContext efContext)
        {
            EFContext = efContext;
        }

        public async Task<IPagedList<CoreUserDTO>> FindAsync(string sortBy, string sortOrder, int? pageNumber, int? pageSize, string searchString = null)
        {
            
            IQueryable<CoreUser> query = EFContext.CoreUser.AsQueryable();

            query = query.Where(user => user.IsActive);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(user => user.Username.Contains(searchString) || user.Email.Contains(searchString));
            }

            sortOrder = sortOrder?.ToLower() ?? "asc";

            switch (sortBy)
            {
                case "Username":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(user => user.Username)
                        : query.OrderBy(user => user.Username);
                    break;
                case "Email":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(user => user.Email)
                        : query.OrderBy(user => user.Email);
                    break;
                default:
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(user => user.Username)
                        : query.OrderBy(user => user.Username);
                    break;
            }

            int currentPage = pageNumber ?? 1;
            int itemsPerPage = pageSize ?? 5;

            int totalCount = await query.CountAsync();
            var results = await query.Skip((currentPage - 1) * itemsPerPage)
                                     .Take(itemsPerPage)
                                     .Select(user => new CoreUserDTO
                                     {
                                         Id = user.Id,
                                         Username = user.Username,
                                         Email = user.Email
                                     }).ToListAsync();

            var pagedList = new StaticPagedList<CoreUserDTO>(results, currentPage, itemsPerPage, totalCount);

            return pagedList;

        }


        public async Task<CoreUserDTO> GetByIdAsync(Guid id)
        {
            CoreUserDTO query = await EFContext.CoreUser
                .Join(
                    EFContext.UsersDetail,
                    coreUser => coreUser.Id,
                    userDetails => userDetails.Id,
                    (coreUser, userDetails) => new { CoreUser = coreUser, UserDetails = userDetails }
                )
                .Where(joined => joined.CoreUser.Id == id && joined.CoreUser.IsActive && joined.UserDetails.IsActive)
                .Select(joined => new CoreUserDTO
                {
                    Id = joined.CoreUser.Id,
                    Username = joined.CoreUser.Username,
                    Email = joined.CoreUser.Email,
                    FirstName = joined.UserDetails.FirstName,
                    LastName = joined.UserDetails.LastName,
                    DOB = joined.UserDetails.DOB,
                    Address = joined.UserDetails.Address,
                    DateCreated = joined.CoreUser.DateCreated,
                    DateUpdated = joined.CoreUser.DateUpdated
                })
                .FirstOrDefaultAsync();

            return query;


        }


        public async Task<bool> DeactivateAsync(Guid id)
        {
            IQueryable<CoreUser> coreUserQuery = EFContext.CoreUser.AsQueryable();
            IQueryable<UsersDetail> usersDetailQuery = EFContext.UsersDetail.AsQueryable();

            var joinedData = from coreUser in coreUserQuery
                             join usersDetail in usersDetailQuery on coreUser.Id equals usersDetail.Id
                             where coreUser.Id == id
                             select new { CoreUser = coreUser, UsersDetail = usersDetail };

            var userData = await joinedData.FirstOrDefaultAsync();

            if (userData != null)
            {
                userData.CoreUser.IsActive = false;
                userData.UsersDetail.IsActive = false;  
                await EFContext.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<bool> PutAsync(Guid id, CoreUserDTO user)
        {
            IQueryable<CoreUser> userQuery = EFContext.CoreUser.AsQueryable();
            IQueryable<Role> roleQuery = EFContext.Role.AsQueryable();

            CoreUser existingCoreUser = await userQuery.FirstOrDefaultAsync(c => c.Id == id);
            Role existingRole = await roleQuery.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            if (existingCoreUser != null && existingRole != null)
            {

                existingCoreUser.RoleId = existingRole.Id;

                await EFContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePersonalInfoAsync(Guid id, UsersDetailDTO userDetailDTO)
        {
            UsersDetail existingUserDetail = await EFContext.UsersDetail.FindAsync(id);

            if (existingUserDetail != null)
            {
                existingUserDetail.FirstName = userDetailDTO.FirstName;
                existingUserDetail.LastName = userDetailDTO.LastName;
                existingUserDetail.DOB = userDetailDTO.DOB;
                existingUserDetail.Address = userDetailDTO.Address;
                existingUserDetail.UpdatedByUserId = userDetailDTO.UpdatedByUserId;
                existingUserDetail.DateUpdated = userDetailDTO.DateUpdated;

                await EFContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
