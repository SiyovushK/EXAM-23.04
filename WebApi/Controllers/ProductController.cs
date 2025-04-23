using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.ProductDTOs;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetProductDTO>> CreateProductAsync(CreateProductDTO createProduct)
    {
        return await productService.CreateProductAsync(createProduct);
    }
    
    [HttpPut]
    public async Task<Response<GetProductDTO>> UpdateProductAsync(int productId, UpdateProductDTO updateProduct)
    {
        return await productService.UpdateProductAsync(productId, updateProduct);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteProductAsync(int productId)
    {
        return await productService.DeleteProductAsync(productId);
    }
    
    [HttpGet("id")]
    public async Task<Response<GetProductDTO>> GetByIdAsync(int productId)
    {
        return await productService.GetByIdAsync(productId);
    }
    
    [HttpGet]
    public async Task<Response<List<GetProductDTO>>> GetAllAsync()
    {
        return await productService.GetAllAsync();
    }
    
    [HttpGet("filtered")]
    public async Task<Response<List<GetProductDTO>>> GetAllAsync([FromQuery] ProductFilter filter)
    {
        return await productService.GetProductsFiltered(filter);
    }

    [HttpGet("ProductsLessThenFiveInStock")]
    public async Task<Response<List<ProductsDTO>>> ProductsLessThanFiveInStock()
    {
        return await productService.ProductsLessThanFiveInStock();
    }

    [HttpGet("GeneralProductsStats")]
    public async Task<Response<ProductsStatsDTO>> ProductsStatistics()
    {
        return await productService.ProductsStatistics();
    }
}