using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Quiz.Business.Abstract;
using Quiz.Business.Security.Microsoft.Jwt.Abstract;
using Quiz.Business.Security.Microsoft.Jwt.Concrete;
using Quiz.Dto;
using Quiz.Entities;
using Quiz.Results.Abstract;
using Quiz.Results.Concrete;

namespace Quiz.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult> GetListAsync()
        {
            var result = await _quizService.GetListAsync();
            if (!result.Successeded && !result.Data.Any())
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("add-quiz")]
        public async Task<ActionResult> AddAsync(QuizDto quizDto)
        {
            var result = await _quizService.AddAsync(quizDto);
            if (!result.Successeded && result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("update-quiz")]
        public async Task<ActionResult> UpdateAsync(QuizDto quizDto)
        {
            var result = await _quizService.UpdateAsync(quizDto);
            if (!result.Successeded && result.Data==null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}