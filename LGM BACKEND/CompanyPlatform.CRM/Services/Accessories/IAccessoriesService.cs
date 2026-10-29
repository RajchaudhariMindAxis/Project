using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyPlatform.Core.Models.CRM;
using CompanyPlatform.Core.Models.CRM.Dashboards;
using CompanyPlatform.CRM.DTO.Accessories;
using Microsoft.AspNetCore.Http;

namespace CompanyPlatform.CRM.Services.Accessories
{
    public interface IAccessoriesService
    {
        Task<List<AccessoriesDto>> GetAllAsync();

        Task<AccessoriesDto> GetByIdAsync(int id);

        Task<int> CreateAsync(AccessoriesDto dto);

        Task<bool> UpdateAsync(AccessoriesDto dto);

        Task<bool> DeleteAsync(int id);

        // Generate accessory code without any parameter.
        Task<string> GenerateAccessoryCode();

        Task<List<AccessoryLog>> GetLogsAsync(int id);

        Task ImportAccessoriesAsync(IFormFile file);
    }
}