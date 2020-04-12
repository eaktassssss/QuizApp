using Quiz.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Quiz.Entities;

namespace Quiz.DataAccess.Abstract
{
    public interface IQuizDal:IRepository<Quizs>
    {
    }
}
