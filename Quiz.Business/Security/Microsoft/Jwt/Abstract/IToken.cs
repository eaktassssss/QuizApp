using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Quiz.Business.Security.Microsoft.Jwt.Model;
using Quiz.Entities;

namespace Quiz.Business.Security.Microsoft.Jwt.Abstract
{
    public interface IToken
    {
        AccessToken CreateAccessToken(Users user);
        void RemoveRefreshToken(Users user);
    }
}
