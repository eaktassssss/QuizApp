using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Quiz.Business.Security.Microsoft.Jwt.Concrete
{
    public static class SignHandler
    {
        public static SecurityKey GetSecurityKey(string securityKey)
        {
            /*
             * Token oluşturmada ve doğrulanmasından tek bir security key kullanıldığı için SymmetricSecurityKey dönüyoruz
             */
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
