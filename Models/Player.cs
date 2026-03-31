using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50, ErrorMessage = "Nazwa nie może przekraczać 50 znaków")]
        public string Username { get; set; }

        [MaxLength(100, ErrorMessage = "Email nie może przekraczać 100 znaków")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy adres email (wymagany znak @)")]
        public string? Email { get; set; }

        public ICollection<PlayerGame> Games { get; set; } = new List<PlayerGame>();
    }
}