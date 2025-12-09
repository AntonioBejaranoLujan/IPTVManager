using System;

namespace IPTVManager.Shared.DTOs;

public class LicenseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string LicenseKey { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Password { get; set; }
    
    public DateTime PurchaseDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    
    public decimal Cost { get; set; }
    public string? Notes { get; set; }
    
    public string? UserId { get; set; } // ← Agregado el ?
    
    // Propiedades calculadas
    public bool IsExpired { get; set; }
    public int DaysUntilExpiration { get; set; }
}
