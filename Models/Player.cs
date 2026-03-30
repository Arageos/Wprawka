using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        // Nawigacja do tabeli łączącej PlayerGame
        public ICollection<PlayerGame> Games { get; set; } = new List<PlayerGame>();
    }
}