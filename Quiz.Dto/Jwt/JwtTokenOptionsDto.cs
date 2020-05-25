using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Quiz.Dto.Jwt
{
    public class JwtTokenOptionsDto
    {
        public int RefreshTokenExpiration { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
