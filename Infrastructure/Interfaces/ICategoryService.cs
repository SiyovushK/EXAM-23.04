using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.CategoryDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<Response<GetCategoryDTO>> CreateCategoryAsync(CreateCategoryDTO createCategory);
    Task<Response<GetCategoryDTO>> UpdateCategoryAsync(int categoryId, UpdateCategoryDTO updateCategory);
    Task<Response<string>> DeleteCategoryAsync(int categoryId);
    Task<Response<GetCategoryDTO>> GetByIdAsync(int categoryId);
    Task<Response<List<GetCategoryDTO>>> GetAllAsync();
    Task<Response<List<CategoryAndProducts>>> CategoriesWithProducts();
}