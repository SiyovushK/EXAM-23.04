using System.Net;
using AutoMapper;
using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.ProductDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(DataContext context, IMapper mapper) : IProductService
{
    public async Task<Response<GetProductDTO>> CreateProductAsync(CreateProductDTO createProduct)
    {
        var product = mapper.Map<Product>(createProduct);

        await context.Products.AddAsync(product);
        var result = await context.SaveChangesAsync();

        var getProduct = mapper.Map<GetProductDTO>(product);

        return result == 0 
            ? new Response<GetProductDTO>(HttpStatusCode.InternalServerError, "Product couldn't be added")
            : new Response<GetProductDTO>(getProduct);
    }

    public async Task<Response<GetProductDTO>> UpdateProductAsync(int productId, UpdateProductDTO updateProduct)
    {
        var info = await context.Products.FindAsync(productId);
        if (info == null)
            return new Response<GetProductDTO>(HttpStatusCode.NotFound, "Product was not found");

        mapper.Map(updateProduct, info);
        var result = await context.SaveChangesAsync();

        var getProduct = mapper.Map<GetProductDTO>(info);

        return result == 0 
            ? new Response<GetProductDTO>(HttpStatusCode.InternalServerError, "Product couldn't be updated")
            : new Response<GetProductDTO>(getProduct);
    }

    public async Task<Response<string>> DeleteProductAsync(int productId)
    {
        var info = await context.Products.FindAsync(productId);
        if (info == null)
            return new Response<string>(HttpStatusCode.NotFound, "Product was not found");
        
        context.Products.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0 
            ? new Response<string>(HttpStatusCode.InternalServerError, "Product couldn't be deleted")
            : new Response<string>("Product deleted successfully");
    }

    public async Task<Response<GetProductDTO>> GetByIdAsync(int productId)
    {
        var info = await context.Products.FindAsync(productId);
        if (info == null)
            return new Response<GetProductDTO>(HttpStatusCode.NotFound, "Product was not found");

        var getProduct = mapper.Map<GetProductDTO>(info);

        return new Response<GetProductDTO>(getProduct);
    }

    public async Task<Response<List<GetProductDTO>>> GetAllAsync()
    {
        var info = await context.Products.ToListAsync();
        if (info.Count == 0)
            return new Response<List<GetProductDTO>>(HttpStatusCode.NotFound, "Products are not found");

        var getProducts = mapper.Map<List<GetProductDTO>>(info);

        return new Response<List<GetProductDTO>>(getProducts);
    }

    public async Task<Response<List<GetProductDTO>>> GetProductsFiltered(ProductFilter filter)
    {
        var pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
        var pageSize = filter.PageSize < 10 ? 10 : filter.PageSize;

        var productsQuery = context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            productsQuery = productsQuery
                .Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if (filter.PriceFrom != null)
        {
            productsQuery = productsQuery
                .Where(p => p.Price >= filter.PriceFrom);
        }

        if (filter.PriceTo != null)
        {
            productsQuery = productsQuery
                .Where(p => p.Price <= filter.PriceTo);
        }

        var totalRecords = await productsQuery.CountAsync();

        var rest = await productsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var getProducts = mapper.Map<List<GetProductDTO>>(rest);

        return new PagedResposne<List<GetProductDTO>>(getProducts, pageSize, pageNumber, totalRecords);
    }


    //Task 2
    public async Task<Response<List<ProductsDTO>>> ProductsLessThanFiveInStock()
    {
        var query = await context.Products
            .Where(p => p.QuantityInStock < 5)
            .Select(p => new ProductsDTO
            {
                Id = p.Id,
                Name = p.Name,
                QuantityInStock = p.QuantityInStock
            })
            .ToListAsync();

        return query.Count == 0
            ? new Response<List<ProductsDTO>>(HttpStatusCode.NotFound, "No products with less than five in stock")
            : new Response<List<ProductsDTO>>(query);
    } 

    //Task 3
    public async Task<Response<ProductsStatsDTO>> ProductsStatistics()
    {   
        var totalRecords = await context.Products.CountAsync();
        if(totalRecords == 0)
            return new Response<ProductsStatsDTO>(HttpStatusCode.NotFound, "No products found");

        var price = await context.Products.AverageAsync(p => p.Price);

        var sold = await context.Sales.SumAsync(s => s.QuantitySold);

        var getProductStats = new ProductsStatsDTO()
        {
            totalProducts = totalRecords,
            averagePrice = price,
            totalSold = sold
        };

        return new Response<ProductsStatsDTO>(getProductStats);
    }
}