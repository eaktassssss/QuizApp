using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quiz.Dto;
using Quiz.Entities;
using Quiz.Results.Abstract;

namespace Quiz.Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<UserDto>>> GetListAsync();
        Task<IDataResult<UserDto>> AddAsync(UserDto entity);
        Task<IDataResult<UserDto>> UpdateAsync(UserDto entity);
        Task<IDataResult<UserDto>> DeleteAsync(UserDto entity);
        Task<IDataResult<UserDto>> GetAsync(int id);
        Task<IDataResult<UserDto>> GetByEmailPassword(string email, string password);
        Task<IDataResult<UserDto>> GetByRefreshToken(string refreshToken);
        Task<IDataResult<UserDto>> SaveRefreshToken(int userId, string refreshToken);
        Task<IDataResult<UserDto>> RemoveRefreshToken(string refreshToken);

    }
}
