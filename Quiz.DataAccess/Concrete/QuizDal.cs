using System;
using System.Collections.Generic;
using System.Text;
using Quiz.DataAccess.Abstract;
using Quiz.Entities;
using Quiz.Entities.Context;
using Quiz.Repositories.Abstract;
using Quiz.Repositories.Concrete;

namespace Quiz.DataAccess.Concrete
{
    public class QuizDal:RepositoryBase<QuizContext,Quizs>,IQuizDal
    {
        public QuizDal(QuizContext context) : base(context)
        {
        }
    }
}
