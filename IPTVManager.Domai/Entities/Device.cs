namespace IPTVManager.Domain.Entities;

/// <summary>
/// Dispositivo registrado del usuario
/// </summary>
public class Device
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DeviceType { get; set; } = string.Empty;
    public string? Model { get; set; }
    
    public string MacAddress { get; set; } = string.Empty;
    public string? IpAddress { get; set; }
    
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastAccessAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    public bool RequiresParentalControl { get; set; } = false;
    
    // Relación con usuario
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}