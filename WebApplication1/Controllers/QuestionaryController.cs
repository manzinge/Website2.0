using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.CustomClasses;

namespace WebApplication1.Controllers
{
    public class QuestionaryController : Controller
    {
        private readonly ILogger<QuestionaryController> _logger;

        public QuestionaryController(ILogger<QuestionaryController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<string[]> GetQuestions(int choice)
        {
            string topic = "none";
            switch(choice)
            {
                case 1: topic = "topic1";break;
                case 2: topic = "topic2";break;
                case 3: topic = "topic3";break;
                case 4: topic = "topic4";break;
                case 5: topic = "topic5";break;
                case 6: topic = "topic6";break;
                default: break;
            }
            string questionDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Resources\"));
            string targetFile = questionDirectory + "questions.json";
            var jsonContent = System.IO.File.ReadAllText(targetFile);
            try
            {
                Question questions = JsonConvert.DeserializeObject<Question>(jsonContent);
                //object[] questionsToAsk = questions.GetType().GetProperty(topic).GetValue(questions, null);
                //return Array.ConvertAll<object, string>(questionsToAsk, ConvertObjectToString);
                return new string[0];
            }
            catch (Exception ex)
            {
                return new string[0];
            }
        }
        string ConvertObjectToString(object obj)
        {
            return obj?.ToString() ?? string.Empty;
        }
    }
}
