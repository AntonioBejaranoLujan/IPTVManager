using System.Text.Json.Serialization;

namespace IPTVManager.Domain.Entities;

/// <summary>
/// Licencia de servicio IPTV (Netflix, HBO, etc.)
/// </summary>
public class License
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string LicenseKey { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Password { get; set; }

    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; } = true;

    public decimal Cost { get; set; }
    public string? Notes { get; set; }

    // Relación con usuario (AHORA OPCIONAL)
    public string? UserId { get; set; } // ← Agregado el ?
    
    [JsonIgnore]
    public ApplicationUser? User { get; set; } // ← Agregado el ?

    // Propiedades calculadas
    [JsonIgnore]
    public bool IsExpired => ExpirationDate < DateTime.UtcNow;
    
    [JsonIgnore]
    public int DaysUntilExpiration => (ExpirationDate - DateTime.UtcNow).Days;
}