using Quiz.Dto;
using Quiz.Entities;
using Quiz.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Business.Abstract
{
    public interface IQuizService
    {
        Task<IDataResult<List<QuizDto>>> GetListAsync();
        Task<IDataResult<QuizDto>> AddAsync(QuizDto entity);
        Task<IDataResult<QuizDto>> UpdateAsync(QuizDto entity);
        Task<IDataResult<QuizDto>> DeleteAsync(QuizDto entity);
        Task<IDataResult<QuizDto>> GetAsync(int id);
    }
}
