using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.SupplierDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController(ISupplierService supplierService) : ControllerBase
{   
    [HttpPost]
    public async Task<Response<GetSupplierDTO>> CreateSupplierAsync(CreateSupplierDTO createSupplier)
    {
        return await supplierService.CreateSupplierAsync(createSupplier);
    }
    
    [HttpPut]
    public async Task<Response<GetSupplierDTO>> UpdateSupplierAsync(int supplierId, UpdateSupplierDTO updateSupplier)
    {
        return await supplierService.UpdateSupplierAsync(supplierId, updateSupplier);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteSupplierAsync(int supplierId)
    {
        return await supplierService.DeleteSupplierAsync(supplierId);
    }
    
    [HttpGet("id")]
    public async Task<Response<GetSupplierDTO>> GetByIdAsync(int supplierId)
    {
        return await supplierService.GetByIdAsync(supplierId);
    }
    
    [HttpGet]
    public async Task<Response<List<GetSupplierDTO>>> GetAllAsync()
    {
        return await supplierService.GetAllAsync();
    }
    
    [HttpGet("SuppliersWithTheirProducts")]
    public async Task<Response<List<SupplierDTO>>> SuppliersWithProducts()
    {
        return await supplierService.SuppliersWithProducts();
    }
    
    [HttpGet("GeneralStats")]
    public async Task<Response<TotalStats>> OverallStats()
    {
        return await supplierService.OverallStats();
    }
}