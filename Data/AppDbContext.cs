using Microsoft.EntityFrameworkCore;
using GamePlatform.Models;

namespace GamePlatform.Data
{
    public class GamePlatformContext : DbContext
    {
        public GamePlatformContext(DbContextOptions options) :
            base(options)
        { }

        public DbSet<Player> Player { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Achievement> Achievement { get; set; }
        public DbSet<PlayerGame> PlayerGame { get; set; }
        public DbSet<AppUser> AppUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerGame>()
                .HasKey(pg => new { pg.PlayerId, pg.GameId });

            modelBuilder.Entity<PlayerGame>()
                .HasOne(pg => pg.Player)
                .WithMany(p => p.Games)
                .HasForeignKey(pg => pg.PlayerId);

            modelBuilder.Entity<PlayerGame>()
                .HasOne(pg => pg.Game)
                .WithMany(g => g.Players)
                .HasForeignKey(pg => pg.GameId);

            // Unikalny login i email
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}