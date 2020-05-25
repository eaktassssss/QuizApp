using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quiz.Dto;
using Quiz.Dto.Account;
using Quiz.Entities;
using Quiz.Results.Concrete;
using Quiz.Web.Helper;

namespace Quiz.Web.Controllers
{

    public class AccountController :Controller
    {

        private HttpClient _httpClient;
        public AccountController()
        {

            _httpClient = QuizClientOptions.HttpClient();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var requestPath = "user/add-user";
                var content = QuizClientOptions.StringContent(registerDto);
                var request = _httpClient.PostAsync(requestPath, content).Result;
                var response = await request.Content.ReadAsStringAsync();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
        }
        [HttpGet]
        public ActionResult Login() => this.View();

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var requestPath = "account/access-token";
                var content = QuizClientOptions.StringContent(loginDto);
                var request = _httpClient.PostAsync(requestPath, content).Result;
                var response = await request.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<DataResult<AccessTokenDto>>(response);
                if (token.Successeded && token.Data.Token != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,loginDto.Email),
                    };
                    var identity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                    QuizClientOptions.SetTokenCookie(token.Data);
                    return RedirectToAction("Index", "Home");
                }
                return View(loginDto);

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        public async Task<ActionResult> LogOut()
        {
            try
            {
                QuizClientOptions.RemoveTokenCookie("token");
                await HttpContext.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
