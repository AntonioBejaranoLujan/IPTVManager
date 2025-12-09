using IPTVManager.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IPTVManager.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets - Cada uno representa una tabla en la base de datos
    public DbSet<License> Licenses { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistItem> PlaylistItems { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // License -> User (muchos a uno - AHORA OPCIONAL)
        builder.Entity<License>()
            .HasOne(l => l.User)
            .WithMany(u => u.Licenses)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.SetNull) // ← Cambiado de Cascade a SetNull
            .IsRequired(false); // ← Agregado: la relación es opcional

        // Playlist -> User (muchos a uno)
        builder.Entity<Playlist>()
            .HasOne(p => p.User)
            .WithMany(u => u.Playlists)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // PlaylistItem -> Playlist (muchos a uno)
        builder.Entity<PlaylistItem>()
            .HasOne(pi => pi.Playlist)
            .WithMany(p => p.Items)
            .HasForeignKey(pi => pi.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);

        // Device -> User (muchos a uno)
        builder.Entity<Device>()
            .HasOne(d => d.User)
            .WithMany(u => u.Devices)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // AuditLog -> User (opcional)
        builder.Entity<AuditLog>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.Entity<License>()
            .HasIndex(l => l.ExpirationDate);

        builder.Entity<Device>()
            .HasIndex(d => d.MacAddress)
            .IsUnique();

        builder.Entity<AuditLog>()
            .HasIndex(a => a.Timestamp);

        // Precisión decimal
        builder.Entity<License>()
            .Property(l => l.Cost)
            .HasPrecision(18, 2);
    }
}
