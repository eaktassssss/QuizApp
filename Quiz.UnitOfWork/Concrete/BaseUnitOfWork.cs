using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quiz.Entities.Context;
using Quiz.UnitOfWork.Abstract;

namespace Quiz.UnitOfWork.Concrete
{
    public class BaseUnitOfWork :IUnitOfWork
    {
        private readonly QuizContext _quizContext;
        public BaseUnitOfWork(QuizContext quizContext)
        {
            _quizContext = quizContext;
        }
        public async Task CompletedAsync()
        {
            await _quizContext.SaveChangesAsync();

        }
    }
}
