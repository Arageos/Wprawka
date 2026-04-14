using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Login jest wymagany")]
        [MaxLength(50)]
        [Display(Name = "Login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [Display(Name = "Hasło")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [MaxLength(50)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [MaxLength(50)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy adres email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}