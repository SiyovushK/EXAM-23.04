using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.SaleDTOs;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface ISaleService
{
    Task<Response<GetSaleDTO>> CreateSaleAsync(CreateSaleDTO createSale);
    Task<Response<GetSaleDTO>> UpdateSaleAsync(int saleId, UpdateSaleDTO updateSale);
    Task<Response<string>> DeleteSaleAsync(int saleId);
    Task<Response<GetSaleDTO>> GetByIdAsync(int saleId);
    Task<Response<List<GetSaleDTO>>> GetAllAsync();
    Task<Response<List<GetSaleDTO>>> SalesByDate(SaleFilter filter);
    Task<Response<List<ProductAndSales>>> MostSoldProducts();
    Task<Response<List<DateAndRevenue>>> DailyRevenue();
}