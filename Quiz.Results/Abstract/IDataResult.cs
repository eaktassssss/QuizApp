using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Results.Abstract
{
    public interface IDataResult<T> where T : class
    {
        T Data { get; set; }
        string Message { get; set; }
        bool Successeded { get; set; }
    }
}
