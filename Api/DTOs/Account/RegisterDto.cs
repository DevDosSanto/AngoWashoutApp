using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password  deve conter {1} ou mais caracteres ")]
        public string Password { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "O primeiro nome deve conter {1} ou mais caracteres ")]
        public string PrimeiroNome { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "O Segundo nome deve conter {1} ou mais caracteres ")]
        public string SegundoNome { get; set; }
        public string? img { get; set; }

    }
}
