using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CompanyPlatform.Core.Data;
using CompanyPlatform.Core.Models;
using CompanyPlatform.Core.Models.CRM;
using CompanyPlatform.Core.Models.CRM.Dashboards;
using CompanyPlatform.CRM.DTO.Accessories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace CompanyPlatform.CRM.Services.Accessories
{
    public class AccessoriesService : IAccessoriesService
    {
        private readonly ApplicationDBContext _context;

        public AccessoriesService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateAccessoryCode()
        {
            const string prefix = "LGM-ACS-";

            var existingCodes = await _context.Tbl_Accessories
                .AsNoTracking()
                .Where(x => x.AccessoriesCode.StartsWith(prefix))
                .Select(x => x.AccessoriesCode)
                .ToListAsync();

            int nextSequence = existingCodes
                .Select(code =>
                {
                    string suffix = code.Length > prefix.Length
                        ? code[prefix.Length..]
                        : string.Empty;

                    return int.TryParse(
                        suffix,
                        out int number)
                        ? number
                        : 0;
                })
                .DefaultIfEmpty(0)
                .Max() + 1;

            return $"{prefix}{nextSequence:D4}";
        }

        private async Task<string?> GetVendorNameAsync(int? vendorId)
        {
            if (!vendorId.HasValue)
                return null;

            var vendor = await _context.Set<Vendor>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == vendorId.Value);

            return vendor?.VendorName;
        }

        private static async Task<string?> SaveImageAsync(
            IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            if (file.Length > 5 * 1024 * 1024)
            {
                throw new ArgumentException(
                    "Image size must not exceed 5 MB.");
            }

            string extension = Path
                .GetExtension(file.FileName)
                .ToLowerInvariant();

            if (!new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            }.Contains(extension))
            {
                throw new ArgumentException(
                    "Only JPG, JPEG, PNG and WEBP images are allowed.");
            }

            string directory = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "accessories");

            Directory.CreateDirectory(directory);

            string fileName =
                $"{Guid.NewGuid():N}{extension}";

            string path =
                Path.Combine(directory, fileName);

            await using var stream =
                new FileStream(
                    path,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None);

            await file.CopyToAsync(stream);

            return $"/uploads/accessories/{fileName}";
        }

        private static AccessoriesDto MapToDto(
            CompanyPlatform.Core.Models.CRM.Accessories item)
        {
            return new AccessoriesDto
            {
                AccessoriesId = item.AccessoriesId,

                AccessoriesCode = item.AccessoriesCode,

                AccessorySKU = item.AccessorySKU,

                PartNo = item.PartNo,

                UOM = item.UOM,

                AccessoriesName = item.AccessoriesName,

                AccessoriesImage = item.AccessoriesImage,

                // Vendor
                VendorId = item.VendorId,
                VendorName = item.VendorName,

                // Master relationships
                ProductGroupId = item.ProductGroupId,

                CategoryId = item.CategoryId,

                BrandId = item.BrandId,

                UnitId = item.UnitId,

                TaxId = item.TaxId,

                VersionId = item.VersionId,

                CurrencyId = item.CurrencyId,

                // Description
                AccessoriesDescription =
                    item.AccessoriesDescription,

                Specification =
                    item.Specification,

                WarrantyPeriod =
                    item.WarrantyPeriod,

                // Manufacturing
                ManufacturingDate =
                    item.ManufacturingDate,

                // Calibration
                IsCalibration =
                    item.IsCalibration,

                CalibrationFrequency =
                    item.CalibrationFrequency,

                // Pricing
                PurchasePrice =
                    item.PurchasePrice,

                SalePrice =
                    item.SalePrice,

                // Stock
                TrackStock =
                    item.TrackStock,

                AvailableQuantity =
                    item.AvailableQuantity,

                MinStock =
                    item.MinStock,

                OutOfStockWarning =
                    item.OutOfStockWarning,

                // Status
                IsActive =
                    item.IsActive,

                IsDeleted =
                    item.IsDeleted,

                // Audit
                CreatedAt =
                    item.CreatedAt,

                UpdatedAt =
                    item.UpdatedAt
            };
        }

        public async Task<List<AccessoriesDto>> GetAllAsync()
        {
            var items = await _context.Tbl_Accessories
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return items
                .Select(item => MapToDto(item))
                .ToList();
        }

        public async Task<AccessoriesDto> GetByIdAsync(int id)
        {
            var item = await _context.Tbl_Accessories
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.AccessoriesId == id &&
                    !x.IsDeleted);

            if (item == null)
            {
                throw new KeyNotFoundException(
                    $"Accessory with ID {id} was not found.");
            }

            return MapToDto(item);
        }

        public async Task<int> CreateAsync(
            AccessoriesDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            if (string.IsNullOrWhiteSpace(
                dto.AccessoriesName))
            {
                throw new ArgumentException(
                    "AccessoriesName is required.");
            }

            // Resolve vendor name from VendorId.
            // VendorId remains the FK.
            string? vendorName =
                dto.VendorId.HasValue
                    ? await GetVendorNameAsync(dto.VendorId)
                    : dto.VendorName;

            // Save image
            string? imagePath =
                await SaveImageAsync(
                    dto.AccessoriesImageFile);

            var item =
                new CompanyPlatform.Core.Models.CRM.Accessories
                {
                    // Generate code without any parameter
                    AccessoriesCode =
                        await GenerateAccessoryCode(),

                    AccessorySKU =
                        dto.AccessorySKU,

                    PartNo =
                        dto.PartNo,

                    UOM =
                        dto.UOM,

                    AccessoriesName =
                        dto.AccessoriesName.Trim(),

                    AccessoriesImage =
                        imagePath ?? dto.AccessoriesImage,

                    VendorId =
                        dto.VendorId,

                    VendorName =
                        vendorName,


                    ProductGroupId =
                        dto.ProductGroupId,

                    CategoryId =
                        dto.CategoryId,

                    BrandId =
                        dto.BrandId,

                    UnitId =
                        dto.UnitId,

                    TaxId =
                        dto.TaxId,

                    VersionId =
                        dto.VersionId,

                    CurrencyId =
                        dto.CurrencyId,


                    AccessoriesDescription =
                        dto.AccessoriesDescription,

                    Specification =
                        dto.Specification,

                    WarrantyPeriod =
                        dto.WarrantyPeriod,


                    ManufacturingDate =
                        dto.ManufacturingDate,

                    IsCalibration =
                        dto.IsCalibration,

                    CalibrationFrequency =
                        dto.IsCalibration
                            ? dto.CalibrationFrequency
                            : null,

                    PurchasePrice =
                        dto.PurchasePrice,

                    SalePrice =
                        dto.SalePrice,

                    TrackStock =
                        dto.TrackStock,

                    AvailableQuantity =
                        dto.AvailableQuantity,

                    MinStock =
                        dto.MinStock,

                    OutOfStockWarning =
                        dto.OutOfStockWarning,


                    IsActive =
                        dto.IsActive,

                    IsDeleted =
                        false,


                    CreatedAt =
                        DateTime.UtcNow,

                    UpdatedAt =
                        DateTime.UtcNow
                };

            _context.Tbl_Accessories.Add(item);

            await _context.SaveChangesAsync();

            _context.Tbl_QualityAssurance.Add(
                new QualityAssurance
                {
                    ItemType =
                        "Accessory",

                    ItemId =
                        item.AccessoriesId,

                    ItemName =
                        item.AccessoriesName,

                    ItemCode =
                        item.AccessoriesCode,

                    Quantity =
                        item.AvailableQuantity,

                    Status =
                        "Pending",

                    Remarks =
                        "Initial Entry from Accessory Master",

                    CreatedDate =
                        DateTime.Now
                });

            await _context.SaveChangesAsync();

            return item.AccessoriesId;
        }


        public async Task<bool> UpdateAsync(
            AccessoriesDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var item =
                await _context.Tbl_Accessories
                    .FirstOrDefaultAsync(x =>
                        x.AccessoriesId ==
                            dto.AccessoriesId &&
                        !x.IsDeleted);

            if (item == null)
                return false;

            if (string.IsNullOrWhiteSpace(
                dto.AccessoriesName))
            {
                throw new ArgumentException(
                    "AccessoriesName is required.");
            }


            string? imagePath =
                await SaveImageAsync(
                    dto.AccessoriesImageFile);

            if (imagePath != null)
            {
                item.AccessoriesImage =
                    imagePath;
            }
            else if (!string.IsNullOrWhiteSpace(
                dto.AccessoriesImage))
            {
                item.AccessoriesImage =
                    dto.AccessoriesImage;
            }

            item.AccessoriesName =
                dto.AccessoriesName.Trim();

            item.AccessorySKU =
                dto.AccessorySKU;

            item.PartNo =
                dto.PartNo;

            item.UOM =
                dto.UOM;


            item.VendorId =
                dto.VendorId;

            item.VendorName =
                dto.VendorId.HasValue
                    ? await GetVendorNameAsync(dto.VendorId)
                    : dto.VendorName;


            item.ProductGroupId =
                dto.ProductGroupId;

            item.CategoryId =
                dto.CategoryId;

            item.BrandId =
                dto.BrandId;

            item.UnitId =
                dto.UnitId;

            item.TaxId =
                dto.TaxId;

            item.VersionId =
                dto.VersionId;

            item.CurrencyId =
                dto.CurrencyId;


            item.AccessoriesDescription =
                dto.AccessoriesDescription;

            item.Specification =
                dto.Specification;

            item.WarrantyPeriod =
                dto.WarrantyPeriod;


            item.ManufacturingDate =
                dto.ManufacturingDate;


            item.IsCalibration =
                dto.IsCalibration;

            item.CalibrationFrequency =
                dto.IsCalibration
                    ? dto.CalibrationFrequency
                    : null;


            item.PurchasePrice =
                dto.PurchasePrice;

            item.SalePrice =
                dto.SalePrice;


            item.TrackStock =
                dto.TrackStock;

            item.AvailableQuantity =
                dto.AvailableQuantity;

            item.MinStock =
                dto.MinStock;

            item.OutOfStockWarning =
                dto.OutOfStockWarning;


            item.IsActive =
                dto.IsActive;

            item.UpdatedAt =
                DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var item =
                await _context.Tbl_Accessories
                    .FirstOrDefaultAsync(x =>
                        x.AccessoriesId == id &&
                        !x.IsDeleted);

            if (item == null)
                return false;

            item.IsDeleted =
                true;

            item.UpdatedAt =
                DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AccessoryLog>> GetLogsAsync(
            int id)
        {
            return await _context.Tbl_AccessoryLogs
                .AsNoTracking()
                .Where(x =>
                    x.AccessoryId == id)
                .OrderByDescending(x =>
                    x.Timestamp)
                .ToListAsync();
        }

        public async Task ImportAccessoriesAsync(
            IFormFile file)
        {
            ArgumentNullException.ThrowIfNull(file);

            if (file.Length == 0)
            {
                throw new ArgumentException(
                    "The uploaded Excel file is empty.");
            }

            ExcelPackage.License
                .SetNonCommercialOrganization(
                    "CompanyPlatform");

            using var stream =
                new MemoryStream();

            await file.CopyToAsync(stream);

            stream.Position = 0;

            using var package =
                new ExcelPackage(stream);

            var sheet =
                package.Workbook
                    .Worksheets
                    .FirstOrDefault();

            if (sheet?.Dimension == null)
            {
                throw new InvalidOperationException(
                    "Excel worksheet is empty.");
            }

            var headers =
                new Dictionary<string, int>(
                    StringComparer.OrdinalIgnoreCase);

            for (
                int col = 1;
                col <= sheet.Dimension.Columns;
                col++)
            {
                string header =
                    NormalizeHeader(
                        sheet.Cells[1, col].Text);

                if (!string.IsNullOrWhiteSpace(header))
                {
                    headers[header] =
                        col;
                }
            }


            for (
                int row = 2;
                row <= sheet.Dimension.Rows;
                row++)
            {
                if (
                    Enumerable
                        .Range(
                            1,
                            sheet.Dimension.Columns)
                        .All(c =>
                            string.IsNullOrWhiteSpace(
                                sheet.Cells[row, c].Text)))
                {
                    continue;
                }


                string code =
                    Cell(
                        sheet,
                        row,
                        headers,
                        "accessoriescode",
                        "accessories_code");

                string name =
                    Cell(
                        sheet,
                        row,
                        headers,
                        "accessoriesname",
                        "accessories_name");

                if (
                    string.IsNullOrWhiteSpace(code) ||
                    string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                var item =
                    await _context.Tbl_Accessories
                        .FirstOrDefaultAsync(x =>
                            x.AccessoriesCode ==
                            code.Trim());

                bool isNew =
                    item == null;

                item ??=
                    new CompanyPlatform.Core.Models.CRM.Accessories();

                item.AccessoriesCode =
                    code.Trim();

                item.AccessoriesName =
                    name.Trim();


                item.AccessorySKU =
                    NullIfEmpty(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "accessorysku",
                            "accessory_sku"));

                item.PartNo =
                    NullIfEmpty(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "partno",
                            "part_no"));

                item.UOM =
                    NullIfEmpty(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "uom",
                            "unit_of_measure"));

                item.AccessoriesImage =
                    NullIfEmpty(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "accessoriesimage",
                            "accessories_image"));


                item.VendorId =
                    NullableInt(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "vendorid",
                            "vendor_id"));

                string importedVendorName =
                    NullIfEmpty(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "vendorname",
                            "vendor_name"))
                    ?? string.Empty;

                item.VendorName =
                    string.IsNullOrWhiteSpace(
                        importedVendorName)
                        ? await GetVendorNameAsync(
                            item.VendorId)
                        : importedVendorName;

                item.ProductGroupId =
                    NullableInt(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "productgroupid",
                            "product_group_id"));

                item.CategoryId =
                    NullableInt(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "categoryid",
                            "category_id"));

                item.BrandId =
                    NullableInt(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "brandid",
                            "brand_id"));

                item.UnitId =
                    NullableInt(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "unitid",
                            "unit_id"));

                item.TaxId =
                    NullableInt(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "taxid",
                            "tax_id"));

                item.VersionId =
                    NullableInt(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "versionid",
                            "version_id"));

                item.CurrencyId =
                    NullableInt(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "currencyid",
                            "currency_id"));


                item.AccessoriesDescription =
                    NullIfEmpty(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "accessoriesdescription",
                            "accessories_description"));

                item.Specification =
                    NullIfEmpty(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "specification"));

                item.WarrantyPeriod =
                    NullIfEmpty(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "warrantyperiod",
                            "warranty_period"));


                item.ManufacturingDate =
                    NullableDateTime(
                        sheet,
                        row,
                        headers,
                        "manufacturingdate",
                        "manufacturing_date");


                item.IsCalibration =
                    Bool(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "iscalibration",
                            "is_calibration"));

                item.CalibrationFrequency =
                    item.IsCalibration
                        ? NullIfEmpty(
                            Cell(
                                sheet,
                                row,
                                headers,
                                "calibrationfrequency",
                                "calibration_frequency"))
                        : null;

                item.PurchasePrice =
                    Decimal(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "purchaseprice",
                            "purchase_price"));

                item.SalePrice =
                    Decimal(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "saleprice",
                            "sale_price"));

                // =====================================================
                // STOCK
                // =====================================================

                item.TrackStock =
                    Bool(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "trackstock",
                            "track_stock"));

                item.AvailableQuantity =
                    Int(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "availablequantity",
                            "available_quantity"));

                item.MinStock =
                    Int(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "minstock",
                            "min_stock"));

                item.OutOfStockWarning =
                    Int(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "outofstockwarning",
                            "out_of_stock_warning"));


                item.IsActive =
                    Bool(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "isactive",
                            "active"));

                item.IsDeleted =
                    Bool(
                        Cell(
                            sheet,
                            row,
                            headers,
                            "isdeleted",
                            "is_deleted"));

                if (isNew)
                {
                    item.CreatedAt =
                        DateTime.UtcNow;

                    item.UpdatedAt =
                        DateTime.UtcNow;

                    _context.Tbl_Accessories
                        .Add(item);
                }
                else
                {
                    item.UpdatedAt =
                        DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }
        }


        private static string NormalizeHeader(
            string? value) =>
            (value ?? "")
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "_");

        private static string Cell(
            ExcelWorksheet sheet,
            int row,
            Dictionary<string, int> headers,
            params string[] names)
        {
            foreach (string name in names)
            {
                if (
                    headers.TryGetValue(
                        NormalizeHeader(name),
                        out int col))
                {
                    return
                        sheet.Cells[row, col]
                            .Text?
                            .Trim()
                        ?? "";
                }
            }

            return "";
        }

        private static string? NullIfEmpty(
            string? value) =>
            string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();

        private static int Int(
            string? value) =>
            int.TryParse(
                value,
                out int result)
                ? result
                : 0;


        private static int? NullableInt(
            string? value) =>
            int.TryParse(
                value,
                out int result)
                && result > 0
                ? result
                : null;

        private static decimal Decimal(
            string? value)
        {
            if (
                decimal.TryParse(
                    value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out decimal result))
            {
                return result;
            }

            return decimal.TryParse(
                    value,
                    out result)
                ? result
                : 0m;
        }


        private static bool Bool(
            string? value) =>
            value?
                .Trim()
                .ToLowerInvariant()
                is
                "1"
                or "true"
                or "yes"
                or "y";

        private static DateTime? NullableDateTime(
            ExcelWorksheet sheet,
            int row,
            Dictionary<string, int> headers,
            params string[] names)
        {
            foreach (string name in names)
            {
                if (
                    !headers.TryGetValue(
                        NormalizeHeader(name),
                        out int col))
                {
                    continue;
                }

                var cell =
                    sheet.Cells[row, col];

                if (
                    cell.Value == null &&
                    string.IsNullOrWhiteSpace(
                        cell.Text))
                {
                    return null;
                }

                if (
                    cell.Value is DateTime dateValue)
                {
                    return dateValue.Date;
                }

                if (
                    double.TryParse(
                        cell.Value?.ToString(),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out double serialDate))
                {
                    try
                    {
                        return
                            DateTime
                                .FromOADate(
                                    serialDate)
                                .Date;
                    }
                    catch (ArgumentException)
                    {
                        // Continue with text parsing.
                    }
                }

                string text =
                    cell.Text?.Trim()
                    ?? "";

                string[] formats =
                {
                    "yyyy-MM-dd",
                    "dd-MM-yyyy",
                    "dd/MM/yyyy",
                    "MM/dd/yyyy",
                    "yyyy/MM/dd",
                    "yyyy-MM-dd HH:mm:ss"
                };

                if (
                    DateTime.TryParseExact(
                        text,
                        formats,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime parsed))
                {
                    return parsed.Date;
                }

                if (
                    DateTime.TryParse(
                        text,
                        CultureInfo.CurrentCulture,
                        DateTimeStyles.None,
                        out parsed))
                {
                    return parsed.Date;
                }

                throw new ArgumentException(
                    $"Invalid ManufacturingDate at Excel row {row}. " +
                    "Use yyyy-MM-dd or dd-MM-yyyy.");
            }

            return null;
        }
    }
}