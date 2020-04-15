using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Quiz.Business.Abstract;
using Quiz.DataAccess.Abstract;
using Quiz.Dto;
using Quiz.Dto.JwtToken;
using Quiz.Entities;
using Quiz.Results.Abstract;
using Quiz.Results.Concrete;
using Quiz.UnitOfWork.Abstract;

namespace Quiz.Business.Concrete
{
    public class UserManager :IUserService
    {
        private readonly IOptions<JwtTokenDto> _tokenOptions;
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserManager(IUserDal userDal, IMapper mapper, IUnitOfWork unitOfWork, IOptions<JwtTokenDto> tokenOptions)
        {
            _userDal = userDal;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenOptions = tokenOptions;
        }
        public async Task<IDataResult<UserDto>> AddAsync(UserDto entity)
        {
            var response = new DataResult<UserDto>();
            try
            {
                var result = await _userDal.AddAsync(_mapper.Map<Users>(entity));
                await _unitOfWork.CompletedAsync();
                var claims =  
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
        public async Task<IDataResult<UserDto>> GetByEmailPassword(string email, string password)
        {
            var response = new DataResult<UserDto>();
            try
            {
                if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
                {
                    var user = await _userDal.GetAsync(filter =>
                        filter.Email.ToLower().Trim() == email.ToLower().Trim() &&
                        filter.Password.Trim() == password.Trim());
                    if (user != null)
                    {
                        response.Message = "Successful transaction";
                        response.Successeded = true;
                        response.Data = _mapper.Map<UserDto>(user);
                    }
                    else
                    {
                        response.Message = "No Content";
                        response.Successeded = false;
                        response.Data = null;
                        response.StatusCode = 204;
                    }
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
        public async Task<IDataResult<UserDto>> GetByRefreshToken(int userId, string refreshToken)
        {
            var response = new DataResult<UserDto>();
            try
            {
                if (!String.IsNullOrEmpty(refreshToken) && userId > 0)
                {
                    var user = await _userDal.GetAsync(filter =>
                        filter.RefreshToken.Trim() == refreshToken.Trim() && filter.Id == userId);
                    if (user != null)
                    {
                        response.Message = "Successful transaction";
                        response.Successeded = true;
                        response.Data = _mapper.Map<UserDto>(user);
                    }
                    else
                    {
                        response.Message = "No Content";
                        response.Successeded = false;
                        response.Data = null;
                        response.StatusCode = 204;
                    }
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
        public async Task<IDataResult<UserDto>> SaveRefreshToken(int userId, string refreshToken)
        {
            var response = new DataResult<UserDto>();
            try
            {
                if (!String.IsNullOrEmpty(refreshToken) && userId > 0)
                {
                    var user = await _userDal.GetAsync(filter => filter.Id == userId);
                    if (user != null)
                    {
                        user.Data.RefreshToken = refreshToken;
                        user.Data.RefreshTokenEndDate =
                            DateTime.Now.AddMinutes(_tokenOptions.Value.RefreshTokenExpiration);
                        var returnData = _userDal.UpdateAsync(user.Data);
                        await _unitOfWork.CompletedAsync();
                        response.Message = "Successful transaction";
                        response.Successeded = true;
                        response.Data = _mapper.Map<UserDto>(returnData);
                    }
                    else
                    {
                        response.Message = "Registration Update Failed";
                        response.Successeded = false;
                        response.Data = null;
                        response.StatusCode = 400;
                    }
                }
                else
                {
                    response.Message = "User Not Found";
                    response.Successeded = false;
                    response.Data = null;
                    response.StatusCode = 404;
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
        public async Task<IDataResult<UserDto>> RemoveRefreshToken(UserDto userDto)
        {
            var response = new DataResult<UserDto>();
            try
            {
                var result = await _userDal.DeleteAsync(_mapper.Map<Users>(userDto));
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
                    response.Message = "Token Failed To Delete";
                    response.Successeded = false;
                    response.Data = null;
                    response.StatusCode = 400;
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
