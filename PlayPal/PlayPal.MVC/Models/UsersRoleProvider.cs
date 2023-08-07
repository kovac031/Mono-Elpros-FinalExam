using PlayPal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PlayPal.MVC;
using PlayPal.Service.Common;
using System.Threading.Tasks;

namespace PlayPal.MVC.Models
{
    public class UsersRoleProvider : RoleProvider
    {
        public UsersRoleProvider() : base()
        {

        }

        protected ICoreUserService CoreUserService;

        public UsersRoleProvider(ICoreUserService coreUserService)
        {
            CoreUserService = coreUserService;
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
        }
        public override string[] GetRolesForUser(string username)
        {

            //string[] roles = Task.Run(async () => await CoreUserService.GetRolesForUserAsync(username)).Result;
            //return roles;
            using (EFContext context = new EFContext())
            {
                var user = context.CoreUser.Include("Role").FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    return new string[] { user.Role.Name };
                }
                else
                {
                    return new string[0];
                }
            }
        }
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