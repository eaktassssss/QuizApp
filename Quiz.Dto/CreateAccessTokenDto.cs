using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quiz.Dto
{
    public class CreateAccessTokenDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
