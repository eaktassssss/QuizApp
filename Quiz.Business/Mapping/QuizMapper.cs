using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Quiz.Dto;
using Quiz.Entities;

namespace Quiz.Business.Mapping
{
    public class QuizMapper : Profile
    {
        public QuizMapper()
        {
            CreateMap<QuizDto, Quizs>();
            CreateMap<Quizs, QuizDto>();
        }
    }
}
