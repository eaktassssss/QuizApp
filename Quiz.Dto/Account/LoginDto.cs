using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quiz.Dto.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Zorunlu alan")]
        [EmailAddress(ErrorMessage = "Email format dışı")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifre min 6 karakter olmalıdır")]
        [MaxLength(16, ErrorMessage = "Şifre max 16 karakter olmalıdır")]
        public string Password { get; set; }
    }
}
