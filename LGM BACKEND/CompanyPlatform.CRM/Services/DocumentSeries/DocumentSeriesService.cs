using CompanyPlatform.Core.Data;
using CompanyPlatform.CRM.DTO.DocumentSeries;
using Microsoft.EntityFrameworkCore;

namespace CompanyPlatform.CRM.Services.DocumentSeries
{
    public class DocumentSeriesService : IDocumentSeriesService
    {
        private readonly ApplicationDBContext _context;

        private const string CodePrefix = "LGM-DOCS-";

        public DocumentSeriesService(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<DocumentSeriesDto>> GetAllAsync()
        {
            return await _context.Tbl_DocumentSeries
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .OrderBy(d => d.DocumentType)
                .ThenBy(d => d.FinancialYear)
               .Select(d => new DocumentSeriesDto
               {
                   DocumentSeriesId = d.DocumentSeriesId,
                   Code = d.Code,
                   DocumentType = d.DocumentType,
                   Prefix = d.Prefix,
                   NextNumber = d.NextNumber,
                   FinancialYear = d.FinancialYear,
                   BranchId = d.BranchId,
                   IsActive = d.IsActive,
                   IsDeleted = d.IsDeleted
               })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<DocumentSeriesDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Tbl_DocumentSeries
                .AsNoTracking()
                .FirstOrDefaultAsync(d =>
                    d.DocumentSeriesId == id && !d.IsDeleted);

            return entity == null ? null : MapToDto(entity);
        }

        // GENERATE CODE: LGM-DSER-0001, LGM-DSER-0002, ...
        public async Task<string> GenerateCodeAsync()
        {
            var existingCodes = await _context.Tbl_DocumentSeries
                .AsNoTracking()
                .Where(d => d.Code.StartsWith(CodePrefix))
                .Select(d => d.Code)
                .ToListAsync();

            int maxNumber = 0;

            foreach (var existingCode in existingCodes)
            {
                if (string.IsNullOrWhiteSpace(existingCode) ||
                    existingCode.Length <= CodePrefix.Length)
                {
                    continue;
                }

                var suffix = existingCode.Substring(CodePrefix.Length);

                if (int.TryParse(suffix, out int number) &&
                    number > maxNumber)
                {
                    maxNumber = number;
                }
            }

            return $"{CodePrefix}{maxNumber + 1:D4}";
        }

        // CREATE
        public async Task<DocumentSeriesDto> CreateAsync(
            DocumentSeriesDto dto)
        {
            Validate(dto);

            string documentType = dto.DocumentType.Trim();
            string financialYear = dto.FinancialYear.Trim();
            string prefix = dto.Prefix.Trim();

            await ValidateBranchAsync(dto.BranchId);

            bool duplicateSeries = await _context.Tbl_DocumentSeries
                .AnyAsync(d =>
                    d.DocumentType == documentType &&
                    d.FinancialYear == financialYear &&
                    d.BranchId == dto.BranchId &&
                    !d.IsDeleted);

            if (duplicateSeries)
            {
                throw new InvalidOperationException(
                    "A document series already exists for this document type, financial year, and branch.");
            }

            // Generate a code if the client did not send one.
            string code = string.IsNullOrWhiteSpace(dto.Code)
                ? await GenerateCodeAsync()
                : dto.Code.Trim();

            if (code.Length > 50)
            {
                throw new ArgumentException(
                    "Code cannot exceed 50 characters.");
            }

            // Check all records, including soft-deleted records.
            bool duplicateCode = await _context.Tbl_DocumentSeries
                .AnyAsync(d => d.Code == code);

            if (duplicateCode)
            {
                throw new InvalidOperationException(
                    "A document series with this code already exists. Please generate a new code.");
            }

            var entity = new CompanyPlatform.Core.Models.CRM.DocumentSeries
            {
                Code = code,
                DocumentType = documentType,
                Prefix = prefix,
                NextNumber = dto.NextNumber,
                FinancialYear = financialYear,
                BranchId = dto.BranchId,
                IsActive = dto.IsActive,
                IsDeleted = false
            };

            _context.Tbl_DocumentSeries.Add(entity);

            await _context.SaveChangesAsync();

            return MapToDto(entity);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            DocumentSeriesDto dto)
        {
            Validate(dto);

            var entity = await _context.Tbl_DocumentSeries
                .FirstOrDefaultAsync(d =>
                    d.DocumentSeriesId == id && !d.IsDeleted);

            if (entity == null)
                return false;

            string documentType = dto.DocumentType.Trim();
            string financialYear = dto.FinancialYear.Trim();
            string prefix = dto.Prefix.Trim();

            await ValidateBranchAsync(dto.BranchId);

            bool duplicateSeries = await _context.Tbl_DocumentSeries
                .AnyAsync(d =>
                    d.DocumentSeriesId != id &&
                    d.DocumentType == documentType &&
                    d.FinancialYear == financialYear &&
                    d.BranchId == dto.BranchId &&
                    !d.IsDeleted);

            if (duplicateSeries)
            {
                throw new InvalidOperationException(
                    "Another document series already exists for this document type, financial year, and branch.");
            }

            if (dto.NextNumber < entity.NextNumber)
            {
                throw new ArgumentException(
                    "NextNumber cannot be lower than its current value.");
            }

            // Preserve the existing code if the client omits it.
            string code = string.IsNullOrWhiteSpace(dto.Code)
                ? entity.Code
                : dto.Code.Trim();

            if (code.Length > 50)
            {
                throw new ArgumentException(
                    "Code cannot exceed 50 characters.");
            }

            bool duplicateCode = await _context.Tbl_DocumentSeries
                .AnyAsync(d =>
                    d.DocumentSeriesId != id &&
                    d.Code == code);

            if (duplicateCode)
            {
                throw new InvalidOperationException(
                    "Another document series with this code already exists.");
            }

            entity.Code = code;
            entity.DocumentType = documentType;
            entity.Prefix = prefix;
            entity.NextNumber = dto.NextNumber;
            entity.FinancialYear = financialYear;
            entity.BranchId = dto.BranchId;
            entity.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE - SOFT DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Tbl_DocumentSeries
                .FirstOrDefaultAsync(d =>
                    d.DocumentSeriesId == id && !d.IsDeleted);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            entity.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }

        // VALIDATION
        private static void Validate(DocumentSeriesDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.DocumentType))
                throw new ArgumentException("DocumentType is required.");

            if (string.IsNullOrWhiteSpace(dto.Prefix))
                throw new ArgumentException("Prefix is required.");

            if (string.IsNullOrWhiteSpace(dto.FinancialYear))
                throw new ArgumentException("FinancialYear is required.");

            if (dto.DocumentType.Trim().Length > 50)
                throw new ArgumentException(
                    "DocumentType cannot exceed 50 characters.");

            if (dto.Prefix.Trim().Length > 20)
                throw new ArgumentException(
                    "Prefix cannot exceed 20 characters.");

            if (dto.FinancialYear.Trim().Length > 20)
                throw new ArgumentException(
                    "FinancialYear cannot exceed 20 characters.");

            if (dto.NextNumber < 1)
                throw new ArgumentException(
                    "NextNumber must be at least 1.");

            if (dto.BranchId < 1)
                throw new ArgumentException(
                    "A valid BranchId is required.");

            if (dto.Code?.Trim().Length > 50)
                throw new ArgumentException(
                    "Code cannot exceed 50 characters.");
        }

        // VALIDATE BRANCH
        private async Task ValidateBranchAsync(int branchId)
        {
            bool branchExists = await _context.Branches
                .AnyAsync(b => b.BranchId == branchId);

            if (!branchExists)
            {
                throw new ArgumentException(
                    "The selected branch does not exist.");
            }
        }

        // ENTITY TO DTO
        private static DocumentSeriesDto MapToDto(
            CompanyPlatform.Core.Models.CRM.DocumentSeries entity)
        {
            return new DocumentSeriesDto
            {
                DocumentSeriesId = entity.DocumentSeriesId,
                Code = entity.Code,
                DocumentType = entity.DocumentType,
                Prefix = entity.Prefix,
                NextNumber = entity.NextNumber,
                FinancialYear = entity.FinancialYear,
                BranchId = entity.BranchId,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted
            };
        }
    }
}