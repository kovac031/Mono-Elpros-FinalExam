using PagedList;
using PlayPal.ModelDTO;
using PlayPal.MVC.Models;
using PlayPal.Service;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlayPal.MVC.Controllers
{
    public class NotificationController : Controller
    {
    
        protected INotificationService Service { get; set; }

    
        public NotificationController(INotificationService service)
        {
            Service = service;
           
        }

        //--------------------------------------------------------------------
        //  --- GET ALL NOTIFICATIONS ------
        public async Task<ActionResult> GetAllNotificationsAsync(Guid? id, int? pageNumber, int? pageSize)
        {
            try
            {
                IPagedList<NotificationDTO> listDTO = await Service.GetAllNotificationsAsync(id, pageNumber ?? 1, pageSize ?? 10);
                if(listDTO == null)
                {
                    return View("Error");
                }

                List<NotificationVIEW> listVIEW = listDTO.Select(n => new NotificationVIEW()
                {
                    Id = n.Id,
                    Title = n.Title,
                    Message = n.Message,
                    DateCreated = n.DateCreated,
                    DateUpdated = n.DateUpdated,
                    IsActive = n.IsActive,
                }).ToList();

                var pagedList = new StaticPagedList<NotificationVIEW>(listVIEW, pageNumber ?? 1, pageSize ?? 10, listDTO.TotalItemCount);
                return await Task.FromResult(View(pagedList));
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        //--------------------------------------------------------------------
        //----- GET ONE NOTIFICATION BY ID --------------
        public async Task<ActionResult> GetOneNotificationAsync(Guid id)
        {
            NotificationDTO notificationDTO = await Service.GetOneNotificationAsync(id);
            NotificationVIEW notificationVIEW = new NotificationVIEW();
            notificationVIEW.Id = notificationDTO.Id;
            notificationVIEW.Title = notificationDTO.Title;
            notificationVIEW.Message = notificationDTO.Message;
            notificationVIEW.DateCreated = notificationDTO.DateCreated;
            notificationVIEW.DateUpdated = notificationDTO.DateUpdated;
            notificationVIEW.CreatedByUserId = notificationDTO.CreatedByUserId;
            notificationVIEW.UpdatedByUserId = notificationDTO.UpdatedByUserId;
            notificationVIEW.IsActive = notificationDTO.IsActive;

            return View(notificationVIEW);
        }

        //-----------------------------------------------------------------
        //------------- EDIT NOTIFICATION ------------
        public async Task<ActionResult> EditNotificationAsync(Guid id)
        {
            NotificationDTO notificationDTO = await Service.GetOneNotificationAsync(id);
            NotificationVIEW notificationVIEW = new NotificationVIEW();

            notificationVIEW.Id = notificationDTO.Id;
            notificationVIEW.Title = notificationDTO.Title;
            notificationVIEW.Message = notificationDTO.Message;
            //notificationVIEW.DateCreated = notificationDTO.DateCreated;
            notificationVIEW.DateUpdated = notificationDTO.DateUpdated;
            //notificationVIEW.CreatedByUserId = notificationDTO.CreatedByUserId;
            notificationVIEW.UpdatedByUserId = notificationDTO.UpdatedByUserId;
            notificationVIEW.IsActive = notificationDTO.IsActive;

            return View(notificationVIEW);
        }
        [HttpPost]
        public async Task<ActionResult> EditNotificationAsync(NotificationVIEW notificationVIEW)
        {
            var userId = "";
            if (User.Identity is FormsIdentity identity)
            {
                userId = identity.Ticket.UserData;
            }

            NotificationDTO notificationDTO = new NotificationDTO();

            notificationDTO.Id = notificationVIEW.Id;
            notificationDTO.Title = notificationVIEW.Title;
            notificationDTO.Message = notificationVIEW.Message;
            //notificationDTO.DateCreated = notificationVIEW.DateCreated;
            notificationDTO.DateUpdated = notificationVIEW.DateUpdated = DateTime.Now;
            //notificationDTO.CreatedByUserId = notificationVIEW.CreatedByUserId;
            notificationDTO.UpdatedByUserId = notificationVIEW.UpdatedByUserId = new Guid(userId);
            notificationDTO.IsActive = notificationVIEW.IsActive;

            await Service.EditNotificationAsync(notificationDTO.Id, notificationDTO);

            return RedirectToAction("GetAllNotificationsAsync");
        }

        //---------------------------------------------------------
        //---------- HIDE "DELETE" NOTIFICATION -------------
        public async Task<ActionResult> HideDeleteNotificationAsync(Guid id)
        {
            NotificationDTO notificationDTO = await Service.GetOneNotificationAsync(id);
            NotificationVIEW notificationVIEW = new NotificationVIEW();

            notificationVIEW.Id = notificationDTO.Id;
            //notificationVIEW.Title = notificationDTO.Title;
            //notificationVIEW.Message = notificationDTO.Message;
            //notificationVIEW.DateCreated = notificationDTO.DateCreated;
            //notificationVIEW.DateUpdated = notificationDTO.DateUpdated;
            //notificationVIEW.CreatedByUserId = notificationDTO.CreatedByUserId;
            //notificationVIEW.UpdatedByUserId = notificationDTO.UpdatedByUserId;
            notificationVIEW.IsActive = notificationDTO.IsActive;

            return View(notificationVIEW);
        }
        [HttpPost]
        public async Task<ActionResult> HideDeleteNotificationAsync(NotificationVIEW notificationVIEW)
        {
            NotificationDTO notificationDTO = new NotificationDTO();

            notificationDTO.Id = notificationVIEW.Id;
            //notificationDTO.Title = notificationVIEW.Title;
            //notificationDTO.Message = notificationVIEW.Message;
            //notificationDTO.DateCreated = notificationVIEW.DateCreated;
            //notificationDTO.DateUpdated = notificationVIEW.DateUpdated;
            //notificationDTO.CreatedByUserId = notificationVIEW.CreatedByUserId;
            //notificationDTO.UpdatedByUserId = notificationVIEW.UpdatedByUserId;
            notificationDTO.IsActive = notificationVIEW.IsActive;

            await Service.HideDeleteNotificationAsync(notificationDTO.Id, notificationDTO);

            return View();
        }

        //----------------------------------------------------------------------
        // ------------ ADD NEW NOTIFICATION ---------------
        public async Task<ActionResult> AddNewNotificationAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewNotificationAsync(NotificationVIEW notificationVIEW)
        {

            var userId = "";
            if (User.Identity is FormsIdentity identity)
            {
                userId = identity.Ticket.UserData;
            }

            NotificationDTO notificationDTO = new NotificationDTO();

            notificationDTO.Id = notificationVIEW.Id = Guid.NewGuid();
            notificationDTO.Title = notificationVIEW.Title;
            notificationDTO.Message = notificationVIEW.Message;
            notificationDTO.DateCreated = notificationVIEW.DateCreated = DateTime.Now;
            notificationDTO.DateUpdated = notificationVIEW.DateUpdated = DateTime.Now;
            notificationDTO.CreatedByUserId = notificationVIEW.CreatedByUserId = new Guid(userId);
            notificationDTO.UpdatedByUserId = notificationVIEW.UpdatedByUserId = new Guid(userId);
            notificationDTO.IsActive = notificationVIEW.IsActive = true;

            await Service.AddNewNotificationAsync(notificationDTO);



            return RedirectToAction("GetAllNotificationsAsync");
        }
    }
}
