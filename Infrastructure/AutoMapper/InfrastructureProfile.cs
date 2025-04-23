using AutoMapper;
using Domain.DTOs.CategoryDTOs;
using Domain.DTOs.ProductDTOs;
using Domain.DTOs.SaleDTOs;
using Domain.DTOs.StockAdjectmentDTOs;
using Domain.DTOs.SupplierDTOs;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Sale, GetSaleDTO>();
        CreateMap<CreateSaleDTO, Sale>();
        CreateMap<UpdateSaleDTO, Sale>();
        
        CreateMap<Supplier, GetSupplierDTO>();
        CreateMap<CreateSupplierDTO, Supplier>();
        CreateMap<UpdateSupplierDTO, Supplier>();
    
        CreateMap<Product, GetProductDTO>();
        CreateMap<CreateProductDTO, Product>();
        CreateMap<UpdateProductDTO, Product>();
    
        CreateMap<Category, GetCategoryDTO>();
        CreateMap<CreateCategoryDTO, Category>();
        CreateMap<UpdateCategoryDTO, Category>();
    
        CreateMap<StockAdjustment, GetStockAdjustmentDTO>();
        CreateMap<CreateStockAdjustmentDTO, StockAdjustment>();
        CreateMap<UpdateStockAdjustmentDTO, StockAdjustment>();
    }
}