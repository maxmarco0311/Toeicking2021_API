﻿using API_Toeicking2021.Data;
using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using API_Toeicking2021.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        #region 獲得User資料
        public ServiceResponse<GetUserDto> GetUser(string email)
        {
            ServiceResponse<GetUserDto> serviceResponse = new ServiceResponse<GetUserDto>();
            // 用email查出該位在DB中的user
            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                // 是轉成GetUserDto型別的User，WordList型別是List<int>
                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "查無此使用者";
            }
            return serviceResponse;
        }
        #endregion

        #region 新增User
        public async Task<ServiceResponse<User>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            // 轉成User物件，準備要存DB
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

        #region 更新User的Valid或Rating
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

        #region 加入WordList(按下字彙旁邊的愛心)
        public async Task<ServiceResponse<User>> AddWordList(WordListParameter parameter)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            User user = _context.Users.FirstOrDefault(u => u.Email == parameter.Email);
            if (user != null)
            {
                // 判斷user.WordList是否"不"為空
                // 不為空代表之前有加入字彙過
                if (!string.IsNullOrEmpty(user.WordList))
                {                    
                    // 將欄位值字串(user.WordList)轉成List<string>
                    List<string> myWordList = WordListOrderHelper.ConvertToListFromString(user.WordList);
                    // 再檢查是否已加過該字彙
                    if (!myWordList.Contains(parameter.VocabularyId))
                    {
                        // 第二次以上add：把","放在前面，取出時就不用TrimEnd()
                        user.WordList += "," + parameter.VocabularyId;
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "此字彙已加入過";
                    }          
                }
                // 為空代表還沒加過字彙
                else
                {
                    // 第一次add：直接加入VocabularyId
                    user.WordList = parameter.VocabularyId;
                }
                // 存新值進DB，並將該字彙置於WordList第一個
                user.WordList = WordListOrderHelper.MoveToTop(user.WordList, parameter.VocabularyId);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                // 再取出更新後的User資料回傳
                serviceResponse.Data = _context.Users.FirstOrDefault(u => u.Email == parameter.Email);

            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "查無此使用者";
            }
            return serviceResponse;

        }
        #endregion

        #region 刪除WordList裡的字彙(按下字彙旁邊的愛心)
        public async Task<ServiceResponse<User>> DeleteWordList(WordListParameter parameter)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            User user = _context.Users.FirstOrDefault(u => u.Email == parameter.Email);
            if (user!=null)
            {
                // 判斷user.WordList是否"不"為空
                // 不為空代表之前有加入字彙過
                if (!string.IsNullOrEmpty(user.WordList))
                {
                    // 將欄位值字串(user.WordList)轉成List<string>
                    List<string> myWordList = WordListOrderHelper.ConvertToListFromString(user.WordList);
                    // 檢查清單中是否有該字彙
                    if (myWordList.Contains(parameter.VocabularyId))
                    {
                        // 從集合中刪除該字彙編號
                        myWordList.Remove(parameter.VocabularyId);
                        // 將集合轉為字串(,分隔)
                        user.WordList = WordListOrderHelper.ConvertToStringFromList(myWordList);
                        // 存入DB
                        _context.Users.Update(user);
                        await _context.SaveChangesAsync();
                        // 再取出更新後的User資料回傳
                        serviceResponse.Data = _context.Users.FirstOrDefault(u => u.Email == parameter.Email);
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "此字彙不在字彙清單內";
                    }

                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "字彙清單是空的";
                }
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "查無此使用者";
            }
            return serviceResponse;
        }
        #endregion

        #region 檢查使用者權限是否為valid(所有API執行前的第一步檢查)
        public async Task<bool> IsValid(string email)
        {
            // 在Select()後要呼叫FirstOrDefaultAsync()回傳純值或單一物件，呼叫ToList()則回傳集合
            return await _context.Users.Where(u => u.Email == email).Select(u => u.Valid).FirstOrDefaultAsync();
        }
        #endregion

        #region 檢查eamil是否存在(APP忘記密碼)
        public async Task<bool> IsEmailExist(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null ? true : false;
        }
        #endregion

       


    }
}
