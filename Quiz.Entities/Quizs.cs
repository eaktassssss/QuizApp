using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Entities
{
    public class Quizs
    {
        public int Id { get; set; }
        public string QuizName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
