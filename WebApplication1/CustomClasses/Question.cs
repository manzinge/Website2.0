using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.CustomClasses
{
    public class Questions
    {
        public int Id { get; set; }
        public string Frage { get; set; }
        public string Kategorie { get; set; }
        public static Questions FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            Questions questions = new Questions();
            questions.Id = Int32.Parse(values[0]);
            questions.Frage = values[1];
            questions.Kategorie = values[2];
            return questions;
        }

        public static Questions FromCsv(string csvLine, string filter, bool all)
        {
            string[] values = csvLine.Split(';');
            Questions questions = new Questions();
            if(all)
            {
                if (values[2] == filter || values[2] == "ALL")
                {
                    questions.Id = Int32.Parse(values[0]);
                    questions.Frage = values[1];
                    questions.Kategorie = values[2];
                    return questions;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (values[2] == filter)
                {
                    questions.Id = Int32.Parse(values[0]);
                    questions.Frage = values[1];
                    questions.Kategorie = values[2];
                    return questions;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
