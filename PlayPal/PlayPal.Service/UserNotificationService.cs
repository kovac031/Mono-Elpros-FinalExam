using PagedList;
using PlayPal.ModelDTO;
using PlayPal.Repository;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service
{
    public class UserNotificationService : IUserNotificationService
    {
        protected IUserNotificationRepository UserNotificationRepository { get; set; }
        public UserNotificationService(IUserNotificationRepository userNotificationRepository)
        {
            UserNotificationRepository = userNotificationRepository;
        }


        public async Task<IPagedList<UserNotificationDTO>> GetUserNotificationsAsync(Guid? id, int pageNumber, int pageSize)
        {
            IPagedList<UserNotificationDTO>  pagedList = await UserNotificationRepository.GetUserNotificationsAsync(id, pageNumber, pageSize);
            return pagedList;
        }

        public async Task<bool> MarkAsRead(UserNotificationDTO userNotificationDTO)
        {
            UserNotificationDTO checkUserNotification = await GetUserNotificationAsync(userNotificationDTO.Id);
            if (checkUserNotification == null)
            {
                return false;
            }
            UserNotificationDTO editUserNotification = new UserNotificationDTO
            {
                Id = userNotificationDTO.Id,
                DateCreated = userNotificationDTO.DateCreated == default ? checkUserNotification.DateCreated : userNotificationDTO.DateCreated,
                DateUpdated = userNotificationDTO.DateUpdated == default ? checkUserNotification.DateUpdated : userNotificationDTO.DateUpdated,
                CreatedByUserId = userNotificationDTO.CreatedByUserId == default ? checkUserNotification.CreatedByUserId : userNotificationDTO.CreatedByUserId,
                UpdatedByUserId = userNotificationDTO.UpdatedByUserId == default ? checkUserNotification.UpdatedByUserId : userNotificationDTO.UpdatedByUserId,
                IsActive = userNotificationDTO.IsActive == default ? checkUserNotification.IsActive : userNotificationDTO.IsActive,
                CoreUserId = userNotificationDTO.CoreUserId == default ? checkUserNotification.CoreUserId : userNotificationDTO.CoreUserId,
                NotificationId = userNotificationDTO.NotificationId == default ? checkUserNotification.NotificationId : userNotificationDTO.NotificationId,
                IsRead = true
            };

            bool isRead = await UserNotificationRepository.MarkAsRead(editUserNotification);
            return isRead;
        }

        public async Task<UserNotificationDTO> GetUserNotificationAsync(Guid id)
        {
            UserNotificationDTO userNotification = await UserNotificationRepository.GetUserNotificationAsync(id);
            return userNotification;
        }
    }
}
