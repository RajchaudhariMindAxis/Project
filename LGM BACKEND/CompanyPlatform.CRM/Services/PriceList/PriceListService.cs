
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyPlatform.Core.Data;
using CompanyPlatform.Core.Models.CRM;
using CompanyPlatform.Core.Models.CRM.Dashboards;
using CompanyPlatform.CRM.DTO.PriceList;
using Microsoft.EntityFrameworkCore;

namespace CompanyPlatform.CRM.Services.PriceList
{
    public class PriceListService : IPriceListService
    {
        private readonly ApplicationDBContext _context;

        public PriceListService(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/PriceList
        public async Task<IEnumerable<PriceListDto>> GetAllAsync()
        {
            return await _context.Tbl_PriceLists
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new PriceListDto
                {
                    PriceListId = x.PriceListId,
                    Code = x.Code,
                    Name = x.Name,
                    CustomerChannelType = x.CustomerChannelType,
                    CurrencyId = x.CurrencyId,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted
                })
                .ToListAsync();
        }

        // GET: api/PriceList/{id}
        public async Task<PriceListDto?> GetByIdAsync(int id)
        {
            var item = await _context.Tbl_PriceLists
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.PriceListId == id && !x.IsDeleted);

            if (item == null)
                return null;

            return new PriceListDto
            {
                PriceListId = item.PriceListId,
                Code = item.Code,
                Name = item.Name,
                CustomerChannelType = item.CustomerChannelType,
                CurrencyId = item.CurrencyId,
                EffectiveFrom = item.EffectiveFrom,
                EffectiveTo = item.EffectiveTo,
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted
            };
        }

        // POST: api/PriceList
        public async Task<PriceListDto> CreateAsync(PriceListDto dto)
        {
            ValidatePriceList(dto);

            string code = dto.Code.Trim();

            // Validate currency
            bool currencyExists = await _context.Set<Currency>()
                .AnyAsync(c => c.Id == dto.CurrencyId);

            if (!currencyExists)
            {
                throw new ArgumentException(
                    "Invalid CurrencyId. Currency does not exist.");
            }

            // Prevent duplicate codes, including soft-deleted records
            bool codeExists = await _context.Tbl_PriceLists
                .AnyAsync(x => x.Code == code);

            if (codeExists)
            {
                throw new ArgumentException(
                    "Price List code already exists.");
            }

            var item = new CompanyPlatform.Core.Models.CRM.PriceList
            {
                Code = code,
                Name = dto.Name.Trim(),
                CustomerChannelType = dto.CustomerChannelType.Trim(),
                CurrencyId = dto.CurrencyId,
                EffectiveFrom = dto.EffectiveFrom,
                EffectiveTo = dto.EffectiveTo,
                IsActive = dto.IsActive,
                IsDeleted = false
            };

            _context.Tbl_PriceLists.Add(item);
            await _context.SaveChangesAsync();

            return new PriceListDto
            {
                PriceListId = item.PriceListId,
                Code = item.Code,
                Name = item.Name,
                CustomerChannelType = item.CustomerChannelType,
                CurrencyId = item.CurrencyId,
                EffectiveFrom = item.EffectiveFrom,
                EffectiveTo = item.EffectiveTo,
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted
            };
        }

        // PUT: api/PriceList/{id}
        public async Task<bool> UpdateAsync(int id, PriceListDto dto)
        {
            var item = await _context.Tbl_PriceLists
                .FirstOrDefaultAsync(x =>
                    x.PriceListId == id && !x.IsDeleted);

            if (item == null)
                return false;

            ValidatePriceList(dto);

            string code = dto.Code.Trim();

            // Validate currency
            bool currencyExists = await _context.Set<Currency>()
                .AnyAsync(c => c.Id == dto.CurrencyId);

            if (!currencyExists)
            {
                throw new ArgumentException(
                    "Invalid CurrencyId. Currency does not exist.");
            }

            // Exclude the current record from duplicate check
            bool codeExists = await _context.Tbl_PriceLists
                .AnyAsync(x =>
                    x.Code == code &&
                    x.PriceListId != id);

            if (codeExists)
            {
                throw new ArgumentException(
                    "Price List code already exists.");
            }

            item.Code = code;
            item.Name = dto.Name.Trim();
            item.CustomerChannelType = dto.CustomerChannelType.Trim();
            item.CurrencyId = dto.CurrencyId;
            item.EffectiveFrom = dto.EffectiveFrom;
            item.EffectiveTo = dto.EffectiveTo;
            item.IsActive = dto.IsActive;

            // Do not allow clients to undelete records through this method.
            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE: api/PriceList/{id}
        // Soft delete instead of physically removing the database record.
        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Tbl_PriceLists
                .FirstOrDefaultAsync(x =>
                    x.PriceListId == id && !x.IsDeleted);

            if (item == null)
                return false;

            item.IsDeleted = true;
            item.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }
        // GET: api/PriceList/generate-code
        public async Task<string> GenerateCodeAsync()
        {
            const string prefix = "LGM-PRL-";

            var existingCodes = await _context.Tbl_PriceLists
                .AsNoTracking()
                .Where(x => x.Code.StartsWith(prefix))
                .Select(x => x.Code)
                .ToListAsync();

            int maxNumber = 0;

            foreach (var code in existingCodes)
            {
                if (string.IsNullOrWhiteSpace(code))
                    continue;

                string numberPart = code.Substring(prefix.Length);

                if (int.TryParse(numberPart, out int number))
                {
                    if (number > maxNumber)
                        maxNumber = number;
                }
            }

            int nextNumber = maxNumber + 1;

            return $"{prefix}{nextNumber:D4}";
        }
        private static void ValidatePriceList(PriceListDto dto)
        {
            if (dto == null)
                throw new ArgumentException("Price List data is required.");

            if (string.IsNullOrWhiteSpace(dto.Code))
                throw new ArgumentException("Price List code is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Price List name is required.");

            if (string.IsNullOrWhiteSpace(dto.CustomerChannelType))
                throw new ArgumentException(
                    "Customer/Channel Type is required.");

            if (dto.CurrencyId <= 0)
                throw new ArgumentException(
                    "A valid CurrencyId is required.");

            if (dto.EffectiveTo.HasValue &&
                dto.EffectiveTo.Value.Date < dto.EffectiveFrom.Date)
            {
                throw new ArgumentException(
                    "Effective To cannot be earlier than Effective From.");
            }
        }
    }
}
