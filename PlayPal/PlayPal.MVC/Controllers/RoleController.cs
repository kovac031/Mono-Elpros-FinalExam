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

namespace PlayPal.MVC.Controllers
{
    public class RoleController : Controller
    {

        protected IRoleService RoleService;

        public RoleController(IRoleService roleService)
        {
            RoleService = roleService;

        }

        public async Task<ActionResult> FindAsync()
        {
            List<RoleDTO> roles = await RoleService.FindAsync();

            List<RoleView> mappedRoles = new List<RoleView>();

            if (roles == null)
            {
                ViewBag.ErrorMessage = "Roles not found.";
                return View("Error");
            }

            foreach (RoleDTO role in roles)
            {
                RoleView roleView = new RoleView();

                roleView.Id = role.Id;
                roleView.Name = role.Name;
               
                mappedRoles.Add(roleView);

            }

            return View(mappedRoles);
        }
    }
}