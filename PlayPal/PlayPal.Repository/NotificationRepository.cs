using PagedList;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        protected EFContext Context { get; set; }
        public NotificationRepository(EFContext context)
        {
            Context = context;
        }

        //---------------------------------------------------------------------
        //------- GET ALL -----
        public async Task<IPagedList<NotificationDTO>> GetAllNotificationsAsync(Guid? id, int pagedNumber, int pageSize)
        {
            var query = Context.Notification.Select(n => new
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                DateCreated = n.DateCreated,
                DateUpdated = n.DateUpdated,
                CreatedByUserId = n.CreatedByUserId,
                UpdatedByUserId = n.UpdatedByUserId,
                IsActive = n.IsActive
            }).OrderBy(n => n.IsActive);

            var result = query.ToPagedList(pagedNumber, pageSize);

            List<NotificationDTO> list = result.Select(n => new NotificationDTO()
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                DateCreated = n.DateCreated,
                DateUpdated = n.DateUpdated,
                CreatedByUserId = n.CreatedByUserId,
                UpdatedByUserId = n.UpdatedByUserId,
                IsActive = n.IsActive
            }).ToList();

            var pagedList = new StaticPagedList<NotificationDTO>(list, pagedNumber, pageSize, result.Count);

            return await Task.FromResult(pagedList);
        }

        //-----------------------------------------------------------------------
        //----- GET BY ID ------
        public async Task<NotificationDTO> GetOneNotificationAsync(Guid id)
        {
            NotificationDTO notification;

            notification = await Context.Notification.Where(n => n.Id == id).Select(n => new NotificationDTO()
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                DateCreated = n.DateCreated,
                DateUpdated = n.DateUpdated,
                CreatedByUserId = n.CreatedByUserId,
                UpdatedByUserId = n.UpdatedByUserId,
                IsActive = n.IsActive
            }).FirstOrDefaultAsync<NotificationDTO>();

            return notification;
        }

        //-----------------------------------------------------------------------
        //----- EDIT  ------
        public async Task<NotificationDTO> EditNotificationAsync(Guid id, NotificationDTO notificationDTO)
        {
            Notification notification = await Context.Notification.Where(n => n.Id == id).FirstOrDefaultAsync();
            if (notification != null)
            {
                notification.Id = notificationDTO.Id;
                notification.Title = notificationDTO.Title;
                notification.Message = notificationDTO.Message;
                //notification.DateCreated = notificationDTO.DateCreated;
                notification.DateUpdated = notificationDTO.DateUpdated;
                //notification.CreatedByUserId = notificationDTO.CreatedByUserId;
                notification.UpdatedByUserId = notificationDTO.UpdatedByUserId;
                notification.IsActive = notificationDTO.IsActive;

                Context.SaveChanges();
            }
            else
            {
                return (null);
            }

            return notificationDTO;
        }

        //------------------------------------------------------------------
        //------- HIDE "DELETE" --------

        public async Task<NotificationDTO> HideDeleteNotificationAsync(Guid id, NotificationDTO notificationDTO)
        {
            Notification notification = await Context.Notification.Where(n => n.Id == id && n.IsActive == true).FirstOrDefaultAsync();

            notification.Id = notificationDTO.Id;
            //notification.Title = notificationDTO.Title;
            //notification.Message = notificationDTO.Message;
            //notification.DateCreated = notificationDTO.DateCreated;
            //notification.DateUpdated = notificationDTO.DateUpdated;
            //notification.CreatedByUserId = notificationDTO.CreatedByUserId;
            //notification.UpdatedByUserId = notificationDTO.UpdatedByUserId;
            notification.IsActive = notificationDTO.IsActive;

            Context.SaveChanges();

            await Context.SaveChangesAsync();

            return notificationDTO;
        }

        //--------------------------------------------------------------------
        //----------- ADD NEW  ------

        public async Task<NotificationDTO> AddNewNotificationAsync(NotificationDTO notificationDTO)
        {
            Context.Notification.Add(new Notification()
            {
                Id = notificationDTO.Id,
                Title = notificationDTO.Title,
                Message = notificationDTO.Message,
                DateCreated = notificationDTO.DateCreated,
                DateUpdated = notificationDTO.DateUpdated,
                CreatedByUserId = notificationDTO.CreatedByUserId,
                UpdatedByUserId = notificationDTO.UpdatedByUserId,
                IsActive = notificationDTO.IsActive = true
            });

            await Context.SaveChangesAsync();
            return notificationDTO;
        }
    }
}
