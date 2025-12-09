namespace IPTVManager.Domain.Entities;

/// <summary>
/// Registro de auditoría de acciones
/// </summary>
public class AuditLog
{
    public int Id { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    
    public string Action { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public string? IpAddress { get; set; }
    public string? Details { get; set; }
    
    // Relación opcional con usuario
    public ApplicationUser? User { get; set; }
}