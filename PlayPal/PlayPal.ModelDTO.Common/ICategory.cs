using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.ModelDTO.Common
{
    public interface ICategory
    {
        System.Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        System.DateTime DateCreated { get; set; }
        System.DateTime DateUpdated { get; set; }
        System.Guid CreatedByUserId { get; set; }
        System.Guid UpdatedByUserId { get; set; }
        bool IsActive { get; set; }
    }
}
