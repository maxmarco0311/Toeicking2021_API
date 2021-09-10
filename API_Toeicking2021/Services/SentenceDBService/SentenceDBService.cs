using API_Toeicking2021.Data;
using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using API_Toeicking2021.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Services.SentenceDBService
{
    public class SentenceDBService : ISentenceDBService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SentenceDBService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        #region 查出(依條件篩選的)所有句子
        public async Task<ServiceResponse<List<SentenceBundleDto>>> GetSentences(TableQueryFormData FormData)
        {
            // 先宣告回傳serviceResponse
            ServiceResponse<List<SentenceBundleDto>> serviceResponse = new ServiceResponse<List<SentenceBundleDto>>();
            try
            {
                // 將要查詢資料表物件AsQueryable()
                var source = _context.Sentences.AsQueryable();
                // 要檢查FormData參數，FormData若為空(沒有做任何篩選)，進入DynamicPredicateHelper會報例外錯誤
                if (FormData!=null)
                {
                    // 將篩選資料處理成動態where條件式
                    var predicate = DynamicPredicateHelper.SentenceDynamicPredicate(FormData);
                    // predicate不為null代表有進行where條件篩選，反之沒有做任何篩選
                    if (predicate != null)
                    {
                        // 使用Where(predicate)篩選IQueryable<T>物件，送到分頁的方法裡查出那一頁的資料
                        source = source.Where(predicate);
                    }
                }
                // 取得所有的sentence
                List<Sentence> sentences = await source.ToListAsync();
                // 宣告ServiceResponse的T，這裡是要回傳的List<SentenceBundleDto>
                List<SentenceBundleDto> data = new List<SentenceBundleDto>();
                // 將所有的sentence跑迴圈，每跑一次就要完成一個SentenceBundleDto物件
                foreach (var item in sentences)
                {
                    // 呼叫GenerateSentenceBundleBySentence，生出SentenceBundleDto物件
                    SentenceBundleDto bundle = await GenerateSentenceBundleBySentence(item);
                    // 將該SentenceBundleDto物件放進List<SentenceBundleDto>
                    data.Add(bundle);
                }
                // 將List<SentenceBundleDto>放進serviceResponse.Data
                serviceResponse.Data = data;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        #endregion

        #region 依字彙編號取得句子bundle(點按字彙列表某字彙後顯示該字彙全部相關內容)
        public async Task<ServiceResponse<SentenceBundleDto>> GetSentenceBundleByVocabularyId(WordListParameter parameter)
        {
            // 將參數轉型成int的vocabularyId
            int vocabularyId = Convert.ToInt32(parameter.VocabularyId);
            // 先宣告回傳serviceResponse
            ServiceResponse<SentenceBundleDto> serviceResponse = new ServiceResponse<SentenceBundleDto>();
            try
            {
                // 利用vocabularyId取得Sentence物件(靠Vocablary類別裡的導覽屬性Sentence去獲得關聯的Sentence物件)：
                Sentence sentence = await _context.Vocabularies
                                .Where(v => v.VocabularyId == vocabularyId)
                                // 1. 利用Select()方法將Vocabulary物件轉成Sentence物件
                                .Select(v => new Sentence
                                {
                                    // 2. Vocabulary物件中有導覽屬性Sentence，所以可獲得與其關聯的Sentence物件資料
                                    SentenceId = v.Sentence.SentenceId,
                                    Sen = v.Sentence.Sen,
                                    Chinesese = v.Sentence.Chinesese
                                })
                                // 3. Select()之後物件型別是IQueryable<Sentence>，要利用FirstOrDefaultAsync()才能轉為Sentence型別
                                .FirstOrDefaultAsync();
                // 呼叫GenerateSentenceBundleBySentence，生出SentenceBundleDto物件
                SentenceBundleDto bundle = await GenerateSentenceBundleBySentence(sentence);
                serviceResponse.Data = bundle;
                // ***將該字彙置於WordList的第一個***
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == parameter.Email);
                if (user != null)
                {
                    // 呼叫自訂方法將該VocabularyId置於集合第一個元素後再轉成字串
                    user.WordList = WordListOrderHelper.MoveToTop(user.WordList, parameter.VocabularyId);
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
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

        #region 依句子編號取得句子bundle(點按字彙列表某字彙後顯示該字彙全部相關內容)
        public async Task<ServiceResponse<SentenceBundleDto>> GetSentenceBundleBySentenceId(GetSingleSentenceParameter parameter)
        {
            // 先宣告回傳serviceResponse
            ServiceResponse<SentenceBundleDto> serviceResponse = new ServiceResponse<SentenceBundleDto>();
            try
            {
                Sentence sentence = await _context.Sentences.FirstOrDefaultAsync(s=> s.SentenceId==Convert.ToInt16(parameter.SentenceId));
                // 呼叫GenerateSentenceBundleBySentence，生出SentenceBundleDto物件
                SentenceBundleDto bundle = await GenerateSentenceBundleBySentence(sentence);
                serviceResponse.Data = bundle;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        #endregion

        #region 類別內工具方法：用Sentence物件生出SentenceBundleDto物件
        public async Task<SentenceBundleDto> GenerateSentenceBundleBySentence(Sentence sentence)
        {
            // 宣告一個空的SentenceBundleDto物件
            SentenceBundleDto bundle = new SentenceBundleDto();
            // 將Sentence物件轉成SentenceDto物件，並放進SentenceBundleDto物件
            bundle.Sentence = _mapper.Map<SentenceDto>(sentence);
            // 將每個Vocabulary物件轉成VocabularyDto物件
            // ***Select()是將選到的物件(或集合)迴圈做加工處理後回傳，這裡的加工就是將Vocabulary物件轉成VocabularyDto物件***
            List<VocabularyDto> vocabularies = await _context.Vocabularies.Where(v => v.SentenceId == sentence.SentenceId)
                .Select(v => _mapper.Map<VocabularyDto>(v)).ToListAsync();
            // 將List<VocabularyDto> 放進該SentenceBundleDto物件
            bundle.Vocabularies = vocabularies;
            // 將每個GA物件轉成GADto物件
            List<GADto> grammars = await _context.GAs.Where(g => g.SentenceId == sentence.SentenceId)
                .Select(g => _mapper.Map<GADto>(g)).ToListAsync();
            // 將List<GADto>放進該SentenceBundleDto物件
            bundle.GAs = grammars;
            // 將每個VA物件轉成VADto物件
            List<VADto> vocAnalyses = await _context.VAs.Where(va => va.SentenceId == sentence.SentenceId)
                .Select(va => _mapper.Map<VADto>(va)).ToListAsync();
            // 將List<VADto>放進該SentenceBundleDto物件
            bundle.VAs = vocAnalyses;
            // 音檔url
            bundle.NormalAudioUrls = GenerateAudioUrl.AudioUrls("1.0", sentence.SentenceId.ToString());
            bundle.FastAudioUrls = GenerateAudioUrl.AudioUrls("1.25", sentence.SentenceId.ToString());
            bundle.SlowAudioUrls = GenerateAudioUrl.AudioUrls("0.75", sentence.SentenceId.ToString());
            return bundle;

        }
        #endregion

    }
}
