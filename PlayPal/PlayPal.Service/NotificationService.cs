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
    public class NotificationService : INotificationService
    {
        protected INotificationRepository Repository { get; set; }
        public NotificationService(INotificationRepository repository)
        {
            Repository = repository;
        }

        public async Task<IPagedList<NotificationDTO>> GetAllNotificationsAsync(Guid? id, int pagedNumber, int pageSize)
        {
            IPagedList<NotificationDTO> list = await Repository.GetAllNotificationsAsync(id, pagedNumber, pageSize);
            return list;
        }

        public async Task<NotificationDTO> GetOneNotificationAsync(Guid id)
        {
            NotificationDTO notification = await Repository.GetOneNotificationAsync(id);
            return notification;
        }

        public async Task<NotificationDTO> EditNotificationAsync(Guid id, NotificationDTO notificationDTO)
        {
            NotificationDTO notificationCheck = await Repository.GetOneNotificationAsync(id);

            if (notificationCheck == null)
            {
                return null;
            }


            NotificationDTO notificationToEdit = new NotificationDTO
            {
                Id = id,
                Title = notificationDTO.Title == default ? notificationCheck.Title : notificationDTO.Title,
                Message = notificationDTO.Message == default ? notificationCheck.Message : notificationDTO.Message,
                DateCreated = notificationDTO.DateCreated == default ? notificationCheck.DateCreated : notificationDTO.DateCreated,
                DateUpdated = notificationDTO.DateUpdated == default ? notificationCheck.DateUpdated : notificationDTO.DateUpdated,
                CreatedByUserId = notificationDTO.CreatedByUserId == default ? notificationCheck.CreatedByUserId : notificationDTO.CreatedByUserId,
                UpdatedByUserId = notificationDTO.UpdatedByUserId == default ? notificationCheck.UpdatedByUserId : notificationDTO.UpdatedByUserId,
                IsActive = notificationDTO.IsActive == default ? notificationCheck.IsActive : notificationDTO.IsActive
            };

            NotificationDTO notification = await Repository.EditNotificationAsync(id, notificationToEdit);

            return notification;
        }
        public async Task<NotificationDTO> HideDeleteNotificationAsync(Guid id, NotificationDTO notificationDTO)
        {
            NotificationDTO notification = await Repository.HideDeleteNotificationAsync(id, notificationDTO);
            return notification;
        }

        public async Task<NotificationDTO> AddNewNotificationAsync(NotificationDTO notificationDTO)
        {
            NotificationDTO notification = await Repository.AddNewNotificationAsync(notificationDTO);
            return notification;
        }
    }
}
