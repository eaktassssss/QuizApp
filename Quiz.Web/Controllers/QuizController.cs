using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quiz.Dto;
using Quiz.Results.Abstract;
using Quiz.Results.Concrete;
 
using Quiz.Web.Helper;

namespace Quiz.Web.Controllers
{
    [Authorize]
    public class QuizController :Controller
    {
        private readonly HttpClient _httpClient;
        public QuizController()
        {
            _httpClient = QuizClientOptions.HttpClient();
        }

        [HttpGet]
        public ActionResult AddNewQuiz()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddNewQuiz(QuizDto quizDto)
        {
            if (!ModelState.IsValid)
                return View(quizDto);
            else
            {
                var requestPath = "quiz/add-quiz";
                var content = QuizClientOptions.StringContent(quizDto);
                var request = _httpClient.PostAsync(requestPath, content);

                var response = request.Result.Content.ReadAsStringAsync();
                var entity = JsonConvert.DeserializeObject<DataResult<QuizDto>>(response.Result);
                if (entity.Data != null && entity.Successeded)
                {
                    return RedirectToAction("AddNewQuiz", "Quiz");
                }
                else
                {
                    return View(quizDto);
                }
            }
        }
        [HttpGet]
        public ActionResult GetQuizList()
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
        [HttpGet]
        public ActionResult EditQuiz(int id)
        {
            var requestPath = "quiz/get-quiz";
            var request = _httpClient.GetAsync($"{requestPath}/{id}");
            var conent = request.Result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<DataResult<QuizDto>>(conent.Result);
            return View(response.Data);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            var getQuizPath = "quiz/get-quiz";
            var getQuizRequest = _httpClient.GetAsync($"{getQuizPath}/{id}");
            var getQuizConent = await getQuizRequest.Result.Content.ReadAsStringAsync();
            var getQuizResult = JsonConvert.DeserializeObject<DataResult<QuizDto>>(getQuizConent);
            if (getQuizResult.Data != null && getQuizResult.Successeded)
            {
                var requestPath = "quiz/delete-quiz";
                var content = QuizClientOptions.StringContent(getQuizResult.Data);
                var request = await _httpClient.PostAsync($"{requestPath}", content);
                var result = await request.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<DataResult<QuizDto>>(result);
                if (response.Successeded && response.Data != null)
                {
                    return RedirectToAction("GetQuizList", "Quiz");
                }
                else
                {
                    return View("Error");
                }

            }
            else
            {
                return View("Error");
            }
        }
    }
}
