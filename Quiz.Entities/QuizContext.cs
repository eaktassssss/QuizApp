using Microsoft.EntityFrameworkCore;
using Quiz.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Entities.Context
{
    public class QuizContext:DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> contextOptions):base(contextOptions)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Quizs> Quizs { get; set; }
    }
}
