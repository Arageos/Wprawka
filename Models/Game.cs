using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100, ErrorMessage = "Tytuł nie może przekraczać 100 znaków")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [MaxLength(256, ErrorMessage = "Opis nie może przekraczać 256 znaków")]
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        public ICollection<PlayerGame> Players { get; set; } = new List<PlayerGame>();
        public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    }
}