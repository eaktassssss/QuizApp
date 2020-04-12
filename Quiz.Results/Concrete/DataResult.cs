using System;
using System.Collections.Generic;
using System.Text;
using Quiz.Results.Abstract;

namespace Quiz.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Successeded { get; set; }
        public int StatusCode { get; set; }
    }
}
