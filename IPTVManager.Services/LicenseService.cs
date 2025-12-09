using IPTVManager.Data;
using IPTVManager.Domain.Entities;
using IPTVManager.Services.Interfaces;
using IPTVManager.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IPTVManager.Services;

public class LicenseService : ILicenseService
{
    private readonly ApplicationDbContext _context;

    public LicenseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LicenseDto>> GetAllLicensesAsync()
    {
        var licenses = await _context.Licenses
            .OrderByDescending(l => l.PurchaseDate)
            .ToListAsync();

        return licenses.Select(MapToDto);
    }

    public async Task<IEnumerable<LicenseDto>> GetUserLicensesAsync(string userId)
    {
        var licenses = await _context.Licenses
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.ExpirationDate)
            .ToListAsync();

        return licenses.Select(MapToDto);
    }

    public async Task<LicenseDto?> GetLicenseByIdAsync(int id)
    {
        var license = await _context.Licenses.FindAsync(id);
        
        return license == null ? null : MapToDto(license);
    }

    public async Task<LicenseDto> CreateLicenseAsync(CreateLicenseDto licenseDto)
    {
        if (licenseDto.ExpirationDate < DateTime.UtcNow)
        {
            throw new ArgumentException("No se puede crear una licencia ya expirada");
        }

        var license = new License
        {
            Name = licenseDto.Name,
            Provider = licenseDto.Provider,
            LicenseKey = licenseDto.LicenseKey,
            Username = licenseDto.Username,
            Password = licenseDto.Password,
            PurchaseDate = licenseDto.PurchaseDate,
            ExpirationDate = licenseDto.ExpirationDate,
            IsActive = licenseDto.IsActive,
            Cost = licenseDto.Cost,
            Notes = licenseDto.Notes,
            UserId = licenseDto.UserId
        };

        _context.Licenses.Add(license);
        await _context.SaveChangesAsync();
        
        return MapToDto(license);
    }

    public async Task<LicenseDto> UpdateLicenseAsync(int id, CreateLicenseDto licenseDto)
    {
        var license = await _context.Licenses.FindAsync(id);
        
        if (license == null)
            throw new KeyNotFoundException($"Licencia con ID {id} no encontrada");

        license.Name = licenseDto.Name;
        license.Provider = licenseDto.Provider;
        license.LicenseKey = licenseDto.LicenseKey;
        license.Username = licenseDto.Username;
        license.Password = licenseDto.Password;
        license.PurchaseDate = licenseDto.PurchaseDate;
        license.ExpirationDate = licenseDto.ExpirationDate;
        license.IsActive = licenseDto.IsActive;
        license.Cost = licenseDto.Cost;
        license.Notes = licenseDto.Notes;
        license.UserId = licenseDto.UserId;

        await _context.SaveChangesAsync();
        
        return MapToDto(license);
    }

    public async Task<bool> DeleteLicenseAsync(int id)
    {
        var license = await _context.Licenses.FindAsync(id);
        
        if (license == null)
            return false;

        _context.Licenses.Remove(license);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<LicenseDto>> GetExpiringLicensesAsync(int daysThreshold)
    {
        var thresholdDate = DateTime.UtcNow.AddDays(daysThreshold);
        
        var licenses = await _context.Licenses
            .Where(l => l.ExpirationDate <= thresholdDate && l.ExpirationDate >= DateTime.UtcNow)
            .OrderBy(l => l.ExpirationDate)
            .ToListAsync();

        return licenses.Select(MapToDto);
    }

    private static LicenseDto MapToDto(License license)
    {
        return new LicenseDto
        {
            Id = license.Id,
            Name = license.Name,
            Provider = license.Provider,
            LicenseKey = license.LicenseKey,
            Username = license.Username,
            Password = license.Password,
            PurchaseDate = license.PurchaseDate,
            ExpirationDate = license.ExpirationDate,
            IsActive = license.IsActive,
            Cost = license.Cost,
            Notes = license.Notes,
            UserId = license.UserId,
            IsExpired = license.IsExpired,
            DaysUntilExpiration = license.DaysUntilExpiration
        };
    }
}
