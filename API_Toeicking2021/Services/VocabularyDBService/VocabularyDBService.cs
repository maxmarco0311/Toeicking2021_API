using API_Toeicking2021.Data;
using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Services.VocabularyDBService
{
    public class VocabularyDBService : IVocabularyDBService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public VocabularyDBService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        #region 取得我的字彙列表(分頁)
        public async Task<ServiceResponse<List<VocabularyDto>>> GetVocabularies(GetWordListParameter parameter)
        {
            ServiceResponse<List<VocabularyDto>> serviceResponse = new ServiceResponse<List<VocabularyDto>>();
            // 宣告要送出的那頁字彙列表(List<VocabularyDto>)
            List<VocabularyDto> myWordList = new List<VocabularyDto>();
            // 每頁5筆(正式上線改20筆)
            int pageSize = 5;
            try
            {
                // 從DB中獲得WordList
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == parameter.Email);
                if (user != null)
                {
                    // 字串轉成List<int>
                    List<int> wordList = user.WordList.Split(',').Select(int.Parse).ToList();
                    // 使用LINQ取得每頁的List<int>
                    List<int> pageWordList = wordList.Skip((parameter.PageToLoad - 1) * pageSize).Take(pageSize).ToList();
                    // 將每頁的vocabularyId跑迴圈查出每筆Vocabulary物件，並傳成VocabularyDto物件
                    foreach (var item in pageWordList)
                    {
                        // FindAsync()的參數為int
                        Vocabulary myWord = await _context.Vocabularies.FindAsync(item);
                        myWordList.Add(_mapper.Map<VocabularyDto>(myWord));
                    }
                    serviceResponse.Data = myWordList;
                }
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        #endregion

    }
}
