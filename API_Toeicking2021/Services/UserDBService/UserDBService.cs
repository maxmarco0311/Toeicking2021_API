using API_Toeicking2021.Data;
using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Services.UserDBService
{
    public class UserDBService : IUserDBService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserDBService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public ServiceResponse<GetUserDto> GetUser(string email)
        {
            ServiceResponse<GetUserDto> serviceResponse = new ServiceResponse<GetUserDto>();
            // 用email查出該位在DB中的user
            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "查無此使用者";
            }
            return serviceResponse;
        }

        #region 新增User
        public async Task<ServiceResponse<User>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            User user = _mapper.Map<User>(newUser);
            // 先存起email，待會可從DB中查到這筆新增的User
            string email = user.Email;
            try
            {
                // 存進DB
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                // 撈出剛剛存進DB的User當作回覆資料
                User DBUser = _context.Users.FirstOrDefault(u => u.Email == email);
                serviceResponse.Data = DBUser;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        #endregion

        #region 更新User
        public async Task<ServiceResponse<User>> UpdateUser(UpdateUserDto updateUser)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            // 用email查出該位在DB中的user
            User user = _context.Users.FirstOrDefault(u => u.Email == updateUser.Email);
            // 檢查是否有該位user
            if (user != null)
            {
                // 將傳來的資料更新在DB中的user
                user.Valid = updateUser.Valid;
                user.Rating = updateUser.Rating;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                // 將更新後的user回傳
                serviceResponse.Data = user;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "查無此使用者";
            }
            return serviceResponse;
        }
        #endregion

    }
}
