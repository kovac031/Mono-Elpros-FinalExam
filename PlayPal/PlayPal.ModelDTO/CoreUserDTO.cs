using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayPal.ModelDTO.Common;


namespace PlayPal.ModelDTO
{
    public class CoreUserDTO : UsersDetailDTO, ICoreUserDTO

    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsActive { get; set; }

        public Guid  CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public Guid RoleId { get; set; }

        
        public UsersDetailDTO UsersDetail { get; set; }

        



    }
}
