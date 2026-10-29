using System.ComponentModel.DataAnnotations;

namespace CompanyPlatform.CRM.DTO.BundleItem
{
    public class BundleItemDto : IValidatableObject
    {
        public int BundleItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BundleId { get; set; }

        [Range(1, int.MaxValue)]
        public int? ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int? VariantId { get; set; }

        [Range(1, int.MaxValue)]
        public int? AccessoriesId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        [Required]
        [StringLength(30)]
        public string PricingType { get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal AllocatedPrice { get; set; }

        public bool IsOptional { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            int selectedCount =
                (ProductId.HasValue ? 1 : 0) +
                (VariantId.HasValue ? 1 : 0) +
                (AccessoriesId.HasValue ? 1 : 0);

            if (selectedCount != 1)
            {
                yield return new ValidationResult(
                    "Select exactly one item: Product, Variant, or Accessory.",
                    new[]
                    {
                        nameof(ProductId),
                        nameof(VariantId),
                        nameof(AccessoriesId)
                    });
            }

            if (string.IsNullOrWhiteSpace(PricingType))
            {
                yield return new ValidationResult(
                    "PricingType is required.",
                    new[] { nameof(PricingType) });
            }
        }
    }
}