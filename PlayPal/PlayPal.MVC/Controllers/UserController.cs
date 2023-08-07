using PagedList;
using PlayPal.Common;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.MVC.Models;
using PlayPal.Service;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Security;

namespace PlayPal.MVC.Controllers
{
    public class UserController : Controller
    {
        protected IUserService UserService;

        protected IRoleService RoleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            UserService = userService;
            RoleService = roleService;
        }

        [Authorize(Roles = "SYSTEMADMIN")]
        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult> FindAsync(string sortBy, string sortOrder, int? pageNumber, int? pageSize, string searchString = null)
        {
            try
            {
                UserFilter filteringUser = new UserFilter()
                {
                    SearchQuery = searchString                    
                };

                Sorting sorting = new Sorting
                {
                    
                    SortOrder = sortOrder
                };

                sortOrder = sortOrder?.ToLower() ?? "asc";
                ViewBag.CurrentSortOrder = sortOrder;
                ViewBag.NextSortOrder = sortOrder == "asc" ? "desc" : "asc";
                ViewBag.SortByUsername = sortBy == "Username" ? $"Username {ViewBag.NextSortOrder}" : "Username";
                ViewBag.SortByEmail = sortBy == "Email" ? $"Email {ViewBag.NextSortOrder}" : "Email";



                IPagedList<CoreUserDTO> usersDTO = await UserService.FindAsync(sortBy, sortOrder, pageNumber, pageSize, searchString);

                if (usersDTO == null)
                {
                    return View("Error");
                }
                List<CoreUserView> coreUserView = usersDTO.Select(u => new CoreUserView()
                {
                    Username = u.Username,
                    Email = u.Email,
                    Id = u.Id
                }).ToList();

                var pagedList = new StaticPagedList<CoreUserView>(coreUserView, pageNumber ?? 1, pageSize ?? 10, usersDTO.TotalItemCount);
                return await Task.FromResult(View(pagedList));
            }
            catch (Exception)
            {
                return View("Error");
            }



        }

        //List<CoreUserDTO> users = await UserService.FindAsync();
        //List<CoreUserVIEW> mappedUsers = new List<CoreUserVIEW>();

        //if (users == null || users.Count == 0)
        //{
        //    ViewBag.ErrorMessage = "Users not found.";
        //    return View("Error");
        //}

        //foreach (CoreUserDTO user in users)
        //{
        //    CoreUserVIEW coreUserVIEW = new CoreUserVIEW();

        //    coreUserVIEW.Username = user.Username;
        //    coreUserVIEW.Email = user.Email;
        //    coreUserVIEW.Id = user.Id;

        //    mappedUsers.Add(coreUserVIEW);
        //}

        //return View(mappedUsers);





        /* --------------------------------------- */
        // GetById Method - Get user by id
        /* --------------------------------------- */
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                CoreUserDTO user = await UserService.GetByIdAsync(id);



                if (user == null)
                {
                    ViewBag.ErrorMessage = "User not found.";
                    return View("Error");
                }


                CoreUserView coreUserVIEW = new CoreUserView();

                coreUserVIEW.Id = user.Id;
                coreUserVIEW.FirstName = user.FirstName;
                coreUserVIEW.LastName = user.LastName;
                coreUserVIEW.Username = user.Username;
                coreUserVIEW.Email = user.Email;
                coreUserVIEW.DOB = user.DOB;
                coreUserVIEW.Address = user.Address;



                return View(coreUserVIEW);
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while deleting the chef. {ex}";
            }

            return View("Error");

        }


        /* --------------------------------------- */
        //PUT Method Id
        /* --------------------------------------- */


        public async Task<ActionResult> PutAsync(Guid Id)
        {
            try
            {
                CoreUserDTO user = await UserService.GetByIdAsync(Id);

                List<RoleDTO> allRoles = await RoleService.FindAsync();
                List<RoleView> roleViews = allRoles.Select(r => new RoleView { Id = r.Id, Name = r.Name }).ToList();
                CoreUserView coreUserVIEW = new CoreUserView();

                coreUserVIEW.Id = user.Id;
                coreUserVIEW.FirstName = user.FirstName;
                coreUserVIEW.LastName = user.LastName;
                coreUserVIEW.Username = user.Username;
                coreUserVIEW.Email = user.Email;
                coreUserVIEW.DOB = user.DOB;
                coreUserVIEW.Address = user.Address;
                coreUserVIEW.AllRoles = roleViews;

                return View(coreUserVIEW);
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while fetching the user's information. {ex}";
            }

            return View("Error");

        }

        /* --------------------------------------- */
        //PUT Method Submit
        /* --------------------------------------- */

        [HttpPost]
        public async Task<ActionResult> PutAsync(Guid id, CoreUserView coreUserVIEW)
        {
                try
                {
                    CoreUserDTO user = new CoreUserDTO();
                    user.Id = coreUserVIEW.Id;
                    user.FirstName = coreUserVIEW.FirstName;
                    user.LastName = coreUserVIEW.LastName;
                    user.Username = coreUserVIEW.Username;
                    user.Email = coreUserVIEW.Email;
                    user.DOB = coreUserVIEW.DOB;
                    user.Address = coreUserVIEW.Address;
                    user.RoleId = coreUserVIEW.SelectedRole;
                    

                    await UserService.PutAsync(id, user);

                    return RedirectToAction("FindAsync");
                }
                catch (Exception)
                {
                    return View("Error");
                }
        }


        public async Task<ActionResult> UpdatePersonalInfoAsync(Guid Id)
        {
            try
            {
                

                CoreUserDTO user = await UserService.GetByIdAsync(Id);

               
                CoreUserView coreUserVIEW = new CoreUserView();

                coreUserVIEW.Id = user.Id;
                coreUserVIEW.FirstName = user.FirstName;
                coreUserVIEW.LastName = user.LastName;
                coreUserVIEW.DOB = user.DOB;
                coreUserVIEW.Address = user.Address;
                
                
                
                return View(coreUserVIEW);
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while fetching the user's information. {ex}";
            }

            return View("Error");

        }


        [HttpPost]
        public async Task<ActionResult> UpdatePersonalInfoAsync(Guid id, CoreUserView coreUserVIEW)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = "";
                    if (User.Identity is FormsIdentity identity)
                    {
                        userId = identity.Ticket.UserData;
                    }
                    
                    UsersDetailDTO user = new UsersDetailDTO();
                    user.Id = coreUserVIEW.Id;
                    user.FirstName = coreUserVIEW.FirstName;
                    user.LastName = coreUserVIEW.LastName;                
                    user.DOB = coreUserVIEW.DOB;
                    user.Address = coreUserVIEW.Address;
                    coreUserVIEW.UpdatedByUserId = user.UpdatedByUserId = new Guid(userId);
                    coreUserVIEW.DateUpdated = user.DateUpdated = DateTime.Now;

                    await UserService.UpdatePersonalInfoAsync(id, user);

                    return RedirectToAction("GetByIdAsync", new { id = coreUserVIEW.Id });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred while updating the user's information. {ex}";
                }
            }

            return View("Error");

        }


        //}

        /* --------------------------------------- */
        // Delete Method - Delete by id (Deactivate)
        /* --------------------------------------- */

        public async Task<ActionResult> DeactivateAsync(Guid id)
        {
            try
            {
                bool user = await UserService.DeactivateAsync(id);

                if (!user)
                {
                    ViewBag.ErrorMessage = "The user could not be deleted. Please check if the specified user exists.";
                    return View("Error");
                }

                return RedirectToAction("FindAsync");
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while deleting the user. {ex}";
            }

            return View("Error");
        }


    }
}
