using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;

namespace PlayPal.Service
{

    public class UsersRoleProvider : RoleProvider


    {
        protected IRoleRepository RoleRepository;

        public UsersRoleProvider(IRoleRepository roleRepository)
        {
            RoleRepository = roleRepository;
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();

            //string[] roles = Task.Run(async () => await RoleRepository.FindAsync()).Result;

            //return roles;
        }
        public override string[] GetRolesForUser(string username)
        {

            throw new NotImplementedException();
            //string[] role = Task.Run(async () => await UserRepository.GetRolesForUser(username)).Result;
            //return role;
        }
        //using (EFContext context = new EFContext())
        //{
        //    var user = context.CoreUser.Include("Role").FirstOrDefault(u => u.Username == username);
        //    if (user != null)
        //    {
        //        return new string[] { user.Role.Name };
        //    }
        //    else
        //    {
        //        return new string[0];
        //    }
        //}

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}