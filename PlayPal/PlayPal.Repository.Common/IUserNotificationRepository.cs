using PagedList;
using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository.Common
{
    public interface IUserNotificationRepository
    {
        Task<IPagedList<UserNotificationDTO>> GetUserNotificationsAsync(Guid? id, int pageNumber, int pageSize);
        Task<UserNotificationDTO> GetUserNotificationAsync(Guid id);
        Task<bool> MarkAsRead(UserNotificationDTO userNotificationDTO);
    }
}
