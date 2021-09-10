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

        #region 取得字彙列表第一頁(含總頁數)
        public async Task<ServiceResponse<FirstPageVocabularyDto>> GetFirstPageVocabularies(GetWordListParameter parameter)
        {
            ServiceResponse<FirstPageVocabularyDto> serviceResponse = new ServiceResponse<FirstPageVocabularyDto>();
            // 宣告要送出的那頁字彙列表(List<VocabularyDto>)
            List<VocabularyDto> myWordList = new List<VocabularyDto>();
            // ****若serviceResponse.Data是複雜物件，要記得先宣告一個空的，然後再去裝資料***
            FirstPageVocabularyDto data = new FirstPageVocabularyDto();
            // 每頁5筆(正式上線改20筆)
            //int pageSize = 5;
            try
            {
                // 從DB中獲得WordList
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == parameter.Email);
                if (user != null)
                {
                    // 字串轉成List<int>
                    List<int> wordList = user.WordList.Split(',').Select(int.Parse).ToList();
                    // 使用LINQ取得每頁的List<int>
                    // 算出總頁數(到時還要加條件式)
                    data.TotalPages = (int)Math.Ceiling(wordList.Count / (double)Convert.ToInt16(parameter.PageSize));
                    // 每頁撈幾筆(parameter.PageSize)由Flutter端利用API參數決定(到時還要加條件式)
                    List<int> pageWordList = wordList.Skip((Convert.ToInt16(parameter.PageToLoad) - 1) * Convert.ToInt16(parameter.PageSize)).Take(Convert.ToInt16(parameter.PageSize)).ToList();
                    // 將每頁的vocabularyId跑迴圈查出每筆Vocabulary物件，並傳成VocabularyDto物件
                    foreach (var item in pageWordList)
                    {
                        // FindAsync()的參數為int
                        Vocabulary myWord = await _context.Vocabularies.FindAsync(item);
                        myWordList.Add(_mapper.Map<VocabularyDto>(myWord));
                    }
                    data.Vocabularies = myWordList;
                    serviceResponse.Data = data;
                    
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

        #region 取得我的字彙列表(第二頁以上，不含總頁數)
        public async Task<ServiceResponse<List<VocabularyDto>>> GetVocabularies(GetWordListParameter parameter)
        {
            ServiceResponse<List<VocabularyDto>> serviceResponse = new ServiceResponse<List<VocabularyDto>>();
            // 宣告要送出的那頁字彙列表(List<VocabularyDto>)
            List<VocabularyDto> myWordList = new List<VocabularyDto>();
            // 每頁5筆(正式上線改20筆)
            //int pageSize = 5;
            try
            {
                // 從DB中獲得WordList
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == parameter.Email);
                if (user != null)
                {
                    // 字串轉成List<int>
                    List<int> wordList = user.WordList.Split(',').Select(int.Parse).ToList();
                    // 使用LINQ取得每頁的List<int>
                    // 算出總頁數(到時還要加條件式)
                    //int totalPages = (int)Math.Ceiling(wordList.Count / (double)Convert.ToInt16(parameter.PageSize));
                    // 每頁撈幾筆(parameter.PageSize)由Flutter端利用API參數決定(到時還要加條件式)
                    List<int> pageWordList = wordList.Skip((Convert.ToInt16(parameter.PageToLoad) - 1) * Convert.ToInt16(parameter.PageSize)).Take(Convert.ToInt16(parameter.PageSize)).ToList();
                    // 將每頁的vocabularyId跑迴圈查出每筆Vocabulary物件，並傳成VocabularyDto物件
                    foreach (var item in pageWordList)
                    {
                        // FindAsync()的參數為int
                        Vocabulary myWord = await _context.Vocabularies.FindAsync(item);
                        myWordList.Add(_mapper.Map<VocabularyDto>(myWord));
                    }
                    serviceResponse.Data = myWordList;
                    //serviceResponse.Message = totalPages.ToString();
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
