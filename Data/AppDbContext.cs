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
            modelBuilder.Entity<Player>()
                .HasMany(e => e.Games)
                .WithMany(e => e.Players)
                .UsingEntity<PlayerGame>();
        }
    }
}