using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using API_Toeicking2021.Services.UserDBService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Toeicking2021.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserDBService _UserDBService;

        public UserController(IUserDBService UserDBService)
        {
            _UserDBService = UserDBService;
        }

        // 取得某位使用者資料(url：domain/User/GetUser)
        // GET查詢參數是純值型別可以不用加[FromQuery]
        [HttpGet("GetUser")]
        public IActionResult GetUser(string email)
        {
            var response = _UserDBService.GetUser(email);
            return Ok(response);
        }

        // 新增一位使用者資料(url：domain/User/Add)
        // POST時即便是複雜(自訂)型別，也可以不用加[FromBody]
        [HttpPost("Add")]
        public async Task<IActionResult> AddUser(AddUserDto newUser)
        {
            var response = await _UserDBService.AddUser(newUser);
            return Ok(response);
        }


        // 更新某位使用者資料(url：domain/User/Add)
        // POST時即便是複雜(自訂)型別，也可以不用加[FromBody]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUser)
        {
            var response = await _UserDBService.UpdateUser(updateUser);
            return Ok(response);
        }

        // 更新某位使用者資料(url：domain/User/AddWordList)
        // POST時即便是複雜(自訂)型別，也可以不用加[FromBody]
        [HttpPost("AddWordList")]
        public async Task<IActionResult> AddWordList(AddWordListParameter parameter)
        {
            var response = await _UserDBService.AddWordList(parameter);
            return Ok(response);
        }
        // 更新某位使用者資料(url：domain/User/AddWordList)
        // ***POST時即便參數是一個純值，也要用物件包起來，不然收不到***
        [HttpPost("IsEmailExist")]
        public async Task<IActionResult> IsEmailExist(CheckEmail parameter)
        {
            var response = await _UserDBService.IsEmailExist(parameter.Email);
            return Ok(response);
        }

    }
}
