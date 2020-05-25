using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quiz.Dto;
using Quiz.Results.Concrete;
using Quiz.Web.Helper;
using Quiz.Web.Models;

namespace Quiz.Web.Controllers
{
    public class HomeController :Controller
    {
        private readonly HttpClient _httpClient;
        public HomeController()
        {
            _httpClient = QuizClientOptions.HttpClient();
        }
        [HttpGet]
        public ActionResult Index() => View();
        [HttpGet]
        public ActionResult QuizList()
        {
            try
            {
                var requestPath = "quiz/get-all";
                var request = _httpClient.GetAsync(requestPath).Result;
                var response = request.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DataResult<List<QuizDto>>>(response.Result);
                return View(data.Data);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }
    }
}
