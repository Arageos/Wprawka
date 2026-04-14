using System.ComponentModel.DataAnnotations;

namespace GamePlatform.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login jest wymagany")]
        [MaxLength(50)]
        [Display(Name = "Login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć minimum 6 znaków")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne")]
        [Display(Name = "Potwierdź hasło")]
        public string ConfirmPassword { get; set; }

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

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login jest wymagany")]
        [Display(Name = "Login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}