using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quiz.Dto
{
    public class QuizDto
    {
        public int Id { get; set; }

        [Display(Name = "Quiz Name")]
        [Required(ErrorMessage = "{0} is reuired")]
        public string QuizName { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is reuired")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
