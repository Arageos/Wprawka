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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key dla PlayerGame
            modelBuilder.Entity<PlayerGame>()
                .HasKey(pg => new { pg.PlayerId, pg.GameId });

            // Relacja PlayerGame -> Player
            modelBuilder.Entity<PlayerGame>()
                .HasOne(pg => pg.Player)
                .WithMany(p => p.Games)
                .HasForeignKey(pg => pg.PlayerId);

            // Relacja PlayerGame -> Game
            modelBuilder.Entity<PlayerGame>()
                .HasOne(pg => pg.Game)
                .WithMany(g => g.Players)
                .HasForeignKey(pg => pg.GameId);
        }
    }
}