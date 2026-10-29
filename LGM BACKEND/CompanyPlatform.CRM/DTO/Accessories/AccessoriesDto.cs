using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CompanyPlatform.CRM.DTO.Accessories
{
    public class AccessoriesDto
    {
        public int AccessoriesId { get; set; }

        [StringLength(50)]
        public string? AccessoriesCode { get; set; }

        [StringLength(50)]
        public string? AccessorySKU { get; set; }

        [StringLength(100)]
        public string? PartNo { get; set; }

        [StringLength(50)]
        public string? UOM { get; set; }

        [Required]
        [StringLength(150)]
        public string? AccessoriesName { get; set; }

        public IFormFile? AccessoriesImageFile { get; set; }

        public string? AccessoriesImage { get; set; }

        // Foreign Key
        public int? VendorId { get; set; }

        // Stored vendor name
        [StringLength(150)]
        public string? VendorName { get; set; }

        public int? ProductGroupId { get; set; }

        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }

        public int? UnitId { get; set; }

        public int? TaxId { get; set; }

        public int? VersionId { get; set; }

        public int? CurrencyId { get; set; }

        public string? AccessoriesDescription { get; set; }

        public string? Specification { get; set; }

        [StringLength(100)]
        public string? WarrantyPeriod { get; set; }

        public DateTime? ManufacturingDate { get; set; }

        public bool IsCalibration { get; set; } = false;

        [StringLength(100)]
        public string? CalibrationFrequency { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public bool TrackStock { get; set; } = true;

        public int AvailableQuantity { get; set; }

        public int MinStock { get; set; }

        public int OutOfStockWarning { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}