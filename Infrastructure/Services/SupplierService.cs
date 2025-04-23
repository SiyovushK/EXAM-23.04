using System.Net;
using AutoMapper;
using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.SupplierDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SupplierService(DataContext context, IMapper mapper) : ISupplierService
{
    public async Task<Response<GetSupplierDTO>> CreateSupplierAsync(CreateSupplierDTO createSupplier)
    {
        var supplier = mapper.Map<Supplier>(createSupplier);

        await context.Suppliers.AddAsync(supplier);
        var result = await context.SaveChangesAsync();

        var getSupplier = mapper.Map<GetSupplierDTO>(supplier);

        return result == 0 
            ? new Response<GetSupplierDTO>(HttpStatusCode.InternalServerError, "Supplier couldn't be added")
            : new Response<GetSupplierDTO>(getSupplier);
    }

    public async Task<Response<GetSupplierDTO>> UpdateSupplierAsync(int supplierId, UpdateSupplierDTO updateSupplier)
    {
        var info = await context.Suppliers.FindAsync(supplierId);
        if (info == null)
            return new Response<GetSupplierDTO>(HttpStatusCode.NotFound, "Supplier was not found");

        mapper.Map(updateSupplier, info);
        var result = await context.SaveChangesAsync();

        var getSupplier = mapper.Map<GetSupplierDTO>(info);

        return result == 0 
            ? new Response<GetSupplierDTO>(HttpStatusCode.InternalServerError, "Supplier couldn't be updated")
            : new Response<GetSupplierDTO>(getSupplier);
    }

    public async Task<Response<string>> DeleteSupplierAsync(int supplierId)
    {
        var info = await context.Suppliers.FindAsync(supplierId);
        if (info == null)
            return new Response<string>(HttpStatusCode.NotFound, "Supplier was not found");
        
        context.Suppliers.Remove(info);
        var result = await context.SaveChangesAsync();

        return result == 0 
            ? new Response<string>(HttpStatusCode.InternalServerError, "Supplier couldn't be deleted")
            : new Response<string>("Supplier deleted successfully");
    }

    public async Task<Response<GetSupplierDTO>> GetByIdAsync(int supplierId)
    {
        var info = await context.Suppliers.FindAsync(supplierId);
        if (info == null)
            return new Response<GetSupplierDTO>(HttpStatusCode.NotFound, "Supplier was not found");

        var getSupplier = mapper.Map<GetSupplierDTO>(info);

        return new Response<GetSupplierDTO>(getSupplier);
    }

    public async Task<Response<List<GetSupplierDTO>>> GetAllAsync()
    {
        var info = await context.Suppliers.ToListAsync();
        if (info.Count == 0)
            return new Response<List<GetSupplierDTO>>(HttpStatusCode.NotFound, "Suppliers are not found");

        var getSuppliers = mapper.Map<List<GetSupplierDTO>>(info);

        return new Response<List<GetSupplierDTO>>(getSuppliers);
    }

    //Task 9
    public async Task<Response<List<SupplierDTO>>> SuppliersWithProducts()
    {
        var query = await context.Suppliers
            .Select(s => new SupplierDTO
            {
                SupplierId = s.Id,
                SupplierName = s.Name,
                Products = s.Products.Select(p => p.Name).ToList()
            })
            .ToListAsync();

        return query.Count == 0
            ? new Response<List<SupplierDTO>>(HttpStatusCode.NotFound, "Suppliers not found")
            : new Response<List<SupplierDTO>>(query);
    }

    //Task 10
    public async Task<Response<TotalStats>> OverallStats()
    {
        var TotalProducts = await context.Products.CountAsync();

        var TotalRevenue = await context.Sales.SumAsync(s => s.QuantitySold * s.Product.Price);

        var TotalSales = await context.Sales.CountAsync();

        var getStats = new TotalStats()
        {
            totalProducts = TotalProducts,
            totalRevenue = TotalRevenue,
            totalSales = TotalSales
        };

        return getStats == null
            ? new Response<TotalStats>(HttpStatusCode.NotFound, "Couldn't get statistics")
            : new Response<TotalStats>(getStats);
    }
}