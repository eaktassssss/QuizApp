using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Quiz.Business.Abstract;
using Quiz.Business.Security.Microsoft.Jwt.Abstract;
using Quiz.DataAccess.Abstract;
using Quiz.Dto;
using Quiz.Entities;
using Quiz.Results.Abstract;
using Quiz.Results.Concrete;
using Quiz.UnitOfWork.Abstract;

namespace Quiz.Business.Concrete
{
    public class AuthenticationManager :IAuthenticationService
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IToken _token;
        public AuthenticationManager(IMapper mapper, IToken token, IUserService userService)
        {
            _mapper = mapper;
            _token = token;
            _userService = userService;
        }
        public async Task<IDataResult<AccessTokenDto>> CreateAccessTokenAsync(CreateAccessTokenDto accessTokenDto)
        {
            var response = new DataResult<AccessTokenDto>();
            try
            {
                var user = await _userService.GetByEmailPassword(accessTokenDto.Email, accessTokenDto.Password);
                if (user.Data != null && user.Successeded)
                {
                    var accessToken = _token.CreateAccessToken(_mapper.Map<Users>(user.Data));
                    var result =await _userService.SaveRefreshToken(user.Data.Id, accessToken.RefreshToken);
                    if (result.Successeded && result.Data !=null)
                    {
                        response.Message = result.Message;
                        response.Successeded = result.Successeded;
                        response.Data = accessToken;
                    }
                    else
                    {
                        response.Message = result.Message;
                        response.Successeded =false;
                        response.Data = null;
                    }

                }
                else
                {
                    response.Message = user.Message;
                    response.Successeded = false;
                    response.Data = null;
                }
            }
            catch (Exception exception)
            {
                response.Successeded = false;
                response.Data = null;
                response.Message = exception.Message;
            }
            return response;
        }
        public async Task<IDataResult<AccessTokenDto>> CreateRefreshTokenAsync(CreateRefreshTokenDto refreshTokenDto)
        {
            var response = new DataResult<AccessTokenDto>();
            try
            {
                var user = await _userService.GetByRefreshToken(refreshTokenDto.RefreshToken);
                if (user.Data != null && user.Successeded)
                {
                    if (user.Data.RefreshTokenEndDate > DateTime.Now)
                    {
                        var newToken = _token.CreateAccessToken(_mapper.Map<Users>(user.Data));
                        var result =await _userService.SaveRefreshToken(user.Data.Id, newToken.RefreshToken);
                        response.Successeded =result.Successeded;
                        response.Data = newToken;
                        response.Message = "Token üretimi başarılı";

                    }
                    else
                    {
                        response.Successeded = false;
                        response.Data = null;
                        response.Message = "Refresh Token Süresi Dolmuş";
                    }
                }
                else
                {
                    response.Successeded = false;
                    response.Data = null;
                    response.Message = "Refresh Token'a ait kullanıcı bulunamadı";
                }
            }
            catch (Exception exception)
            {
                response.Successeded = false;
                response.Data = null;
                response.Message = exception.Message;
            }
            return response;
        }
        public async Task<IDataResult<UserDto>> RemoveRefreshTokenAsync(CreateRefreshTokenDto refreshTokenDto)
        {
            var response = new DataResult<UserDto>();
            try
            {
                var user = await _userService.GetByRefreshToken(refreshTokenDto.RefreshToken);
                if (user.Data != null && user.Successeded)
                {
                   
                    var result = await _userService.RemoveRefreshToken(user.Data.RefreshToken);
                    if (result.Successeded)
                    {
                        response.Successeded = true;
                        response.Data = result.Data;
                    }
                    else
                    {
                        response.Successeded = false;
                        response.Data = null;
                    }
                }
                else
                {
                    response.Successeded = false;
                    response.Data = null;
                    response.Message = "Refresh Token bulunamadı";
                }
            }
            catch (Exception exception)
            {
                response.Successeded = false;
                response.Data = null;
                response.Message = exception.Message;
            }
            return response;
        }
    }
}
