using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Models
{
    public class PlayerGame
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        // Właściwości nawigacyjne
        public Player Player { get; set; }   // <-- nawigacja do gracza
        public Game Game { get; set; }       // <-- nawigacja do gry
    }
}