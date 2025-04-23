using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.StockAdjectmentDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IStockAdjustmentService
{
    Task<Response<GetStockAdjustmentDTO>> CreateStockAdjustmentAsync(CreateStockAdjustmentDTO createStockAdjustment);
    Task<Response<GetStockAdjustmentDTO>> UpdateStockAdjustmentAsync(int stockAdjustmentId, UpdateStockAdjustmentDTO updateStockAdjustment);
    Task<Response<string>> DeleteStockAdjustmentAsync(int stockAdjustmentId);
    Task<Response<GetStockAdjustmentDTO>> GetByIdAsync(int stockAdjustmentId);
    Task<Response<List<GetStockAdjustmentDTO>>> GetAllAsync();
    Task<Response<List<AdjestmentDTO>>> AdjuctmentsOfProductOne();
    Task<Response<List<AdjustmentDTO>>> ProductsFullInfo();
}