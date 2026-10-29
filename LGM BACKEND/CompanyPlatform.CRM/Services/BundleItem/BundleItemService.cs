using CompanyPlatform.Core.Data;
using CompanyPlatform.Core.Models.CRM;
using CompanyPlatform.CRM.DTO.BundleItem;
using Microsoft.EntityFrameworkCore;

namespace CompanyPlatform.CRM.Services.BundleItem
{
    public class BundleItemService : IBundleItemService
    {
        private readonly ApplicationDBContext _context;

        public BundleItemService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BundleItemDto>> GetAllAsync()
        {
            return await _context.Tbl_BundleItems
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => new BundleItemDto
                {
                    BundleItemId = x.BundleItemId,
                    BundleId = x.BundleId,
                    ProductId = x.ProductId,
                    VariantId = x.VariantId,
                    AccessoriesId = x.AccessoriesId,
                    Quantity = x.Quantity,
                    PricingType = x.PricingType,
                    AllocatedPrice = x.AllocatedPrice,
                    IsOptional = x.IsOptional
                })
                .ToListAsync();
        }

        public async Task<BundleItemDto?> GetByIdAsync(int id)
        {
            var item = await _context.Tbl_BundleItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.BundleItemId == id && !x.IsDeleted);

            return item == null ? null : MapToDto(item);
        }

        public async Task<BundleItemDto> CreateAsync(BundleItemDto dto)
        {
            Validate(dto);
            await ValidateForeignKeysAsync(dto);

            var item = new CompanyPlatform.Core.Models.CRM.BundleItem
            {
                BundleId = dto.BundleId,
                ProductId = dto.ProductId,
                VariantId = dto.VariantId,
                AccessoriesId = dto.AccessoriesId,
                Quantity = dto.Quantity,
                PricingType = dto.PricingType.Trim(),
                AllocatedPrice = dto.AllocatedPrice,
                IsOptional = dto.IsOptional,
                IsDeleted = false
            };

            _context.Tbl_BundleItems.Add(item);
            await _context.SaveChangesAsync();

            return MapToDto(item);
        }

        public async Task<bool> UpdateAsync(int id, BundleItemDto dto)
        {
            Validate(dto);

            var item = await _context.Tbl_BundleItems
                .FirstOrDefaultAsync(x =>
                    x.BundleItemId == id && !x.IsDeleted);

            if (item == null)
                return false;

            await ValidateForeignKeysAsync(dto);

            item.BundleId = dto.BundleId;
            item.ProductId = dto.ProductId;
            item.VariantId = dto.VariantId;
            item.AccessoriesId = dto.AccessoriesId;
            item.Quantity = dto.Quantity;
            item.PricingType = dto.PricingType.Trim();
            item.AllocatedPrice = dto.AllocatedPrice;
            item.IsOptional = dto.IsOptional;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Tbl_BundleItems
                .FirstOrDefaultAsync(x =>
                    x.BundleItemId == id && !x.IsDeleted);

            if (item == null)
                return false;

            item.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task ValidateForeignKeysAsync(BundleItemDto dto)
        {
            var bundleExists = await _context.Tbl_Bundles
                .AnyAsync(x =>
                    x.BundleId == dto.BundleId &&
                    !x.IsDeleted);

            if (!bundleExists)
                throw new ArgumentException(
                    "Bundle does not exist or has been deleted.");

            if (dto.ProductId.HasValue)
            {
                var productExists = await _context.Tbl_Products
                    .AnyAsync(p =>
                        p.ProductId == dto.ProductId.Value &&
                        !p.IsDeleted);

                if (!productExists)
                    throw new ArgumentException(
                        "Product does not exist or has been deleted.");
            }

            if (dto.VariantId.HasValue)
            {
                var variant = await _context.Set<
                        CompanyPlatform.Core.Models.Commonmaster.ProductVariant>()
                    .AsNoTracking()
                    .Where(v => v.Id == dto.VariantId.Value)
                    .Select(v => new
                    {
                        v.ProductId,
                        v.IsDeleted
                    })
                    .FirstOrDefaultAsync();

                if (variant == null || variant.IsDeleted)
                    throw new ArgumentException(
                        "Product variant does not exist or has been deleted.");

                if (dto.ProductId.HasValue &&
                    variant.ProductId != dto.ProductId.Value)
                {
                    throw new ArgumentException(
                        "The selected variant does not belong to the selected product.");
                }
            }

            if (dto.AccessoriesId.HasValue)
            {
                var accessoryExists = await _context
                    .Set<CompanyPlatform.Core.Models.CRM.Accessories>()
                    .AnyAsync(a =>
                        a.AccessoriesId == dto.AccessoriesId.Value &&
                        !a.IsDeleted);

                if (!accessoryExists)
                    throw new ArgumentException(
                        "Accessory does not exist or has been deleted.");
            }

            int selectedCount =
                (dto.ProductId.HasValue ? 1 : 0) +
                (dto.VariantId.HasValue ? 1 : 0) +
                (dto.AccessoriesId.HasValue ? 1 : 0);

            if (selectedCount != 1)
            {
                throw new ArgumentException(
                    "Select exactly one item: Product, Variant, or Accessory.");
            }
        }

        private static void Validate(BundleItemDto dto)
        {
            if (dto.BundleId <= 0)
                throw new ArgumentException(
                    "A valid BundleId is required.");

            if (dto.Quantity < 1)
                throw new ArgumentException(
                    "Quantity must be at least 1.");

            if (dto.AllocatedPrice < 0)
                throw new ArgumentException(
                    "Allocated price cannot be negative.");

            if (string.IsNullOrWhiteSpace(dto.PricingType))
                throw new ArgumentException(
                    "PricingType is required.");

            if (dto.PricingType.Trim().Length > 30)
                throw new ArgumentException(
                    "PricingType cannot exceed 30 characters.");
        }

        private static BundleItemDto MapToDto(
            CompanyPlatform.Core.Models.CRM.BundleItem item)
        {
            return new BundleItemDto
            {
                BundleItemId = item.BundleItemId,
                BundleId = item.BundleId,
                ProductId = item.ProductId,
                VariantId = item.VariantId,
                AccessoriesId = item.AccessoriesId,
                Quantity = item.Quantity,
                PricingType = item.PricingType,
                AllocatedPrice = item.AllocatedPrice,
                IsOptional = item.IsOptional
            };
        }
    }
}