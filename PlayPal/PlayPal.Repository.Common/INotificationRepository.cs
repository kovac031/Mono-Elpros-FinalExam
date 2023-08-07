using PagedList;
using PlayPal.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository.Common
{
    public interface INotificationRepository
    {
        Task<IPagedList<NotificationDTO>> GetAllNotificationsAsync(Guid? id, int pageNumber, int pageSize);
        Task<NotificationDTO> GetOneNotificationAsync(Guid id);
        Task<NotificationDTO> EditNotificationAsync(Guid id, NotificationDTO notification);
        Task<NotificationDTO> HideDeleteNotificationAsync(Guid id, NotificationDTO notificationDTO);
        Task<NotificationDTO> AddNewNotificationAsync(NotificationDTO notificationDTO);
    }
}
