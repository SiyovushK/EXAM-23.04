using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.CategoryDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetCategoryDTO>> CreateCategoryAsync(CreateCategoryDTO createCategory)
    {
        return await categoryService.CreateCategoryAsync(createCategory);
    }

    [HttpPut]
    public async Task<Response<GetCategoryDTO>> UpdateCategoryAsync(int categoryId, UpdateCategoryDTO updateCategory)
    {
        return await categoryService.UpdateCategoryAsync(categoryId, updateCategory);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteCategoryAsync(int categoryId)
    {
        return await categoryService.DeleteCategoryAsync(categoryId);
    }

    [HttpGet("id")]
    public async Task<Response<GetCategoryDTO>> GetByIdAsync(int categoryId)
    {
        return await categoryService.GetByIdAsync(categoryId);
    }

    [HttpGet]
    public async Task<Response<List<GetCategoryDTO>>> GetAllAsync()
    {
        return await categoryService.GetAllAsync();
    }

    [HttpGet("CategoriesWithProducts")]
    public async Task<Response<List<CategoryAndProducts>>> CategoriesWithProducts()
    {
        return await categoryService.CategoriesWithProducts();
    }
}