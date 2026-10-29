using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyPlatform.Core.Models.Commonmaster;
using CompanyPlatform.Core.Models.CRM.Dashboards;
using CompanyPlatform.Core.Models.Inventory;
using Microsoft.AspNetCore.Http;

namespace CompanyPlatform.Core.Models.CRM
{
    public class Accessories
    {
        [Key]
        public int AccessoriesId { get; set; }

        [Required]
        [StringLength(50)]
        public string AccessoriesCode { get; set; } = string.Empty;

        [StringLength(50)]
        public string? AccessorySKU { get; set; }

        [StringLength(100)]
        public string? PartNo { get; set; }

        [StringLength(50)]
        public string? UOM { get; set; }

        [Required]
        [StringLength(150)]
        public string AccessoriesName { get; set; } = string.Empty;

        public string? AccessoriesImage { get; set; }

        [NotMapped]
        public IFormFile? AccessoriesImageFile { get; set; }


        // Foreign Key
        public int? VendorId { get; set; }

        [ForeignKey(nameof(VendorId))]
        public virtual Vendor? Vendor { get; set; }

        // Vendor name is also stored in Accessories table
        // for display/history purposes.
        [StringLength(150)]
        public string? VendorName { get; set; }

        public int? ProductGroupId { get; set; }

        [ForeignKey(nameof(ProductGroupId))]
        public virtual ProductGroup? ProductGroup { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }

        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand? Brand { get; set; }

        public int? UnitId { get; set; }

        [ForeignKey(nameof(UnitId))]
        public virtual Unit? Unit { get; set; }

        public int? TaxId { get; set; }

        [ForeignKey(nameof(TaxId))]
        public virtual Tax? Tax { get; set; }

        public int? VersionId { get; set; }

        [ForeignKey(nameof(VersionId))]
        public virtual ProductVersion? Version { get; set; }

        public int? CurrencyId { get; set; }

        [ForeignKey(nameof(CurrencyId))]
        public virtual Currency? Currency { get; set; }

        public string? AccessoriesDescription { get; set; }

        public string? Specification { get; set; }

        [StringLength(100)]
        public string? WarrantyPeriod { get; set; }

        public DateTime? ManufacturingDate { get; set; }

        public bool IsCalibration { get; set; } = false;

        [StringLength(100)]
        public string? CalibrationFrequency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; } = 0m;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; } = 0m;

        public bool TrackStock { get; set; } = true;

        public int AvailableQuantity { get; set; } = 0;

        public int MinStock { get; set; } = 0;

        public int OutOfStockWarning { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}