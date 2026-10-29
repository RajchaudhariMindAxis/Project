using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyPlatform.Core.Models.Inventory;

namespace CompanyPlatform.Core.Models.CRM
{
    [Table("Tbl_DocumentSeries")]
    public class DocumentSeries
    {
        [Key]
        public int DocumentSeriesId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Prefix { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int NextNumber { get; set; } = 1;

        [Required]
        [MaxLength(20)]
        public string FinancialYear { get; set; } = string.Empty;

        [Required]
        public int BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual Branch? Branch { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
    }
}