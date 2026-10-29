using System.ComponentModel.DataAnnotations;

namespace CompanyPlatform.Core.DTOs.CRM
{
    public class ProductAccessoryPriceDto : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PriceListId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ItemId { get; set; }

        [Required]
        [RegularExpression(
            "^(Product|Accessory)$",
            ErrorMessage = "ItemType must be Product or Accessory.")]
        public string ItemType { get; set; } = "Product";

        [Required]
        [StringLength(30)]
        public string PriceType { get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal Amount { get; set; }

        [Range(1, int.MaxValue)]
        public int MinQty { get; set; } = 1;

        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; } = true;

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (EffectiveTo.HasValue &&
                EffectiveTo.Value.Date < EffectiveFrom.Date)
            {
                yield return new ValidationResult(
                    "EffectiveTo cannot be earlier than EffectiveFrom.",
                    new[] { nameof(EffectiveTo) });
            }
        }
    }
}