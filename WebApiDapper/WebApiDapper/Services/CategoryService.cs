using WebApiDapper.DTOs.CategoryDTO;
using WebApiDapper.DTOs.ExtendAttribute;
using WebApiDapper.Entities;
using WebApiDapper.IRepositories;

namespace WebApiDapper.Services
{
    public class CategoryService
    {
        private ICategoryRepository<int> _categoryRepository { get; set; }
        public CategoryService(ICategoryRepository<int> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var result = await _categoryRepository.GetAllAsync();
            return result;
        }

        public async Task<CategoryResponseDTO?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null) return null;
            var parentCategory = await GetCategoryByIdAsync(category.ParentId);
            var result = new CategoryResponseDTO
            {
                Id = category.Id,
                Name = category.Name,
                CreateDate = DateTime.Now,
                IsActive = category.IsActive,
                ParentId = parentCategory,
                SeoAlias = category.SeoAlias,
                SeoDescription = category.SeoDescription,
                SeoKeyword = category.SeoKeyword,
                SeoTitle = category.SeoTitle,
                UpdateDate = category.UpdateDate,

            };
            return result;
        }

        public async Task<int?> CreateCategory(CategoryRequestDTO categoryRequest)
        {
            var category = new Category()
            {
                Name = categoryRequest.Name,
                SeoAlias = categoryRequest.SeoAlias,
                SeoDescription = categoryRequest.SeoDescription,
                SeoKeyword = categoryRequest.SeoKeyword,
                SeoTitle = categoryRequest.SeoTitle,
                IsActive = categoryRequest.IsActive,
                ParentId = categoryRequest.ParentId,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };

            return await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteCategory(int Id)
        {
            var category = await _categoryRepository.GetByIdAsync(Id);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category.Id);
            }
        }

        public async Task UpdateCategory(Category categoryRequest)
        {
            await _categoryRepository.UpdateAsync(categoryRequest);
        }
    }
}
