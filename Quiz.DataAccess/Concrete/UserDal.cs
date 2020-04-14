using Quiz.Entities.Context;
using Quiz.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Quiz.DataAccess.Abstract;
using Quiz.Entities;

namespace Quiz.DataAccess.Concrete
{
    public class UserDal :RepositoryBase<QuizContext, Users>, IUserDal
    {
        private readonly QuizContext _quizContext;
        public UserDal(QuizContext quizContext):base(quizContext)
        {
            _quizContext = quizContext;
        }
    }
}
