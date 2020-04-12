using System;
using System.Collections.Generic;
using System.Text;
using Quiz.Results.Abstract;

namespace Quiz.Results.Concrete
{
    public class Result:IResult
    {
        public string Message { get; set; }
        public bool Successeded { get; set; }
    }
}
