using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CompanyPlatform.Core.Data;
using CompanyPlatform.CRM.DTO.Accessories;
using CompanyPlatform.CRM.Services.Accessories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace PayrollManagementAPI.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoriesController : ControllerBase
    {
        private readonly IAccessoriesService _service;
        private readonly ApplicationDBContext _context;

        public AccessoriesController(
            IAccessoriesService service,
            ApplicationDBContext context)
        {
            _service = service;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(
                await _service.GetAllAsync());
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message =
                        "Invalid accessory ID."
                });
            }

            try
            {
                return Ok(
                    await _service.GetByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    Message =
                        ex.Message
                });
            }
        }


        [HttpGet("generate-code")]
        public async Task<IActionResult> GenerateCode()
        {
            try
            {
                var code =
                    await _service.GenerateAccessoryCode();

                return Ok(new
                {
                    AccessoriesCode =
                        code
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Message =
                        ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    Message =
                        ex.Message
                });
            }
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post(
            [FromForm] AccessoriesDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(
                    ModelState);

            try
            {
                int id =
                    await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(Get),
                    new { id },
                    new
                    {
                        AccessoriesId = id,

                        Message =
                            "Accessory created successfully."
                    });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Message =
                        ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    Message =
                        ex.Message
                });
            }
        }


        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put(
            int id,
            [FromForm] AccessoriesDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message =
                        "Invalid accessory ID."
                });
            }

            if (
                dto.AccessoriesId != 0 &&
                dto.AccessoriesId != id)
            {
                return BadRequest(new
                {
                    Message =
                        "Route ID and AccessoriesId do not match."
                });
            }

            dto.AccessoriesId =
                id;

            if (!ModelState.IsValid)
                return ValidationProblem(
                    ModelState);

            try
            {
                bool updated =
                    await _service.UpdateAsync(dto);

                if (!updated)
                {
                    return NotFound(new
                    {
                        Message =
                            "Accessory not found."
                    });
                }

                return Ok(new
                {
                    Message =
                        "Accessory updated successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Message =
                        ex.Message
                });
            }
        }


        [HttpGet("{id:int}/logs")]
        public async Task<IActionResult> GetAccessoryLogs(
            int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message =
                        "Invalid accessory ID."
                });
            }

            return Ok(
                await _service.GetLogsAsync(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message =
                        "Invalid accessory ID."
                });
            }

            bool deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message =
                        "Accessory not found."
                });
            }

            return Ok(new
            {
                Message =
                    "Accessory deleted successfully."
            });
        }


        [HttpPost("upload-excel")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadExcel(
            [FromForm] IFormFile? file)
        {
            if (
                file == null ||
                file.Length == 0)
            {
                return BadRequest(new
                {
                    Message =
                        "Please upload a valid Excel file."
                });
            }

            if (
                !string.Equals(
                    Path.GetExtension(
                        file.FileName),
                    ".xlsx",
                    StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new
                {
                    Message =
                        "Only .xlsx files are supported."
                });
            }

            try
            {
                await _service
                    .ImportAccessoriesAsync(file);

                return Ok(new
                {
                    Message =
                        "Accessories imported successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Message =
                        ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    Message =
                        ex.Message
                });
            }
        }

        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportExcel()
        {
            var accessories =
                await _context.Tbl_Accessories
                    .AsNoTracking()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.AccessoriesId)
                    .ToListAsync();

            ExcelPackage.License
                .SetNonCommercialOrganization(
                    "CompanyPlatform");

            using var package =
                new ExcelPackage();

            var sheet =
                package.Workbook
                    .Worksheets
                    .Add("Accessories");

            // =====================================================
            // EXCEL HEADERS
            // =====================================================

            string[] headers =
            {
                "AccessoriesId",
                "AccessoriesCode",
                "AccessorySKU",
                "PartNo",
                "UOM",
                "AccessoriesName",
                "AccessoriesImage",

                "VendorId",
                "VendorName",

                "ProductGroupId",
                "CategoryId",
                "BrandId",
                "UnitId",
                "TaxId",
                "CurrencyId",

                "AccessoriesDescription",
                "Specification",
                "WarrantyPeriod",
                "ManufacturingDate",

                "IsCalibration",
                "CalibrationFrequency",

                "PurchasePrice",
                "SalePrice",

                "TrackStock",
                "AvailableQuantity",
                "MinStock",
                "OutOfStockWarning",

                "IsActive",
                "IsDeleted",

                "CreatedAt",
                "UpdatedAt"
            };

            for (
                int col = 0;
                col < headers.Length;
                col++)
            {
                sheet.Cells[
                    1,
                    col + 1]
                    .Value =
                        headers[col];
            }

            int row = 2;

            foreach (var item in accessories)
            {
                object?[] values =
                {
                    item.AccessoriesId,

                    item.AccessoriesCode,

                    item.AccessorySKU,

                    item.PartNo,

                    item.UOM,

                    item.AccessoriesName,

                    item.AccessoriesImage,

                    // Vendor
                    item.VendorId,

                    item.VendorName,

                    // Master
                    item.ProductGroupId,

                    item.CategoryId,

                    item.BrandId,

                    item.UnitId,

                    item.TaxId,

                    item.CurrencyId,

                    // Description
                    item.AccessoriesDescription,

                    item.Specification,

                    item.WarrantyPeriod,

                    item.ManufacturingDate,

                    // Calibration
                    item.IsCalibration,

                    item.CalibrationFrequency,

                    // Pricing
                    item.PurchasePrice,

                    item.SalePrice,

                    // Stock
                    item.TrackStock,

                    item.AvailableQuantity,

                    item.MinStock,

                    item.OutOfStockWarning,

                    // Status
                    item.IsActive,

                    item.IsDeleted,

                    // Audit
                    item.CreatedAt,

                    item.UpdatedAt
                };

                for (
                    int col = 0;
                    col < values.Length;
                    col++)
                {
                    sheet.Cells[
                        row,
                        col + 1]
                        .Value =
                            values[col];
                }

                // ManufacturingDate = column 19
                sheet.Cells[
                    row,
                    19]
                    .Style
                    .Numberformat
                    .Format =
                        "dd-MM-yyyy";

                // PurchasePrice = column 22
                sheet.Cells[
                    row,
                    22]
                    .Style
                    .Numberformat
                    .Format =
                        "#,##0.00";

                // SalePrice = column 23
                sheet.Cells[
                    row,
                    23]
                    .Style
                    .Numberformat
                    .Format =
                        "#,##0.00";

                // CreatedAt = column 31
                sheet.Cells[
                    row,
                    31]
                    .Style
                    .Numberformat
                    .Format =
                        "yyyy-MM-dd HH:mm:ss";

                // UpdatedAt = column 32
                sheet.Cells[
                    row,
                    32]
                    .Style
                    .Numberformat
                    .Format =
                        "yyyy-MM-dd HH:mm:ss";

                row++;
            }

            var header =
                sheet.Cells[
                    1,
                    1,
                    1,
                    headers.Length];

            header.Style.Font.Bold =
                true;

            header.Style.Fill.PatternType =
                OfficeOpenXml.Style
                    .ExcelFillStyle.Solid;

            header.Style.Fill
                .BackgroundColor
                .SetColor(
                    System.Drawing.Color.DarkBlue);

            header.Style.Font.Color
                .SetColor(
                    System.Drawing.Color.White);

            header.Style.HorizontalAlignment =
                OfficeOpenXml.Style
                    .ExcelHorizontalAlignment
                    .Center;

            if (sheet.Dimension != null)
            {
                sheet.Cells[
                    sheet.Dimension.Address]
                    .AutoFitColumns();

                sheet.View
                    .FreezePanes(2, 1);

                sheet.Cells[
                    1,
                    1,
                    1,
                    headers.Length]
                    .AutoFilter = true;
            }


            byte[] bytes =
                await package
                    .GetAsByteArrayAsync();

            Response.Headers[
                "Cache-Control"] =
                    "no-cache, no-store, must-revalidate";

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Accessories_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
    }
}