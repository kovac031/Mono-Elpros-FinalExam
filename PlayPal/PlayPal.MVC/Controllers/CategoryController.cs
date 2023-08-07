using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Xml.Linq;
using PlayPal.DAL;
using PlayPal.ModelDTO;
using PlayPal.MVC.Models;
using PlayPal.Service.Common;
using System.Web.Security;

namespace PlayPal.MVC.Controllers
{
    public class CategoryController : Controller
    {
        protected ICategoryService Service { get; set; }
        public CategoryController(ICategoryService service)
        {
            Service = service;
        }

        //getAllAsync
        public async Task<ActionResult> GetAllCategoriesAsync()
        {
            List<CategoryDTO> listDTO = await Service.GetAllCategoriesAsync();
            List<CategoryView> listView = new List<CategoryView>();
            foreach (CategoryDTO categoryDTO in listDTO)
            {
                CategoryView categoryView = new CategoryView();
                categoryView.Id = categoryDTO.Id;
                categoryView.Name = categoryDTO.Name;
                categoryView.Description = categoryDTO.Description;
                categoryView.DateCreated = categoryDTO.DateCreated;
                categoryView.DateUpdated = categoryDTO.DateUpdated;
                categoryView.CreatedByUserId = categoryDTO.CreatedByUserId;
                categoryView.UpdatedByUserId = categoryDTO.UpdatedByUserId;
                categoryView.IsActive = categoryDTO.IsActive;

                listView.Add(categoryView);
            }
            return View(listView);
        }

        //getOneAsync
        public async Task<ActionResult> GetOneCategoryAsync(Guid id)
        {
            CategoryDTO categoryDTO = await Service.GetOneCategoryAsync(id);
            CategoryView categoryView = new CategoryView();
            categoryView.Id = categoryDTO.Id;
            categoryView.Name = categoryDTO.Name;
            categoryView.Description = categoryDTO.Description;
            categoryView.DateCreated = categoryDTO.DateCreated;
            categoryView.DateUpdated = categoryDTO.DateUpdated;
            categoryView.CreatedByUserId = categoryDTO.CreatedByUserId;
            categoryView.UpdatedByUserId = categoryDTO.UpdatedByUserId;
            categoryView.IsActive = categoryDTO.IsActive;

            return View(categoryView);
        }

        //edit
        [HttpGet]
        public async Task<ActionResult> EditCategoryAsync(Guid id)
        {
            CategoryDTO categoryDTO = await Service.GetOneCategoryAsync(id);
            CategoryView categoryView = new CategoryView();
            categoryView.Id = categoryDTO.Id;
            categoryView.Name = categoryDTO.Name;
            categoryView.Description = categoryDTO.Description;
            categoryView.DateCreated = categoryDTO.DateCreated;
            categoryView.DateUpdated = categoryDTO.DateUpdated;
            categoryView.CreatedByUserId = categoryDTO.CreatedByUserId;
            categoryView.UpdatedByUserId = categoryDTO.UpdatedByUserId;
            categoryView.IsActive = categoryDTO.IsActive;

            return View(categoryView);
        }
        [HttpPost]
        public async Task<ActionResult> EditCategoryAsync(CategoryView categoryView)
        {
            CategoryDTO categoryDTO = new CategoryDTO();

            categoryDTO.Id = categoryView.Id;
            categoryDTO.Name = categoryView.Name;
            categoryDTO.Description = categoryView.Description;
            categoryDTO.DateCreated = categoryView.DateCreated;
            categoryDTO.DateUpdated = categoryView.DateUpdated;
            categoryDTO.CreatedByUserId = categoryView.CreatedByUserId;
            categoryDTO.UpdatedByUserId = categoryView.UpdatedByUserId;
            categoryDTO.IsActive = categoryView.IsActive;

            await Service.EditCategoryAsync(categoryDTO.Id, categoryDTO);

            return RedirectToAction("GetAllCategoriesAsync");
        }

        //delete (hide)
        [HttpGet]
        public async Task<ActionResult> HideDeleteCategoryAsync(Guid id)
        {
            CategoryDTO categoryDTO = await Service.GetOneCategoryAsync(id);
            CategoryView categoryView = new CategoryView();
            categoryView.Id = categoryDTO.Id;
            categoryView.Name = categoryDTO.Name;
            categoryView.Description = categoryDTO.Description;
            categoryView.DateCreated = categoryDTO.DateCreated;
            categoryView.DateUpdated = categoryDTO.DateUpdated;
            categoryView.CreatedByUserId = categoryDTO.CreatedByUserId;
            categoryView.UpdatedByUserId = categoryDTO.UpdatedByUserId;
            categoryView.IsActive = categoryDTO.IsActive;

            return View(categoryView);
        }

        [HttpPost]
        public async Task<ActionResult> HideDeleteCategoryAsync(CategoryView categoryView)
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.Id = categoryView.Id;
            categoryDTO.Name = categoryView.Name;
            categoryDTO.Description = categoryView.Description;
            categoryDTO.DateCreated = categoryView.DateCreated;
            categoryDTO.DateUpdated = categoryView.DateUpdated;
            categoryDTO.CreatedByUserId = categoryView.CreatedByUserId;
            categoryDTO.UpdatedByUserId = categoryView.UpdatedByUserId;
            categoryDTO.IsActive = categoryView.IsActive;

            await Service.HideDeleteCategoryAsync(categoryDTO.Id, categoryDTO);
            
            return RedirectToAction("GetAllCategoriesAsync");
        }

        //add new category
        [HttpGet]
        public async Task<ActionResult> AddNewCategoryAsync()
        {
            return await Task.FromResult(View("AddNewCategoryAsync"));
        }

        [HttpPost]
        public async Task<ActionResult> AddNewCategoryAsync(CategoryView categoryView)
        {
            var userId = "";
            if (User.Identity is FormsIdentity identity)
            {
                userId = identity.Ticket.UserData;
            }

            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.Id = categoryView.Id;
            categoryDTO.Name = categoryView.Name;
            categoryDTO.Description = categoryView.Description;
            categoryDTO.DateCreated = categoryView.DateCreated = DateTime.Now;
            categoryDTO.DateUpdated = categoryView.DateUpdated = DateTime.Now;
            categoryDTO.CreatedByUserId = categoryView.CreatedByUserId = new Guid(userId);
            categoryDTO.UpdatedByUserId = categoryView.UpdatedByUserId = new Guid(userId);
            categoryDTO.IsActive = categoryView.IsActive;

            await Service.AddNewCategoryAsync(categoryDTO);
            
            return RedirectToAction("GetAllCategoriesAsync");
        }
    }
}
