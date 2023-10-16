using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string PrimeiroNome { get; set; }

        [Required]
        public string SegundoNome { get; set; }


        [MaxLength(450)]
        public string? img { get; set; }
        public DateTime DataDeNascimento { get; set; }

    }
}
