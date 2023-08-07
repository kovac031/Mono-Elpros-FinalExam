using PlayPal.ModelDTO;
using PlayPal.MVC.Models;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PlayPal.DAL;
using PagedList;
using PlayPal.Common;

namespace PlayPal.MVC.Controllers
{
    public class AccountController : Controller
    {
        protected ICoreUserService CoreUserService;
        protected IUserNotificationService UserNotificationService { get; set; }

        public AccountController(ICoreUserService coreUserService, IUserNotificationService userNotificationService)
        {
            CoreUserService = coreUserService;
            UserNotificationService = userNotificationService;
        }

        /*--------------------------------------------------
         * Login
         *-------------------------------------------------*/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(CoreUserLoginView coreUserLoginView)
        {
            CoreUserDTO user = await CoreUserService.FindAsync(coreUserLoginView.Username, coreUserLoginView.Password);
            if (user != null)
            {
                var userData = user.Id.ToString(); 
                var authTicket = new FormsAuthenticationTicket(
                    1,
                    coreUserLoginView.Username,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                    false,
                    userData);

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                IPagedList<UserNotificationDTO> userNotifications = await UserNotificationService.GetUserNotificationsAsync(user.Id, 1, 10);

                List<UserNotificationDTO> filterByIsRead = userNotifications.Where(x => x.IsRead == false).ToList();
                if (filterByIsRead.Count > 0)
                {
                    return RedirectToAction("GetAllUserNotification", "UserNotification");
                }      
                return RedirectToAction("", "");
            }
            ModelState.AddModelError("", "Invalid Username or Password");
            return View();
        }


        /*--------------------------------------------------
        * Logout
        *-------------------------------------------------*/

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        /*--------------------------------------------------
        * Signup
        *-------------------------------------------------*/

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Signup(CoreUserSignUpView coreUserSignUpView)
        {

            if(ModelState.IsValid)
            {
                CoreUserDTO coreUserDTO = new CoreUserDTO();

                coreUserDTO.UsersDetail = new UsersDetailDTO
                {
                    FirstName = coreUserSignUpView.FirstName,
                    LastName = coreUserSignUpView.LastName,
                    Address = coreUserSignUpView.Address,
                    DOB = coreUserSignUpView.DOB
                };

                coreUserDTO.Username = coreUserSignUpView.Username;
                coreUserDTO.Email = coreUserSignUpView.Email;
                coreUserDTO.Password = coreUserSignUpView.Password;


                await CoreUserService.PostAsync(coreUserDTO);

                return RedirectToAction("Login");
            }

            return View(coreUserSignUpView);

        }


    }
   


}