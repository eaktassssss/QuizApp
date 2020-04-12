using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Results.Abstract
{
    public interface IResult
    {
        string Message { get; set; }
        bool Successeded { get; set; }
    }
}
