using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.ProductDTOs;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<Response<GetProductDTO>> CreateProductAsync(CreateProductDTO createProduct);
    Task<Response<GetProductDTO>> UpdateProductAsync(int productId, UpdateProductDTO updateProduct);
    Task<Response<string>> DeleteProductAsync(int productId);
    Task<Response<GetProductDTO>> GetByIdAsync(int productId);
    Task<Response<List<GetProductDTO>>> GetAllAsync();
    Task<Response<List<GetProductDTO>>> GetProductsFiltered(ProductFilter filter);
    Task<Response<List<ProductsDTO>>> ProductsLessThanFiveInStock();
    Task<Response<ProductsStatsDTO>> ProductsStatistics();
}