namespace IPTVManager.Domain.Entities;

/// <summary>
/// Canal individual dentro de una playlist
/// </summary>
public class PlaylistItem
{
    public int Id { get; set; }
    public string ChannelName { get; set; } = string.Empty;
    public string ChannelUrl { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Category { get; set; }
    
    public int Order { get; set; }
    public bool IsAdultContent { get; set; } = false;
    
    // Relación con playlist
    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;
}