using PlayPal.Common;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service
{
    public class CoreUserService : ICoreUserService
    {

        protected ICoreUserRepository CoreUserRepository;


        public CoreUserService(ICoreUserRepository coreUserRepository)
        {
            CoreUserRepository = coreUserRepository;
        }

        public async Task<CoreUserDTO> FindAsync(string userName, string password)
        {
            CoreUserDTO coreUser = await CoreUserRepository.FindAsync(userName, password);

            return coreUser;
        }

        public async Task<CoreUserDTO> PostAsync(CoreUserDTO coreUserDTO)
        {

            string hashedPassword = PasswordHasher.HashPassword(coreUserDTO.Password);
            coreUserDTO.Password = hashedPassword;

            coreUserDTO = await CoreUserRepository.PostAsync(coreUserDTO);

            return coreUserDTO;
        }


        //public async Task<string[]> GetRolesForUserAsync(string username)
        //{
        //    var roles = await CoreUserRepository.GetRolesForUserAsync(username);
        //    return roles;
        //}


    }
}
