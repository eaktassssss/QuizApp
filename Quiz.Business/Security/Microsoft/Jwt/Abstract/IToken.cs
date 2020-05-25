using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Quiz.Dto;
using Quiz.Entities;

namespace Quiz.Business.Security.Microsoft.Jwt.Abstract
{
    public interface IToken
    {
        AccessTokenDto CreateAccessToken(Users user);
        void RemoveRefreshToken(Users user);
    }
}
