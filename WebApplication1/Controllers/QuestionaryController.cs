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
        public async Task<List<Questions>> GetQuestions(string choice)
        {
            string questionDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Resources\"));
            string targetFile = questionDirectory + "questions.csv";
            try
            {
                List<Questions> questions = System.IO.File.ReadAllLines(targetFile)
                   .Skip(1)
                   .Select(v => Questions.FromCsv(v, choice))
                   .ToList();
                questions.RemoveAll(item => item == null);
                return questions;
            }
            catch
            {
                return new List<Questions>();
            }
        }
    }
}
