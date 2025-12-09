using IPTVManager.Domain.Entities;
using IPTVManager.Shared.DTOs;

namespace IPTVManager.Services.Interfaces;

public interface ILicenseService
{
    Task<IEnumerable<LicenseDto>> GetAllLicensesAsync();
    Task<IEnumerable<LicenseDto>> GetUserLicensesAsync(string userId);
    Task<LicenseDto?> GetLicenseByIdAsync(int id);
    Task<LicenseDto> CreateLicenseAsync(CreateLicenseDto licenseDto);
    Task<LicenseDto> UpdateLicenseAsync(int id, CreateLicenseDto licenseDto);
    Task<bool> DeleteLicenseAsync(int id);
    Task<IEnumerable<LicenseDto>> GetExpiringLicensesAsync(int daysThreshold);
}