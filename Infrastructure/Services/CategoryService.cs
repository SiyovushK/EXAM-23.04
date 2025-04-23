using System.Net;
using AutoMapper;
using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.CategoryDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CategoryService(DataContext context, IMapper mapper) : ICategoryService
{
    public async Task<Response<GetCategoryDTO>> CreateCategoryAsync(CreateCategoryDTO createCategory)
    {
        var category = mapper.Map<Category>(createCategory);

        await context.Categories.AddAsync(category);
        var result = await context.SaveChangesAsync();

        var getCategory = mapper.Map<GetCategoryDTO>(category);

        return result == 0 
            ? new Response<GetCategoryDTO>(HttpStatusCode.InternalServerError, "Category couldn't be added")
            : new Response<GetCategoryDTO>(getCategory);
    }

    public async Task<Response<GetCategoryDTO>> UpdateCategoryAsync(int categoryId, UpdateCategoryDTO updateCategory)
    {
        var info = await context.Categories.FindAsync(categoryId);
        if (info == null)
            return new Response<GetCategoryDTO>(HttpStatusCode.NotFound, "Category was not found");

        mapper.Map(updateCategory, info);
        var result = await context.SaveChangesAsync();

        var getCategory = mapper.Map<GetCategoryDTO>(info);

        return result == 0 
            ? new Response<GetCategoryDTO>(HttpStatusCode.InternalServerError, "Category couldn't be updated")
            : new Response<GetCategoryDTO>(getCategory);
    }

    public async Task<Response<string>> DeleteCategoryAsync(int categoryId)
    {
        var info = await context.Categories.FindAsync(categoryId);
        if (info == null)
            return new Response<string>(HttpStatusCode.NotFound, "Category was not found");
        
        context.Categories.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0 
            ? new Response<string>(HttpStatusCode.InternalServerError, "Category couldn't be deleted")
            : new Response<string>("Category deleted successfully");
    }

    public async Task<Response<GetCategoryDTO>> GetByIdAsync(int categoryId)
    {
        var info = await context.Categories.FindAsync(categoryId);
        if (info == null)
            return new Response<GetCategoryDTO>(HttpStatusCode.NotFound, "Category was not found");

        var getCategory = mapper.Map<GetCategoryDTO>(info);

        return new Response<GetCategoryDTO>(getCategory);
    }

    public async Task<Response<List<GetCategoryDTO>>> GetAllAsync()
    {
        var info = await context.Categories.ToListAsync();
        if (info.Count == 0)
            return new Response<List<GetCategoryDTO>>(HttpStatusCode.NotFound, "Categories are not found");

        var getCategories = mapper.Map<List<GetCategoryDTO>>(info);

        return new Response<List<GetCategoryDTO>>(getCategories);
    }


    //Task 1
    public async Task<Response<List<CategoryAndProducts>>> CategoriesWithProducts()
    {
        var query = await context.Categories
            .Select(c => new CategoryAndProducts
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                Products = c.Products
                    .Select(p => new ProductsParameters
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToList()
            })
            .ToListAsync();
        
        return query.Count == 0
            ? new Response<List<CategoryAndProducts>>(HttpStatusCode.NotFound, "No categories found")
            : new Response<List<CategoryAndProducts>>(query);
    }
}