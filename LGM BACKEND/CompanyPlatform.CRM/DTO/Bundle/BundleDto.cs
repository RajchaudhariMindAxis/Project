using System.ComponentModel.DataAnnotations;

namespace CompanyPlatform.CRM.DTO.Bundle
{
    public class BundleDto : IValidatableObject
    {
        public int BundleId { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int ParentProductId { get; set; }

        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal PackagePrice { get; set; }

        [Required]
        [StringLength(30)]
        public string TaxMode { get; set; } = "Inclusive";

        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;

        public DateTime? EffectiveTo { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (EffectiveTo.HasValue &&
                EffectiveTo.Value.Date < EffectiveFrom.Date)
            {
                yield return new ValidationResult(
                    "Effective To cannot be earlier than Effective From.",
                    new[] { nameof(EffectiveTo) });
            }

            if (string.IsNullOrWhiteSpace(TaxMode) ||
                !new[] { "Inclusive", "Exclusive" }
                    .Contains(TaxMode.Trim(),
                        StringComparer.OrdinalIgnoreCase))
            {
                yield return new ValidationResult(
                    "TaxMode must be Inclusive or Exclusive.",
                    new[] { nameof(TaxMode) });
            }
        }
    }
}