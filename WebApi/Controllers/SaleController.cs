using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.SaleDTOs;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController(ISaleService saleService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetSaleDTO>> CreateSaleAsync(CreateSaleDTO createSale)
    {
        return await saleService.CreateSaleAsync(createSale);
    }
    
    [HttpPut]
    public async Task<Response<GetSaleDTO>> UpdateSaleAsync(int saleId, UpdateSaleDTO updateSale)
    {
        return await saleService.UpdateSaleAsync(saleId, updateSale);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteSaleAsync(int saleId)
    {
        return await saleService.DeleteSaleAsync(saleId);
    }
    
    [HttpGet("id")]
    public async Task<Response<GetSaleDTO>> GetByIdAsync(int saleId)
    {
        return await saleService.GetByIdAsync(saleId);
    }
    
    [HttpGet]
    public async Task<Response<List<GetSaleDTO>>> GetAllAsync()
    {
        return await saleService.GetAllAsync();
    }

    [HttpGet("SalesFiltered")]
    public async Task<Response<List<GetSaleDTO>>> SalesByDate([FromQuery] SaleFilter filter)
    {
        return await saleService.SalesByDate(filter);
    }
    
    [HttpGet("MostSoldProducts")]
    public async Task<Response<List<ProductAndSales>>> MostSoldProducts()
    {
        return await saleService.MostSoldProducts();
    }
    
    [HttpGet("DailyRevenue")]
    public async Task<Response<List<DateAndRevenue>>> DailyRevenue()
    {
        return await saleService.DailyRevenue();
    }
}