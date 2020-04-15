using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Business.Abstract;
using Quiz.Dto;
using Quiz.Results.Concrete;
using Quiz.WebApi.Helpers.Claims;

namespace Quiz.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController :ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenClaimsHelper _tokenClaimsHelper;
        public UserController(IUserService userService, TokenClaimsHelper tokenClaimsHelper)
        {
            _userService = userService;
            _tokenClaimsHelper = new TokenClaimsHelper();
        }

        [HttpPost("add-user")]
        public async Task<ActionResult> AddAsync(UserDto userDto)
        {
            var result = await _userService.AddAsync(userDto);
            if (!result.Successeded && result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetListAsync()
        {
            var result = await _userService.GetListAsync();
            if (!result.Successeded && !result.Data.Any())
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("edit-user")]
        public async Task<ActionResult> UpdateAsync(UserDto userDto)
        {
            var result = await _userService.UpdateAsync(userDto);
            var dedde = User.Claims;
            if (!result.Successeded && result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("get-user")]
        public async Task<ActionResult> GetAsync()
        {
            var userId = _tokenClaimsHelper.GetByRequestUserId();
            var result = await _userService.GetAsync(userId);
            if (!result.Successeded && result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("delete-quiz")]
        public async Task<ActionResult> DeleteAsync(UserDto userDto)
        {
            var result = await _userService.DeleteAsync(userDto);
            if (!result.Successeded && result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}