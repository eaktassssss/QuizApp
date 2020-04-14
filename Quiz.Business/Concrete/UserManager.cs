using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Quiz.Business.Abstract;
using Quiz.DataAccess.Abstract;
using Quiz.Dto;
using Quiz.Entities;
using Quiz.Results.Abstract;
using Quiz.Results.Concrete;
using Quiz.UnitOfWork.Abstract;

namespace Quiz.Business.Concrete
{
    public class UserManager:IUserService
    {
        private readonly IUserDal _userDal;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public UserManager(IUserDal userDal, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userDal = userDal;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<UserDto>> AddAsync(UserDto entity)
        {
            var response = new DataResult<UserDto>();
            try
            {
                var result = await _userDal.AddAsync(_mapper.Map<Users>(entity));
                await _unitOfWork.CompletedAsync();
                if (result.Successeded && result.Data != null)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<UserDto>(result.Data);
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
        public async Task<IDataResult<UserDto>> DeleteAsync(UserDto entity)
        {
            var response = new DataResult<UserDto>();
            try
            {
                var result = await _userDal.DeleteAsync(_mapper.Map<Users>(entity));
                await _unitOfWork.CompletedAsync();
                if (result.Successeded && result.Data != null)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<UserDto>(result.Data);
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
        public async Task<IDataResult<UserDto>> GetAsync(int id)
        {
            var response = new DataResult<UserDto>();
            try
            {
                var result = await _userDal.GetAsync(x => x.Id == id);
                if (result.Successeded && result.Data != null)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<UserDto>(result.Data);
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
        public async Task<IDataResult<List<UserDto>>> GetListAsync()
        {
            var response = new DataResult<List<UserDto>>();
            try
            {
                var users = await _userDal.GetListAsync();
                var result = _mapper.Map<List<UserDto>>(users.Data);
                if (result.Any() && users.Successeded)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<List<UserDto>>(users.Data);
                    response.Successeded = users.Successeded;
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
        public async Task<IDataResult<UserDto>> UpdateAsync(UserDto entity)
        {
            var response = new DataResult<UserDto>();
            try
            {
                var result = await _userDal.UpdateAsync(_mapper.Map<Users>(entity));
                await _unitOfWork.CompletedAsync();

                if (result.Successeded && result.Data != null)
                {
                    response.Message = "Successful transaction";
                    response.Data = _mapper.Map<UserDto>(result.Data);
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
