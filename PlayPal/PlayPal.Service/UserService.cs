using PagedList;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service
{
    public class UserService : IUserService
    {

        protected IUserRepository UserRepository;


        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<IPagedList<CoreUserDTO>> FindAsync(string sortBy, string sortOrder, int? pageNumber, int? pageSize, string searchString = null)
        {

            IPagedList<CoreUserDTO> users = await UserRepository.FindAsync(sortBy, sortOrder, pageNumber, pageSize, searchString);

            return users;
        }

        public async Task<CoreUserDTO> GetByIdAsync(Guid id)
        {

            CoreUserDTO user = await UserRepository.GetByIdAsync(id);

            return user;
        }


        public async Task<bool> DeactivateAsync(Guid id)
        {

            bool user = await UserRepository.DeactivateAsync(id);

            return user;
        }

        public async Task<bool> PutAsync(Guid id, CoreUserDTO user)
        {


            CoreUserDTO userExist = await UserRepository.GetByIdAsync(id);

            if (userExist == null)
            {
                return false;
            }

            bool userCheck = await UserRepository.PutAsync(id, user);

            return userCheck;
        }


        public async Task<bool> UpdatePersonalInfoAsync(Guid id, UsersDetailDTO user)
        {


            UsersDetailDTO userExist = await UserRepository.GetByIdAsync(id);

            if (userExist == null)
            {
                return false;
            }

            UsersDetailDTO userDetailsToEdit = new UsersDetailDTO
            {
                Id = id,
                FirstName = user.FirstName == default ? userExist.FirstName : user.FirstName,
                LastName = user.LastName == default ? userExist.LastName : user.LastName,
                DOB = user.DOB == default ? userExist.DOB : user.DOB,
                Address = user.Address == default ? userExist.Address : user.Address,
                DateUpdated = user.DateUpdated == default ? userExist.DateUpdated : user.DateUpdated,
                CreatedByUserId = user.CreatedByUserId == default ? userExist.CreatedByUserId : user.CreatedByUserId,
                UpdatedByUserId = user.UpdatedByUserId == default ? userExist.UpdatedByUserId : user.UpdatedByUserId,
                IsActive = user.IsActive == default ? userExist.IsActive : user.IsActive
            };

            bool userCheck = await UserRepository.UpdatePersonalInfoAsync(id, userDetailsToEdit);

            return userCheck;
        }

    }
}
