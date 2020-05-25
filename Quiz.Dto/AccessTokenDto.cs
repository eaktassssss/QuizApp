using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Dto
{
    public class AccessTokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
