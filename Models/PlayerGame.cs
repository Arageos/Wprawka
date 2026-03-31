using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Models
{
    public class PlayerGame
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        public Player? Player { get; set; }
        public Game? Game { get; set; }
    }
}