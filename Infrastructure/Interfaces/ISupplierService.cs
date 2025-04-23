using Domain.DTOs.AnalyticsDTOs;
using Domain.DTOs.SupplierDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface ISupplierService
{
    Task<Response<GetSupplierDTO>> CreateSupplierAsync(CreateSupplierDTO createSupplier);
    Task<Response<GetSupplierDTO>> UpdateSupplierAsync(int supplierId, UpdateSupplierDTO updateSupplier);
    Task<Response<string>> DeleteSupplierAsync(int supplierId);
    Task<Response<GetSupplierDTO>> GetByIdAsync(int supplierId);
    Task<Response<List<GetSupplierDTO>>> GetAllAsync();
    Task<Response<List<SupplierDTO>>> SuppliersWithProducts();
    Task<Response<TotalStats>> OverallStats();
}