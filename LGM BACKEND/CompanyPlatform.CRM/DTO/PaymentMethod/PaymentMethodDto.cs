using System.ComponentModel.DataAnnotations;

namespace CompanyPlatform.CRM.DTO.PaymentMethod
{
    public class PaymentMethodDto
    {
        public int PaymentMethodId { get; set; }

        [Required]
        [StringLength(30)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}