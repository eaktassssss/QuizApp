using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Quiz.Business.Abstract;
using Quiz.DataAccess.Abstract;
using Quiz.Dto;
using Quiz.Entities;
using Quiz.Entities.Context;
using Quiz.Results.Abstract;
using Quiz.Results.Concrete;
using Quiz.UnitOfWork.Abstract;
using Exception = System.Exception;

namespace Quiz.Business.Concrete
{
    public class QuizManager : IQuizService
    {
        private readonly IQuizDal _quizDal;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public QuizManager(IQuizDal quizDal, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _quizDal = quizDal;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<QuizDto>> AddAsync(QuizDto entity)
        {
            var response = new DataResult<QuizDto>();
            try
            {
                var result = await _quizDal.AddAsync(_mapper.Map<Quizs>(entity));
                await _unitOfWork.CompletedAsync();
                if (result.Successeded && result.Data != null)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<QuizDto>(result.Data);
                    response.Successeded = result.Successeded;
                    response.StatusCode = 200;

                }
                else
                {
                    response.Message = "No Content";
                    response.Successeded = false;
                    response.Data = null;
                    response.StatusCode = 204;
                }
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Successeded = false;
                response.Data = null;
                response.StatusCode = 400;
            }
            return response;
        }

        public async Task<IDataResult<QuizDto>> DeleteAsync(QuizDto entity)
        {
            var response = new DataResult<QuizDto>();
            try
            {
                var result = await _quizDal.DeleteAsync(_mapper.Map<Quizs>(entity));
                await _unitOfWork.CompletedAsync();
                if (result.Successeded && result.Data != null)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<QuizDto>(result.Data);
                    response.Successeded = result.Successeded;
                    response.StatusCode = 200;

                }
                else
                {
                    response.Message = "No Content";
                    response.Successeded = false;
                    response.Data = null;
                    response.StatusCode = 204;
                }
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Successeded = false;
                response.Data = null;
                response.StatusCode = 400;
            }
            return response;
        }


        public async Task<IDataResult<List<QuizDto>>> GetListAsync()
        {
            var response = new DataResult<List<QuizDto>>();
            try
            {
                var quizs = await _quizDal.GetListAsync();
                var result = _mapper.Map<List<QuizDto>>(quizs.Data);
                if (result.Any() && quizs.Successeded)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<List<QuizDto>>(quizs.Data);
                    response.Successeded = quizs.Successeded;
                    response.StatusCode = 200;
                }
                else
                {
                    response.Message = "No Content";
                    response.Data = null;
                    response.Successeded = false;
                    response.StatusCode = 204;
                }
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Successeded = false;
                response.Data = null;
                response.StatusCode = 400;
            }
            return response;
        }
        public async Task<IDataResult<QuizDto>> UpdateAsync(QuizDto entity)
        {
            var response = new DataResult<QuizDto>();
            try
            {
                var result = await _quizDal.UpdateAsync(_mapper.Map<Quizs>(entity));
                await _unitOfWork.CompletedAsync();

                if (result.Successeded && result.Data != null)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<QuizDto>(result.Data);
                    response.Successeded = result.Successeded;
                    response.StatusCode = 200;
                }
                else
                {
                    response.Message = "No Content";
                    response.Successeded = false;
                    response.Data = null;
                    response.StatusCode = 204;
                }
            }
            catch (Exception exception)
            {
                response.Message = exception.Message;
                response.Successeded = false;
                response.Data = null;
                response.StatusCode = 400;
            }
            return response;
        }
    }
}
