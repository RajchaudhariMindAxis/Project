using CompanyPlatform.Core.Data;
using CompanyPlatform.CRM.DTO.Reason;
using Microsoft.EntityFrameworkCore;

namespace CompanyPlatform.CRM.Services.Reason
{
    public class ReasonService : IReasonService
    {
        private readonly ApplicationDBContext _context;

        public ReasonService(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<ReasonDto>> GetAllAsync()
        {
            return await _context.Tbl_Reasons
                .AsNoTracking()
                .Where(r => !r.IsDeleted)
                .OrderBy(r => r.Name)
                .Select(r => new ReasonDto
                {
                    ReasonId = r.ReasonId,
                    Code = r.Code,
                    Name = r.Name,
                    ReasonType = r.ReasonType,
                    Description = r.Description,
                    IsActive = r.IsActive
                })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<ReasonDto?> GetByIdAsync(int id)
        {
            var reason = await _context.Tbl_Reasons
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.ReasonId == id && !r.IsDeleted);

            return reason == null ? null : MapToDto(reason);
        }

        // CREATE
        public async Task<ReasonDto> CreateAsync(ReasonDto dto)
        {
            Validate(dto);

            string code = dto.Code.Trim();

            bool duplicateCode = await _context.Tbl_Reasons
                .AnyAsync(r =>
                    r.Code == code && !r.IsDeleted);

            if (duplicateCode)
            {
                throw new InvalidOperationException(
                    "A reason with this code already exists.");
            }

            var reason = new CompanyPlatform.Core.Models.CRM.Reason
            {
                Code = code,
                Name = dto.Name.Trim(),
                ReasonType = dto.ReasonType.Trim(),
                Description = dto.Description?.Trim(),
                IsActive = dto.IsActive,
                IsDeleted = false
            };

            _context.Tbl_Reasons.Add(reason);

            await _context.SaveChangesAsync();

            return MapToDto(reason);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(int id, ReasonDto dto)
        {
            Validate(dto);

            var reason = await _context.Tbl_Reasons
                .FirstOrDefaultAsync(r =>
                    r.ReasonId == id && !r.IsDeleted);

            if (reason == null)
                return false;

            string code = dto.Code.Trim();

            bool duplicateCode = await _context.Tbl_Reasons
                .AnyAsync(r =>
                    r.Code == code &&
                    r.ReasonId != id &&
                    !r.IsDeleted);

            if (duplicateCode)
            {
                throw new InvalidOperationException(
                    "Another reason with this code already exists.");
            }

            reason.Code = code;
            reason.Name = dto.Name.Trim();
            reason.ReasonType = dto.ReasonType.Trim();
            reason.Description = dto.Description?.Trim();
            reason.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE - SOFT DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var reason = await _context.Tbl_Reasons
                .FirstOrDefaultAsync(r =>
                    r.ReasonId == id && !r.IsDeleted);

            if (reason == null)
                return false;

            reason.IsDeleted = true;
            reason.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }

        // VALIDATION
        private static void Validate(ReasonDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Code))
                throw new ArgumentException("Reason code is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Reason name is required.");

            if (string.IsNullOrWhiteSpace(dto.ReasonType))
                throw new ArgumentException("ReasonType is required.");

            if (dto.Code.Trim().Length > 50)
                throw new ArgumentException(
                    "Reason code cannot exceed 50 characters.");

            if (dto.Name.Trim().Length > 150)
                throw new ArgumentException(
                    "Reason name cannot exceed 150 characters.");

            if (dto.ReasonType.Trim().Length > 50)
                throw new ArgumentException(
                    "ReasonType cannot exceed 50 characters.");

            if (dto.Description?.Length > 500)
                throw new ArgumentException(
                    "Description cannot exceed 500 characters.");
        }

        // ENTITY TO DTO
        private static ReasonDto MapToDto(
            CompanyPlatform.Core.Models.CRM.Reason reason)
        {
            return new ReasonDto
            {
                ReasonId = reason.ReasonId,
                Code = reason.Code,
                Name = reason.Name,
                ReasonType = reason.ReasonType,
                Description = reason.Description,
                IsActive = reason.IsActive
            };
        }
        // GENERATE REASON CODE: LGM-RSON-0001, LGM-RSON-0002, ...
        public async Task<string> GenerateCodeAsync()
        {
            const string prefix = "LGM-RSON-";

            // Include soft-deleted records so old codes are not reused.
            var existingCodes = await _context.Tbl_Reasons
                .AsNoTracking()
                .Where(r => r.Code.StartsWith(prefix))
                .Select(r => r.Code)
                .ToListAsync();

            int maxNumber = 0;

            foreach (var code in existingCodes)
            {
                if (code.Length <= prefix.Length)
                    continue;

                var suffix = code.Substring(prefix.Length);

                if (int.TryParse(suffix, out int number) && number > maxNumber)
                {
                    maxNumber = number;
                }
            }

            return $"{prefix}{maxNumber + 1:D4}";
        }
    }
}