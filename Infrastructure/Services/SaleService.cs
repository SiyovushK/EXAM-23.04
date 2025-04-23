using System.Net;
using AutoMapper;
using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.ProductDTOs;
using Domain.DTOs.SaleDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SaleService(DataContext context, IMapper mapper) : ISaleService
{
    public async Task<Response<GetSaleDTO>> CreateSaleAsync(CreateSaleDTO createSale)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == createSale.ProductId);
        if (product == null)
            return new Response<GetSaleDTO>(HttpStatusCode.NotFound, "Product not found");

        if (product.QuantityInStock < createSale.QuantitySold)
            return new Response<GetSaleDTO>(HttpStatusCode.BadRequest, "Not enough stock available");

        product.QuantityInStock -= createSale.QuantitySold;

        var sale = mapper.Map<Sale>(createSale);

        await context.Sales.AddAsync(sale);
        var result = await context.SaveChangesAsync();

        var getSale = mapper.Map<GetSaleDTO>(sale);

        return result == 0 
            ? new Response<GetSaleDTO>(HttpStatusCode.InternalServerError, "Sale couldn't be added")
            : new Response<GetSaleDTO>(getSale);
    }

    public async Task<Response<GetSaleDTO>> UpdateSaleAsync(int saleId, UpdateSaleDTO updateSale)
    {
        var info = await context.Sales.FindAsync(saleId);
        if (info == null)
            return new Response<GetSaleDTO>(HttpStatusCode.NotFound, "Sale was not found");

        mapper.Map(updateSale, info);
        var result = await context.SaveChangesAsync();

        var getSale = mapper.Map<GetSaleDTO>(info);

        return result == 0 
            ? new Response<GetSaleDTO>(HttpStatusCode.InternalServerError, "Sale couldn't be updated")
            : new Response<GetSaleDTO>(getSale);
    }

    public async Task<Response<string>> DeleteSaleAsync(int saleId)
    {
        var info = await context.Sales.FindAsync(saleId);
        if (info == null)
            return new Response<string>(HttpStatusCode.NotFound, "Sale was not found");
        
        context.Sales.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0 
            ? new Response<string>(HttpStatusCode.InternalServerError, "Sale couldn't be deleted")
            : new Response<string>("Sale deleted successfully");
    }

    public async Task<Response<GetSaleDTO>> GetByIdAsync(int saleId)
    {
        var info = await context.Sales.FindAsync(saleId);
        if (info == null)
            return new Response<GetSaleDTO>(HttpStatusCode.NotFound, "Sale was not found");

        var getSale = mapper.Map<GetSaleDTO>(info);

        return new Response<GetSaleDTO>(getSale);
    }

    public async Task<Response<List<GetSaleDTO>>> GetAllAsync()
    {
        var info = await context.Sales.ToListAsync();
        if (info.Count == 0)
            return new Response<List<GetSaleDTO>>(HttpStatusCode.NotFound, "Sales are not found");

        var getSales = mapper.Map<List<GetSaleDTO>>(info);

        return new Response<List<GetSaleDTO>>(getSales);
    }

    //Task 4
    public async Task<Response<List<GetSaleDTO>>> SalesByDate(SaleFilter filter)
    {
        var pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
        var pageSize = filter.PageSize < 10 ? 10 : filter.PageSize;

        var saleQuery = context.Sales.AsQueryable();

        if (filter.DateFrom != null)
        {
            saleQuery = saleQuery
                .Where(s => s.SaleDate >= filter.DateFrom);
        }

        if (filter.DateTo != null)
        {
            saleQuery = saleQuery
                .Where(s => s.SaleDate <= filter.DateTo);
        }

        var totalRecords = await saleQuery.CountAsync();

        var rest = await saleQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var getProducts = mapper.Map<List<GetSaleDTO>>(rest);
        
        return new PagedResposne<List<GetSaleDTO>>(getProducts, pageSize, pageNumber, totalRecords);
    }

    //Task 5
    public async Task<Response<List<ProductAndSales>>> MostSoldProducts()
    {
        var query = await context.Sales
            .GroupBy(s => s.Product)
            .Select(g => new ProductAndSales
            {
                ProductName = g.Key.Name,
                TotalSold = g.Sum(s => s.QuantitySold)
            })
            .OrderByDescending(g => g.TotalSold)
            .Take(5)
            .ToListAsync();
        
        return query.Count == 0
            ? new Response<List<ProductAndSales>>(HttpStatusCode.NotFound, "No products found")
            : new Response<List<ProductAndSales>>(query);
    }

    //Task 6
    public async Task<Response<List<DateAndRevenue>>> DailyRevenue()
    {
        var date = DateTime.UtcNow.AddDays(-6);

        var query = await context.Sales
            .Where(s => s.SaleDate.Date >= date)
            .GroupBy(s => s.SaleDate.Date)
            .Select(g => new DateAndRevenue
            {
                Date = g.Key,
                Revenue = g.Sum(s => s.QuantitySold * s.Product.Price)
            })
            .OrderByDescending(g => g.Date)
            .ToListAsync();

        return query.Count == 0
            ? new Response<List<DateAndRevenue>>(HttpStatusCode.NotFound, "Revenue couldn't be calculated")
            : new Response<List<DateAndRevenue>>(query);
    }
}