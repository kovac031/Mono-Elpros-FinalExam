using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayPal.ModelDTO;

namespace PlayPal.Service.Common
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetOneCategoryAsync(Guid id);
        Task<CategoryDTO> EditCategoryAsync(Guid id, CategoryDTO category);
        Task<CategoryDTO> HideDeleteCategoryAsync(Guid id, CategoryDTO categoryDTO);
        Task<CategoryDTO> AddNewCategoryAsync(CategoryDTO categoryDTO);
    }
}
