using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayPal.ModelDTO;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;

namespace PlayPal.Service
{
    public class CategoryService : ICategoryService
    {
        protected ICategoryRepository Repository { get; set; }
        public CategoryService(ICategoryRepository repository) 
        {
            Repository = repository;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            List<CategoryDTO> list = await Repository.GetAllCategoriesAsync();
            return list;
        }

        public async Task<CategoryDTO> GetOneCategoryAsync(Guid id)
        {
            CategoryDTO category = await Repository.GetOneCategoryAsync(id);
            return category;
        }

        public async Task<CategoryDTO> EditCategoryAsync(Guid id, CategoryDTO categoryDTO)
        {
            CategoryDTO categoryCheck = await Repository.GetOneCategoryAsync(id);
            if (categoryCheck == null)
            {
                return null;
            }
            CategoryDTO categoryToEdit = new CategoryDTO
            {
                Id = id,
                Name = categoryDTO.Name == default ? categoryCheck.Name : categoryDTO.Name,
                Description = categoryDTO.Description == default ? categoryCheck.Description : categoryDTO.Description,
                DateCreated = categoryDTO.DateCreated == default ? categoryCheck.DateCreated : categoryDTO.DateCreated,
                DateUpdated = categoryDTO.DateUpdated == default ? categoryCheck.DateUpdated : categoryDTO.DateUpdated,
                CreatedByUserId = categoryDTO.CreatedByUserId == default ? categoryCheck.CreatedByUserId : categoryDTO.CreatedByUserId,
                UpdatedByUserId = categoryDTO.UpdatedByUserId == default ? categoryCheck.UpdatedByUserId : categoryDTO.UpdatedByUserId,
                IsActive = categoryDTO.IsActive == default ? categoryCheck.IsActive : categoryDTO.IsActive
            };

            CategoryDTO category = await Repository.EditCategoryAsync(id, categoryToEdit);
            return category;
        }

        public async Task<CategoryDTO> HideDeleteCategoryAsync(Guid id, CategoryDTO categoryDTO)
        {
            CategoryDTO categoryCheck = await Repository.GetOneCategoryAsync(id);
            if (categoryCheck == null)
            {
                return null;
            }
            CategoryDTO categoryToDelete = new CategoryDTO
            {
                Id = id,
                Name = categoryDTO.Name == default ? categoryCheck.Name : categoryDTO.Name,
                Description = categoryDTO.Description == default ? categoryCheck.Description : categoryDTO.Description,
                DateCreated = categoryDTO.DateCreated == default ? categoryCheck.DateCreated : categoryDTO.DateCreated,
                DateUpdated = categoryDTO.DateUpdated == default ? categoryCheck.DateUpdated : categoryDTO.DateUpdated,
                CreatedByUserId = categoryDTO.CreatedByUserId == default ? categoryCheck.CreatedByUserId : categoryDTO.CreatedByUserId,
                UpdatedByUserId = categoryDTO.UpdatedByUserId == default ? categoryCheck.UpdatedByUserId : categoryDTO.UpdatedByUserId,
                //IsActive = categoryDTO.IsActive == default ? categoryCheck.IsActive : categoryDTO.IsActive
            };

            CategoryDTO category = await Repository.HideDeleteCategoryAsync(id, categoryToDelete);
            return category;
        }

        public async Task<CategoryDTO> AddNewCategoryAsync(CategoryDTO categoryDTO)
        {
            CategoryDTO category = await Repository.AddNewCategoryAsync(categoryDTO);
            return category;
        }
    }
}