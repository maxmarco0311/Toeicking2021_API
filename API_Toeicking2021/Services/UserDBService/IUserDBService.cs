using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Services.UserDBService
{
    public interface IUserDBService
    {
        ServiceResponse<GetUserDto> GetUser(string email);
        Task<ServiceResponse<User>> AddUser(AddUserDto newUser);
        Task<ServiceResponse<User>> UpdateUser(UpdateUserDto updateUser);
        Task<ServiceResponse<User>> AddWordList(AddWordListParameter parameter);
    }
}
