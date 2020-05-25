using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quiz.Business.Security.Microsoft.Jwt.Abstract;
using Quiz.Dto;
using Quiz.Dto.Jwt;
using Quiz.Entities;

namespace Quiz.Business.Security.Microsoft.Jwt.Concrete
{

    public class Token :IToken
    {
        private readonly JwtTokenOptionsDto _jwtTokenOptions;
        public Token(IOptions<JwtTokenOptionsDto> jwtTokenOptions)
        {
            _jwtTokenOptions = jwtTokenOptions.Value;
        }
        public AccessTokenDto CreateAccessToken(Users user)
        {
            try
            {
                var accessTokenExpiration = DateTime.Now.AddYears(_jwtTokenOptions.AccessTokenExpiration);
                var securityKey = SignHandler.GetSecurityKey(_jwtTokenOptions.SecurityKey);
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var token = new JwtSecurityToken(issuer: _jwtTokenOptions.Issuer, audience: _jwtTokenOptions.Audience, claims:
                    SetClaim(user), expires: accessTokenExpiration, notBefore: DateTime.Now, signingCredentials: signingCredentials);
                var newJwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                var newAccessToken = new AccessTokenDto
                {
                    Token = newJwtToken,
                    Expiration = accessTokenExpiration,
                    RefreshToken = CreateRefreshToken()
                };
                return newAccessToken;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        private IEnumerable<Claim> SetClaim(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            return claims;
        }
        private string CreateRefreshToken()
        {
            byte[] numbers = new Byte[32];
            using (var randomGenerator = RandomNumberGenerator.Create())
            {
                randomGenerator.GetBytes(numbers);
                return Convert.ToBase64String(numbers);
            }
        }
        public void RemoveRefreshToken(Users user)
        {
            user.RefreshToken = null;
        }
    }
}
