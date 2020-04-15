using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quiz.Entities;
using Quiz.Repositories.Abstract;

namespace Quiz.DataAccess.Abstract
{
    public interface IUserDal:IRepository<Users>
    {
    }
}
