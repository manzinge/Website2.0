using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.CustomClasses;

namespace WebApplication1.Controllers
{
    public class QuestionaryController : Controller
    {
        private readonly ILogger<QuestionaryController> _logger;
        private readonly IOptions<Configuration> config;


        public QuestionaryController(ILogger<QuestionaryController> logger, IOptions<Configuration> config)
        {
            this.config = config;
            _logger = logger;
        }

        [HttpPost]
        public async Task<List<Questions>> GetQuestions(string choice, bool all)
        {
            string questionDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Resources\"));
            string targetFile = questionDirectory + "q1.0.csv";
            try
            {
                List<Questions> questions = System.IO.File.ReadAllLines(targetFile)
                   .Skip(1)
                   .Select(v => Questions.FromCsv(v, choice, all))
                   .ToList();
                questions.RemoveAll(item => item == null);
                return questions;
            }
            catch
            {
                return new List<Questions>();
            }
        }
        [HttpPost]
        public async Task<IActionResult> SendAnswersToDB(string ip, List<Answer> answers)
        {
            using (SqlConnection conn = new SqlConnection(config.Value.dbConString))
            {
                try
                {
                    baanswers ent = new baanswers();
                    ent.timestamp = DateTime.Now;
                    ent.ip = ip;
                    foreach (Answer answ in answers)
                    {
                        ent.GetType().GetProperty(answ.question).SetValue(ent, answ.answer);
                    }
                    conn.Open();
                    conn.Insert(ent);
                    conn.Close();
                }
                catch(Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return Ok();
        }
    }
}
