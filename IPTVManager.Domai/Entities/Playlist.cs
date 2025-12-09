namespace IPTVManager.Domain.Entities;

/// <summary>
/// Lista IPTV (M3U) con canales
/// </summary>
public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Url { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdated { get; set; }
    
    public bool IsActive { get; set; } = true;
    public bool RequiresParentalControl { get; set; } = false;
    
    // Relación con usuario
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    
    // Relación con items
    public ICollection<PlaylistItem> Items { get; set; } = new List<PlaylistItem>();
}
