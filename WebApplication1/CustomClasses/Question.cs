using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.CustomClasses
{
    public class Questions
    {
        public string type { get; set; }
        public string question { get; set; }
        public static Questions FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Questions questions = new Questions();
            questions.type = values[0];
            questions.question = values[1];
            return questions;
        }

        public static Questions FromCsv(string csvLine, string filter)
        {
            string[] values = csvLine.Split(',');
            Questions questions = new Questions();
            if (values[0] == filter)
            {
                questions.type = values[0];
                questions.question = values[1];
                return questions;
            }
            else
            {

                return null;
            }
        }
    }
}
