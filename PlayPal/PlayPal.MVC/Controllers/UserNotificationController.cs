using PagedList;
using PlayPal.ModelDTO;
using PlayPal.MVC.Models;
using PlayPal.MVC.Models.RentedBoardGameView;
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
    public class UserNotificationController : Controller
    {
		IUserNotificationService UserNotificationService { get; set; }
        public UserNotificationController(IUserNotificationService userNotificationService)
        {
            UserNotificationService = userNotificationService;
        }

        public async Task<ActionResult> GetAllUserNotification(int? pageNumber, int? pageSize)
        {
			try
			{
                var userId = "";
                if (User.Identity is FormsIdentity identity)
                {
                    userId = identity.Ticket.UserData;
                }

                IPagedList<UserNotificationDTO> userNotifications = await UserNotificationService.GetUserNotificationsAsync(new Guid(userId), pageNumber ?? 1, pageSize ?? 10);
			
                if(userNotifications == null)
                {
                    return await Task.FromResult(View("Error"));
                }
                
                List<UserNotificationView> userNotificationViews = userNotifications.Select(un => new UserNotificationView() 
                {
                    Id = un.Id,
                    DateCreated = un.DateCreated,
                    NotificationMessage = un.NotificationContent,
                    NotificationTitle = un.NotificationTitle,
                    IsRead = un.IsRead,

                }).ToList();
                var pagedList = new StaticPagedList<UserNotificationView>(userNotificationViews, pageNumber ?? 1, pageSize ?? 10, userNotifications.TotalItemCount);
                return await Task.FromResult(View(pagedList));
            }
			catch (Exception)
			{
				return await Task.FromResult(View("Error"));
            }
        }
        [HttpGet]
        public async Task<ActionResult> Read(Guid id)
        {
            try
            {
                UserNotificationDTO userNotificationDTO = await UserNotificationService.GetUserNotificationAsync(id);
                if(userNotificationDTO == null )
                {
                    return await Task.FromResult(View("Error"));
                }
                UserNotificationReadView userNotificationReadView = new UserNotificationReadView
                {
                    IsRead = userNotificationDTO.IsRead,
                    UserNotificationId = id
                };
                return View(userNotificationReadView);
            }
            catch (Exception)
            {
                return await Task.FromResult(View("Error"));
            }
        }


        [HttpPost, ActionName("Read")]
        public async Task<ActionResult> Read(UserNotificationReadView userNotification)
        {
            try
            {
                var userId = "";
                if (User.Identity is FormsIdentity identity)
                {
                    userId = identity.Ticket.UserData;
                }
                UserNotificationDTO userNotificationDTO = new UserNotificationDTO
                {
                    Id = userNotification.UserNotificationId,
                    DateUpdated = DateTime.Now,
                    UpdatedByUserId = new Guid(userId),
                    IsRead = true,
                    
                };
                bool isRead = await UserNotificationService.MarkAsRead(userNotificationDTO);
                if (isRead)
                {
                    return RedirectToAction("GetAllUserNotification");
                }
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}