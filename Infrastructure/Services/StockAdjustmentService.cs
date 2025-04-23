using System.Net;
using AutoMapper;
using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.StockAdjectmentDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StockAdjustmentService(DataContext context, IMapper mapper) : IStockAdjustmentService
{
    public async Task<Response<GetStockAdjustmentDTO>> CreateStockAdjustmentAsync(CreateStockAdjustmentDTO createStockAdjustment)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == createStockAdjustment.ProductId);
        if (product == null)
            return new Response<GetStockAdjustmentDTO>(HttpStatusCode.NotFound, "Product not found");

        product.QuantityInStock += createStockAdjustment.AdjustmentAmount;

        var stockAdjustment = mapper.Map<StockAdjustment>(createStockAdjustment);

        await context.StockAdjustments.AddAsync(stockAdjustment);
        var result = await context.SaveChangesAsync();

        var getStockAdjustment = mapper.Map<GetStockAdjustmentDTO>(stockAdjustment);

        return result == 0 
            ? new Response<GetStockAdjustmentDTO>(HttpStatusCode.InternalServerError, "Stock Adjustment couldn't be added")
            : new Response<GetStockAdjustmentDTO>(getStockAdjustment);
    }

    public async Task<Response<GetStockAdjustmentDTO>> UpdateStockAdjustmentAsync(int stockAdjustmentId, UpdateStockAdjustmentDTO updateStockAdjustment)
    {
        var info = await context.StockAdjustments.FindAsync(stockAdjustmentId);
        if (info == null)
            return new Response<GetStockAdjustmentDTO>(HttpStatusCode.NotFound, "Stock Adjustment was not found");

        mapper.Map(updateStockAdjustment, info);
        var result = await context.SaveChangesAsync();

        var getStockAdjustment = mapper.Map<GetStockAdjustmentDTO>(info);

        return result == 0 
            ? new Response<GetStockAdjustmentDTO>(HttpStatusCode.InternalServerError, "Stock Adjustment couldn't be updated")
            : new Response<GetStockAdjustmentDTO>(getStockAdjustment);
    }

    public async Task<Response<string>> DeleteStockAdjustmentAsync(int stockAdjustmentId)
    {
        var info = await context.StockAdjustments.FindAsync(stockAdjustmentId);
        if (info == null)
            return new Response<string>(HttpStatusCode.NotFound, "Stock Adjustment was not found");
        
        context.StockAdjustments.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0 
            ? new Response<string>(HttpStatusCode.InternalServerError, "Stock Adjustment couldn't be deleted")
            : new Response<string>("Stock Adjustment deleted successfully");
    }

    public async Task<Response<GetStockAdjustmentDTO>> GetByIdAsync(int stockAdjustmentId)
    {
        var info = await context.StockAdjustments.FindAsync(stockAdjustmentId);
        if (info == null)
            return new Response<GetStockAdjustmentDTO>(HttpStatusCode.NotFound, "Stock Adjustment was not found");

        var getStockAdjustment = mapper.Map<GetStockAdjustmentDTO>(info);

        return new Response<GetStockAdjustmentDTO>(getStockAdjustment);
    }

    public async Task<Response<List<GetStockAdjustmentDTO>>> GetAllAsync()
    {
        var info = await context.StockAdjustments.ToListAsync();
        if (info.Count == 0)
            return new Response<List<GetStockAdjustmentDTO>>(HttpStatusCode.NotFound, "Stock Adjustments are not found");

        var getStockAdjustments = mapper.Map<List<GetStockAdjustmentDTO>>(info);

        return new Response<List<GetStockAdjustmentDTO>>(getStockAdjustments);
    }

    //Task 7
    public async Task<Response<List<AdjestmentDTO>>> AdjuctmentsOfProductOne()
    {
        var query = await context.StockAdjustments
            .Where(sa => sa.ProductId == 1)
            .Select(sa => new AdjestmentDTO
            {
                AdjustmentDate = sa.AdjustmentDate.Date,
                Amount = sa.AdjustmentAmount,
                Reason = sa.Reason
            })
            .OrderByDescending(sa => sa.AdjustmentDate)
            .ToListAsync();
        
        return query.Count == 0
            ? new Response<List<AdjestmentDTO>>(HttpStatusCode.NotFound, "History of products with Id 1 is not found")
            : new Response<List<AdjestmentDTO>>(query);
    }

    //Task 8
    public async Task<Response<List<AdjustmentDTO>>> ProductsFullInfo()
    {
        var query = await context.Products
            .Select(p => new AdjustmentDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                QuantityInStock = p.QuantityInStock,
                Category = p.Category.Name,
                Supplier = p.Supplier.Name,
                Sales = p.Sales
                    .Select(s => new QuantityAndDateOfSale
                    {
                        Quantity = s.QuantitySold,
                        Date = s.SaleDate.Date
                    })
                    .ToList(),
                Adjustments = p.StockAdjustments
                    .Select(sa => new AdjestmentDTO
                    {
                        Amount = sa.AdjustmentAmount,
                        AdjustmentDate = sa.AdjustmentDate.Date,
                        Reason = sa.Reason
                    })
                    .ToList()
            })
            .ToListAsync();

        return query.Count == 0
            ? new Response<List<AdjustmentDTO>>(HttpStatusCode.NotFound, "No products found")
            : new Response<List<AdjustmentDTO>>(query);
    }
}