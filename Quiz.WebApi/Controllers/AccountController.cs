using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Business.Abstract;
using Quiz.Dto;

namespace Quiz.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController :ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("remove-token")]
        public async Task<ActionResult> RemoveRefreshToken(CreateRefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RemoveRefreshTokenAsync(refreshTokenDto);
            if (result.Successeded && result.Data != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("access-token")]
        public async Task<ActionResult> AccessToken(CreateAccessTokenDto accessTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(accessTokenDto);
            }
            else
            {
                var accessToken = await _authenticationService.CreateAccessTokenAsync(accessTokenDto);
                if (accessToken.Successeded && accessToken.Data.Token != null)
                {
                    return Ok(accessToken);
                }
                return BadRequest(accessToken);
            }
        }
        public async Task<ActionResult> RefreshToken(CreateRefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateRefreshTokenAsync(refreshTokenDto);
            if (result.Successeded && result.Data.RefreshToken != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}