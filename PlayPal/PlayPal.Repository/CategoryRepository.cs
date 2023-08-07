using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayPal.DAL;
using PlayPal.Repository.Common;
using PlayPal.ModelDTO;
using System.Data.Entity.Validation;
using System.Runtime.Remoting.Contexts;

namespace PlayPal.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        protected EFContext Context { get; set; }
        public CategoryRepository(EFContext context)
        {
            Context = context;
        }

        //getAll
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            List<CategoryDTO> list;

            list = await Context.Category.Select(category => new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                DateCreated = category.DateCreated,
                DateUpdated = category.DateUpdated,
                CreatedByUserId = category.CreatedByUserId,
                UpdatedByUserId = category.UpdatedByUserId,
                IsActive = category.IsActive
            }).ToListAsync<CategoryDTO>();

            return list;
        }

        //getById
        public async Task<CategoryDTO> GetOneCategoryAsync(Guid id)
        {
            CategoryDTO category;

            category = await Context.Category.Where(x => x.Id == id).Select(x => new CategoryDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                CreatedByUserId = x.CreatedByUserId,
                UpdatedByUserId = x.UpdatedByUserId,
                IsActive = x.IsActive
            }).FirstOrDefaultAsync<CategoryDTO>();

            return category;
        }

        //edit
        public async Task<CategoryDTO> EditCategoryAsync(Guid id, CategoryDTO categoryDTO)
        {
            IQueryable<Category> query = Context.Category.AsQueryable();
            Category category = await query.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
            
                category.Id = categoryDTO.Id;
                category.Name = categoryDTO.Name;
                category.Description = categoryDTO.Description;
                category.DateCreated = categoryDTO.DateCreated;
                category.DateUpdated = categoryDTO.DateUpdated = DateTime.Now;
                category.CreatedByUserId = categoryDTO.CreatedByUserId;
                category.UpdatedByUserId = categoryDTO.UpdatedByUserId;
                category.IsActive = categoryDTO.IsActive;

                await Context.SaveChangesAsync();
                return categoryDTO;
            
        }

        //delete (hide)
        public async Task<CategoryDTO> HideDeleteCategoryAsync(Guid id, CategoryDTO categoryDTO)
        {
            IQueryable<Category> query = Context.Category.AsQueryable();
            Category category = await query.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();

            category.Id = categoryDTO.Id;
            category.Name = categoryDTO.Name;
            category.Description = categoryDTO.Description;
            category.DateCreated = categoryDTO.DateCreated;
            category.DateUpdated = categoryDTO.DateUpdated = DateTime.Now;
            category.CreatedByUserId = categoryDTO.CreatedByUserId;
            category.UpdatedByUserId = categoryDTO.UpdatedByUserId;
            category.IsActive = categoryDTO.IsActive;

            await Context.SaveChangesAsync();
            return categoryDTO;
        }

        //add new category

        public async Task<CategoryDTO> AddNewCategoryAsync(CategoryDTO categoryDTO)
        {
            Context.Category.Add(new Category()
            {
                Id = categoryDTO.Id = Guid.NewGuid(),
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                DateCreated = categoryDTO.DateCreated = DateTime.Now,
                DateUpdated = categoryDTO.DateUpdated = DateTime.Now,
                CreatedByUserId = categoryDTO.CreatedByUserId,
                UpdatedByUserId = categoryDTO.UpdatedByUserId,
                IsActive = categoryDTO.IsActive = true
            });

            await Context.SaveChangesAsync();
            return categoryDTO;
        }

    }
}