using Microsoft.AspNetCore.Identity;

namespace IPTVManager.Domain.Entities;

/// <summary>
/// Usuario del sistema extendido con datos adicionales de Identity
/// </summary>
public class ApplicationUser : IdentityUser
{
    // Datos adicionales del usuario
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    // Control parental
    public bool IsParentalControlEnabled { get; set; } = false;
    public string? ParentalControlPin { get; set; }

    // Relaciones (1 usuario tiene muchas licencias, playlists, etc.)
    public ICollection<License> Licenses { get; set; } = new List<License>();
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    public ICollection<Device> Devices { get; set; } = new List<Device>();

    // Propiedad calculada
    public string FullName => $"{FirstName} {LastName}".Trim();
}