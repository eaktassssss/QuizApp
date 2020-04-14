using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Business.Security.Microsoft.Jwt.Model
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
