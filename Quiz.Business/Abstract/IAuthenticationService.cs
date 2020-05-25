using Quiz.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quiz.Dto.Jwt;
using Quiz.Results.Abstract;

namespace Quiz.Business.Abstract
{
    public interface IAuthenticationService
    {
        Task<IDataResult<AccessTokenDto>> CreateAccessTokenAsync(CreateAccessTokenDto accessTokenDto);
        Task<IDataResult<AccessTokenDto>> CreateRefreshTokenAsync(CreateRefreshTokenDto refreshTokenDto);
        Task<IDataResult<UserDto>> RemoveRefreshTokenAsync(CreateRefreshTokenDto refreshTokenDto);
    }
}
