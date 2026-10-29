
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPlatform.Core.Models.CRM
{
    [Table("Tbl_ProductAccessoryPrices")]
    public class ProductAccessoryPrice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PriceListId { get; set; }

        [ForeignKey(nameof(PriceListId))]
        public virtual PriceList PriceList { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int ItemId { get; set; }

        // Allowed values: Product or Accessory
        [Required]
        [MaxLength(30)]
        public string ItemType { get; set; } = string.Empty;

        // Example values must be defined by business rules.
        [Required]
        [MaxLength(30)]
        public string PriceType { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal Amount { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MinQty { get; set; } = 1;

        [Required]
        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
    }
}
