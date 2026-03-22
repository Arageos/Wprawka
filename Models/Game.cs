using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();

        public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    }
}