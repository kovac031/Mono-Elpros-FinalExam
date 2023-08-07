using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service.Common
{
    public interface IUsersRoleProvider
    {
        string[] GetRolesForUser(string username);
        string[] GetAllRoles();
    }
}
