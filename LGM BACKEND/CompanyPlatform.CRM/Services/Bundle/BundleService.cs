using CompanyPlatform.Core.Data;
using CompanyPlatform.Core.Models.CRM;
using CompanyPlatform.CRM.DTO.Bundle;
using Microsoft.EntityFrameworkCore;

namespace CompanyPlatform.CRM.Services.Bundle
{
    public class BundleService : IBundleService
    {
        private readonly ApplicationDBContext _context;

        private static readonly string[] AllowedTaxModes =
        {
            "Inclusive",
            "Exclusive"
        };

        public BundleService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BundleDto>> GetAllAsync()
        {
            return await _context.Tbl_Bundles
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => new BundleDto
                {
                    BundleId = x.BundleId,
                    Code = x.Code,
                    Name = x.Name,
                    ParentProductId = x.ParentProductId,
                    PackagePrice = x.PackagePrice,
                    TaxMode = x.TaxMode,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    Description = x.Description,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<BundleDto?> GetByIdAsync(int id)
        {
            var x = await _context.Tbl_Bundles
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    b => b.BundleId == id && !b.IsDeleted);

            if (x == null)
                return null;

            return MapToDto(x);
        }

        public async Task<BundleDto> CreateAsync(BundleDto dto)
        {
            Validate(dto);

            var code = dto.Code.Trim();

            bool codeExists = await _context.Tbl_Bundles
                .AnyAsync(x =>
                    x.Code == code &&
                    !x.IsDeleted);

            if (codeExists)
                throw new ArgumentException(
                    "Bundle code already exists.");

            await ValidateParentProductAsync(dto.ParentProductId);

            var item = new CompanyPlatform.Core.Models.CRM.Bundle
            {
                Code = code,
                Name = dto.Name.Trim(),
                ParentProductId = dto.ParentProductId,
                PackagePrice = dto.PackagePrice,
                TaxMode = NormalizeTaxMode(dto.TaxMode),
                EffectiveFrom = dto.EffectiveFrom,
                EffectiveTo = dto.EffectiveTo,
                Description = dto.Description?.Trim(),
                IsActive = dto.IsActive,
                IsDeleted = false
            };

            _context.Tbl_Bundles.Add(item);

            await _context.SaveChangesAsync();

            return MapToDto(item);
        }

        public async Task<bool> UpdateAsync(int id, BundleDto dto)
        {
            Validate(dto);

            var item = await _context.Tbl_Bundles
                .FirstOrDefaultAsync(
                    x => x.BundleId == id && !x.IsDeleted);

            if (item == null)
                return false;

            var code = dto.Code.Trim();

            bool codeExists = await _context.Tbl_Bundles
                .AnyAsync(x =>
                    x.Code == code &&
                    x.BundleId != id &&
                    !x.IsDeleted);

            if (codeExists)
                throw new ArgumentException(
                    "Bundle code already exists.");

            await ValidateParentProductAsync(dto.ParentProductId);

            item.Code = code;
            item.Name = dto.Name.Trim();
            item.ParentProductId = dto.ParentProductId;
            item.PackagePrice = dto.PackagePrice;
            item.TaxMode = NormalizeTaxMode(dto.TaxMode);
            item.EffectiveFrom = dto.EffectiveFrom;
            item.EffectiveTo = dto.EffectiveTo;
            item.Description = dto.Description?.Trim();
            item.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Tbl_Bundles
                .FirstOrDefaultAsync(
                    x => x.BundleId == id && !x.IsDeleted);

            if (item == null)
                return false;

            item.IsDeleted = true;
            item.IsActive = false;

            // Preserve BundleItem records for historical reference.
            // If your business requires cascading soft deletion,
            // update the child records explicitly here.

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task ValidateParentProductAsync(int productId)
        {
            // Change Tbl_Products if your DbSet uses a different name.
            bool productExists = await _context.Tbl_Products
                .AnyAsync(x => x.ProductId == productId);

            if (!productExists)
                throw new ArgumentException(
                    "The selected parent product does not exist.");
        }

        private static void Validate(BundleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Code))
                throw new ArgumentException("Bundle code is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Bundle name is required.");

            if (dto.ParentProductId <= 0)
                throw new ArgumentException(
                    "A valid parent product is required.");

            if (dto.PackagePrice < 0)
                throw new ArgumentException(
                    "Package price cannot be negative.");

            if (dto.EffectiveTo.HasValue &&
                dto.EffectiveTo.Value.Date < dto.EffectiveFrom.Date)
            {
                throw new ArgumentException(
                    "Effective To cannot be earlier than Effective From.");
            }

            if (string.IsNullOrWhiteSpace(dto.TaxMode) ||
                !AllowedTaxModes.Contains(
                    dto.TaxMode.Trim(),
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "TaxMode must be Inclusive or Exclusive.");
            }
        }

        private static string NormalizeTaxMode(string taxMode)
        {
            return AllowedTaxModes.First(
                x => string.Equals(
                    x,
                    taxMode.Trim(),
                    StringComparison.OrdinalIgnoreCase));
        }

        private static BundleDto MapToDto(
            CompanyPlatform.Core.Models.CRM.Bundle item)
        {
            return new BundleDto
            {
                BundleId = item.BundleId,
                Code = item.Code,
                Name = item.Name,
                ParentProductId = item.ParentProductId,
                PackagePrice = item.PackagePrice,
                TaxMode = item.TaxMode,
                EffectiveFrom = item.EffectiveFrom,
                EffectiveTo = item.EffectiveTo,
                Description = item.Description,
                IsActive = item.IsActive
            };
        }

        public async Task<string> GenerateCodeAsync()
        {
            const string prefix = "LGM-BNDL-";

            // Include soft-deleted records to avoid reusing old codes.
            var existingCodes = await _context.Tbl_Bundles
                .AsNoTracking()
                .Where(x => x.Code.StartsWith(prefix))
                .Select(x => x.Code)
                .ToListAsync();

            int maxNumber = 0;

            foreach (var code in existingCodes)
            {
                if (string.IsNullOrWhiteSpace(code) ||
                    !code.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string numberPart = code.Substring(prefix.Length);

                if (int.TryParse(numberPart, out int number) &&
                    number > maxNumber)
                {
                    maxNumber = number;
                }
            }

            int nextNumber = maxNumber + 1;

            return $"{prefix}{nextNumber:D4}";
        }

    }
}