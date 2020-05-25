using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quiz.Dto.Account
{
    public class RegisterDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        [EmailAddress(ErrorMessage = "Email format dışı")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifre min 6 karakter olmalıdır")]
        [MaxLength(16, ErrorMessage = "Şifre max 16 karakter olmalıdır")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Zorunlu tekrar alan")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifre tekrar min 6 karakter olmalıdır")]
        [MaxLength(16, ErrorMessage = "Şifre tekrar max 16 karakter olmalıdır")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")]
        public string PasswordConfrim { get; set; }
    }
}
