using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quiz.UnitOfWork.Abstract
{
    public interface IUnitOfWork 
    {
        Task CompletedAsync();
    }
}
