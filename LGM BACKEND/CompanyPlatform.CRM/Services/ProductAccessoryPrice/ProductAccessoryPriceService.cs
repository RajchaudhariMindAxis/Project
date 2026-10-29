using CompanyPlatform.Core.Data;
using CompanyPlatform.Core.DTOs.CRM;
using CompanyPlatform.Core.Models.CRM;
using CompanyPlatform.Core.Services.CRM;
using Microsoft.EntityFrameworkCore;

namespace CompanyPlatform.Core.Services
{
    public class ProductAccessoryPriceService
        : IProductAccessoryPriceService
    {
        private readonly ApplicationDBContext _context;

        public ProductAccessoryPriceService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductAccessoryPriceDto>> GetAllAsync()
        {
            return await _context.Tbl_ProductAccessoryPrices
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => new ProductAccessoryPriceDto
                {
                    Id = x.Id,
                    PriceListId = x.PriceListId,
                    ItemId = x.ItemId,
                    ItemType = x.ItemType,
                    PriceType = x.PriceType,
                    Amount = x.Amount,
                    MinQty = x.MinQty,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<ProductAccessoryPriceDto?> GetByIdAsync(int id)
        {
            return await _context.Tbl_ProductAccessoryPrices
                .AsNoTracking()
                .Where(x => x.Id == id && !x.IsDeleted)
                .Select(x => new ProductAccessoryPriceDto
                {
                    Id = x.Id,
                    PriceListId = x.PriceListId,
                    ItemId = x.ItemId,
                    ItemType = x.ItemType,
                    PriceType = x.PriceType,
                    Amount = x.Amount,
                    MinQty = x.MinQty,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductAccessoryPriceDto> CreateAsync(
            ProductAccessoryPriceDto dto)
        {
            Validate(dto);
            await ValidateReferencesAsync(dto);

            var entity = new ProductAccessoryPrice
            {
                PriceListId = dto.PriceListId,
                ItemId = dto.ItemId,
                ItemType = dto.ItemType.Trim(),
                PriceType = dto.PriceType.Trim(),
                Amount = dto.Amount,
                MinQty = dto.MinQty,
                EffectiveFrom = dto.EffectiveFrom,
                EffectiveTo = dto.EffectiveTo,
                IsActive = dto.IsActive,
                IsDeleted = false
            };

            _context.Tbl_ProductAccessoryPrices.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return (await GetByIdAsync(entity.Id))!;
        }

        public async Task<bool> UpdateAsync(
            int id,
            ProductAccessoryPriceDto dto)
        {
            Validate(dto);
            await ValidateReferencesAsync(dto);

            var entity = await _context.Tbl_ProductAccessoryPrices
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity == null)
                return false;

            entity.PriceListId = dto.PriceListId;
            entity.ItemId = dto.ItemId;
            entity.ItemType = dto.ItemType.Trim();
            entity.PriceType = dto.PriceType.Trim();
            entity.Amount = dto.Amount;
            entity.MinQty = dto.MinQty;
            entity.EffectiveFrom = dto.EffectiveFrom;
            entity.EffectiveTo = dto.EffectiveTo;
            entity.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Tbl_ProductAccessoryPrices
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            entity.IsActive = false;

            await _context.SaveChangesAsync();
            return true;
        }

        private static void Validate(ProductAccessoryPriceDto dto)
        {
            if (dto.PriceListId <= 0)
                throw new ArgumentException("A valid price list is required.");

            if (dto.ItemId <= 0)
                throw new ArgumentException("A valid item is required.");

            if (dto.ItemType != "Product" &&
                dto.ItemType != "Accessory")
            {
                throw new ArgumentException(
                    "ItemType must be Product or Accessory.");
            }

            if (string.IsNullOrWhiteSpace(dto.PriceType))
                throw new ArgumentException("PriceType is required.");

            if (dto.Amount < 0)
                throw new ArgumentException("Amount cannot be negative.");

            if (dto.MinQty < 1)
                throw new ArgumentException("MinQty must be at least 1.");

            if (dto.EffectiveTo.HasValue &&
                dto.EffectiveTo.Value.Date < dto.EffectiveFrom.Date)
            {
                throw new ArgumentException(
                    "EffectiveTo cannot be earlier than EffectiveFrom.");
            }
        }

        private async Task ValidateReferencesAsync(
            ProductAccessoryPriceDto dto)
        {
            bool priceListExists = await _context.Tbl_PriceLists.AnyAsync(
                x => x.PriceListId == dto.PriceListId &&
                     !x.IsDeleted);

            if (!priceListExists)
                throw new ArgumentException(
                    "The selected price list does not exist or is deleted.");

            // ItemId is polymorphic: it can refer to either a Product
            // or an Accessory. Validate it against the appropriate DbSet
            // after confirming the actual DbSet/entity names in your project.
        }
    }
}