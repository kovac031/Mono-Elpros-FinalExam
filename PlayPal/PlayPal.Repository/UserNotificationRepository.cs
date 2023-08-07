using PagedList;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.ModelDTO.Common;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        protected EFContext Context { get; set; }

        public UserNotificationRepository(EFContext context)
        {
            Context = context;
        }

        public async Task<IPagedList<UserNotificationDTO>> GetUserNotificationsAsync(Guid? id, int pageNumber, int pageSize)
        {
            try
            {
                var query = Context.UserNotification.
                Where(un => un.CoreUserId == id).
                Join(Context.Notification, un => un.NotificationId, n => n.Id, (un, n) => new { un, n }).
                OrderBy(x => x.un.IsRead).
                Select(x => new
                {
                    Id = x.un.Id,
                    NotificationTitle = x.n.Title,
                    NotificationContent = x.n.Message,
                    IsRead = x.un.IsRead,
                    DateCreated = x.un.DateCreated,
                    IsActive = x.un.IsActive

                });

                var result = query.ToPagedList(pageNumber, pageSize);

                List<UserNotificationDTO> list = result.Select(un => new UserNotificationDTO()
                {
                    Id = un.Id,
                    DateCreated = un.DateCreated,
                    IsRead = un.IsRead,
                    IsActive = un.IsActive,
                    NotificationTitle = un.NotificationTitle,
                    NotificationContent = un.NotificationContent,

                }).ToList();
                var pagedList = new StaticPagedList<UserNotificationDTO>(list, pageNumber, pageSize, result.TotalItemCount);
                return await Task.FromResult(pagedList);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<bool> MarkAsRead(UserNotificationDTO userNotificationDTO)
        {
            try
            {
                var query = Context.UserNotification.AsQueryable();

                UserNotification userNotificationDal = await query.FirstOrDefaultAsync(un => un.Id == userNotificationDTO.Id);
                if (userNotificationDal == null)
                {
                    return false;
                }
                userNotificationDal.Id = userNotificationDTO.Id;
                userNotificationDal.DateCreated = userNotificationDTO.DateCreated;
                userNotificationDal.DateUpdated = userNotificationDTO.DateUpdated;
                userNotificationDal.CreatedByUserId = userNotificationDTO.CreatedByUserId;
                userNotificationDal.UpdatedByUserId = userNotificationDTO.UpdatedByUserId;
                userNotificationDal.IsActive = userNotificationDTO.IsActive;
                userNotificationDal.CoreUserId = userNotificationDTO.CoreUserId;
                userNotificationDal.NotificationId = userNotificationDTO.NotificationId;
                userNotificationDal.IsRead = userNotificationDTO.IsRead;

                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<UserNotificationDTO> GetUserNotificationAsync(Guid id)
        {
            try
            {
                UserNotification userNotification = await Context.UserNotification.Where(un => un.Id == id).FirstOrDefaultAsync();
                if (userNotification == null)
                {
                    return null;
                }
                UserNotificationDTO userNotificationDTO = new UserNotificationDTO
                {
                    Id = userNotification.Id,
                    DateCreated = userNotification.DateCreated,
                    DateUpdated = userNotification.DateUpdated,
                    CreatedByUserId = userNotification.CreatedByUserId,
                    UpdatedByUserId = userNotification.UpdatedByUserId,
                    IsActive = userNotification.IsActive,
                    CoreUserId = userNotification.CoreUserId,
                    NotificationId = userNotification.NotificationId,
                    IsRead = userNotification.IsRead
                };
                return userNotificationDTO;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}