using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.StockAdjectmentDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockAdjustmentController(IStockAdjustmentService stockAdjustment) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetStockAdjustmentDTO>> CreateStockAdjustmentAsync(CreateStockAdjustmentDTO createStockAdjustment)
    {
        return await stockAdjustment.CreateStockAdjustmentAsync(createStockAdjustment);
    }
    
    [HttpPut]
    public async Task<Response<GetStockAdjustmentDTO>> UpdateStockAdjustmentAsync(int stockAdjustmentId, UpdateStockAdjustmentDTO updateStockAdjustment)
    {
        return await stockAdjustment.UpdateStockAdjustmentAsync(stockAdjustmentId, updateStockAdjustment);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteStockAdjustmentAsync(int stockAdjustmentId)
    {
        return await stockAdjustment.DeleteStockAdjustmentAsync(stockAdjustmentId);
    }
    
    [HttpGet("id")]
    public async Task<Response<GetStockAdjustmentDTO>> GetByIdAsync(int stockAdjustmentId)
    {
        return await stockAdjustment.GetByIdAsync(stockAdjustmentId);
    }
    
    [HttpGet]
    public async Task<Response<List<GetStockAdjustmentDTO>>> GetAllAsync()
    {
        return await stockAdjustment.GetAllAsync();
    }
    
    [HttpGet("AdjustmentsOfProductIdOne")]
    public async Task<Response<List<AdjestmentDTO>>> AdjuctmentsOfProductOne()
    {
        return await stockAdjustment.AdjuctmentsOfProductOne();
    }
    
    [HttpGet("ProductsWithSalesAndAdjustments")]
    public async Task<Response<List<AdjustmentDTO>>> ProductsFullInfo()
    {
        return await stockAdjustment.ProductsFullInfo();
    }
}