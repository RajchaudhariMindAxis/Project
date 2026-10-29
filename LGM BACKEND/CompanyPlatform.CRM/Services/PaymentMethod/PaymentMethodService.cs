using CompanyPlatform.Core.Data;
using CompanyPlatform.CRM.DTO.PaymentMethod;
using CompanyPlatform.CRM.Services.PaymentMethod;
using Microsoft.EntityFrameworkCore;

// Fix: avoid conflict between the PaymentMethod namespace and entity.
using PaymentMethodEntity =
    CompanyPlatform.Core.Models.CRM.PaymentMethod;

namespace CompanyPlatform.CRM.Services.PaymentMethod
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly ApplicationDBContext _context;

        public PaymentMethodService(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<PaymentMethodDto>> GetAllAsync()
        {
            return await _context.Set<PaymentMethodEntity>()
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new PaymentMethodDto
                {
                    PaymentMethodId = x.PaymentMethodId,
                    Code = x.Code,
                    Name = x.Name,
                    Description = x.Description,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        // GET BY ID
        public async Task<PaymentMethodDto?> GetByIdAsync(int id)
        {
            var item = await _context.Set<PaymentMethodEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.PaymentMethodId == id && !x.IsDeleted);

            if (item == null)
                return null;

            return MapToDto(item);
        }

        // CREATE
        public async Task<PaymentMethodDto> CreateAsync(
            PaymentMethodDto dto)
        {
            Validate(dto);

            string code = dto.Code.Trim();

            bool codeExists = await _context
                .Set<PaymentMethodEntity>()
                .AnyAsync(x =>
                    x.Code == code &&
                    !x.IsDeleted);

            if (codeExists)
            {
                throw new ArgumentException(
                    "Payment method code already exists.");
            }

            var item = new PaymentMethodEntity
            {
                Code = code,
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim(),
                IsActive = dto.IsActive,
                IsDeleted = false
            };

            _context.Set<PaymentMethodEntity>().Add(item);

            await _context.SaveChangesAsync();

            return MapToDto(item);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(
            int id,
            PaymentMethodDto dto)
        {
            Validate(dto);

            var item = await _context
                .Set<PaymentMethodEntity>()
                .FirstOrDefaultAsync(
                    x => x.PaymentMethodId == id && !x.IsDeleted);

            if (item == null)
                return false;

            string code = dto.Code.Trim();

            bool codeExists = await _context
                .Set<PaymentMethodEntity>()
                .AnyAsync(x =>
                    x.PaymentMethodId != id &&
                    x.Code == code &&
                    !x.IsDeleted);

            if (codeExists)
            {
                throw new ArgumentException(
                    "Payment method code already exists.");
            }

            item.Code = code;
            item.Name = dto.Name.Trim();
            item.Description = dto.Description?.Trim();
            item.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        // DELETE - SOFT DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context
                .Set<PaymentMethodEntity>()
                .FirstOrDefaultAsync(
                    x => x.PaymentMethodId == id && !x.IsDeleted);

            if (item == null)
                return false;

            item.IsDeleted = true;
            item.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }

        // VALIDATION
        private static void Validate(PaymentMethodDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Code))
            {
                throw new ArgumentException(
                    "Payment method code is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException(
                    "Payment method name is required.");
            }

            if (dto.Code.Trim().Length > 30)
            {
                throw new ArgumentException(
                    "Code cannot exceed 30 characters.");
            }

            if (dto.Name.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Name cannot exceed 100 characters.");
            }

            if (dto.Description?.Length > 500)
            {
                throw new ArgumentException(
                    "Description cannot exceed 500 characters.");
            }
        }

        // ENTITY TO DTO
        private static PaymentMethodDto MapToDto(
            PaymentMethodEntity item)
        {
            return new PaymentMethodDto
            {
                PaymentMethodId = item.PaymentMethodId,
                Code = item.Code,
                Name = item.Name,
                Description = item.Description,
                IsActive = item.IsActive
            };
        }
        // GENERATE PAYMENT METHOD CODE: LGM-PYMD-0001
        public async Task<string> GenerateCodeAsync()
        {
            const string prefix = "LGM-PYMD-";

            // Include soft-deleted records to avoid reusing old codes.
            var existingCodes = await _context
                .Set<PaymentMethodEntity>()
                .AsNoTracking()
                .Where(x => x.Code.StartsWith(prefix))
                .Select(x => x.Code)
                .ToListAsync();

            int maxNumber = 0;

            foreach (var existingCode in existingCodes)
            {
                string suffix = existingCode.Substring(prefix.Length);

                if (int.TryParse(suffix, out int number) && number > maxNumber)
                {
                    maxNumber = number;
                }
            }

            int nextNumber = maxNumber + 1;

            return $"{prefix}{nextNumber:D4}";
        }
    }
}