using CompanyPlatform.Core.DTOs.CRM;

namespace CompanyPlatform.Core.Services
{
    public interface IProductAccessoryPriceService
    {
        Task<IEnumerable<ProductAccessoryPriceDto>> GetAllAsync();

        Task<ProductAccessoryPriceDto?> GetByIdAsync(int id);

        Task<ProductAccessoryPriceDto> CreateAsync(
            ProductAccessoryPriceDto dto);

        Task<bool> UpdateAsync(
            int id,
            ProductAccessoryPriceDto dto);

        Task<bool> DeleteAsync(int id);
    }
}