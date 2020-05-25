using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public string RefreshToken { get; set; }
    }
}
