using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Quiz.Dto;

namespace Quiz.Web.Helper
{

    public static class QuizClientOptions
    {
        private static readonly IHttpContextAccessor _httpContextAccessor;
        static QuizClientOptions()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }
        public static void SetTokenCookie(AccessTokenDto accessToken)
        {
            SetOptionsCookieAppendToken("token", accessToken.Token,60);
        }
        public static HttpClient HttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55561/api/");
            var token = GetTokenCookie("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
        public static StringContent StringContent<T>(T entity) where T : class, new()
        {
            var serializeObject = JsonConvert.SerializeObject(entity);
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            return content;
        }
        private static void SetOptionsCookieAppendToken(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddYears(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMinutes(1);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }
        public static void RemoveTokenCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);

        }
        public static string GetTokenCookie(string key)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[key];
            return token;
        }
    }
}
