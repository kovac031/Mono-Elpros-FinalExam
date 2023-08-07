using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayPal.MVC.Models
{
    public class CoreUserView : UsersDetailView
    {
        public Guid Id { get; set; }
        public string Username { get; set; }       
        public string Email { get; set; }
                
        public Guid SelectedRole { get; set; }
        public List<RoleView> AllRoles { get; set; }
       
    }
}