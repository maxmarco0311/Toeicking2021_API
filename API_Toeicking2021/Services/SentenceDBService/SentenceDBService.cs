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
                // 將篩選資料處理成動態where條件式
                var predicate = DynamicPredicateHelper.SentenceDynamicPredicate(FormData);
                // predicate不為null代表有進行where條件篩選，反之沒有做任何篩選
                if (predicate != null)
                {
                    // 使用Where(predicate)篩選IQueryable<T>物件，送到分頁的方法裡查出那一頁的資料
                    source = source.Where(predicate);
                }
                // 取得所有的sentence
                List<Sentence> sentences = await source.ToListAsync();
                // 宣告ServiceResponse的T，這裡是要回傳的List<SentenceBundleDto>
                List<SentenceBundleDto> data = new List<SentenceBundleDto>();
                // 將所有的sentence跑迴圈，每跑一次就要完成一個SentenceBundleDto物件
                foreach (var item in sentences)
                {
                    // 宣告一個空的SentenceBundleDto物件
                    SentenceBundleDto bundle = new SentenceBundleDto();
                    // 將Sentence物件轉成SentenceDto物件，並放進SentenceBundleDto物件
                    bundle.Sentence = _mapper.Map<SentenceDto>(item);
                    // 將每個Vocabulary物件轉成VocabularyDto物件
                    // ***Select()是將選到的物件做加工處理後回傳，這裡的加工就是將Vocabulary物件轉成VocabularyDto物件***
                    List<VocabularyDto> vocabularies = await _context.Vocabularies.Where(v => v.SentenceId == item.SentenceId)
                        .Select(v => _mapper.Map<VocabularyDto>(v)).ToListAsync();
                    // 將List<VocabularyDto> 放進該SentenceBundleDto物件
                    bundle.Vocabularies = vocabularies;
                    // 將每個GA物件轉成GADto物件
                    List<GADto> grammars = await _context.GAs.Where(g => g.SentenceId == item.SentenceId)
                        .Select(g => _mapper.Map<GADto>(g)).ToListAsync();
                    // 將List<GADto>放進該SentenceBundleDto物件
                    bundle.GAs = grammars;
                    // 將每個VA物件轉成VADto物件
                    List<VADto> vocAnalyses = await _context.VAs.Where(va => va.SentenceId == item.SentenceId)
                        .Select(va => _mapper.Map<VADto>(va)).ToListAsync();
                    // 將List<VADto>放進該SentenceBundleDto物件
                    bundle.VAs = vocAnalyses;
                    // 音檔url
                    bundle.NormalAudioUrls = GenerateAudioUrl.AudioUrls("1.0", item.SentenceId.ToString());
                    bundle.FastAudioUrls = GenerateAudioUrl.AudioUrls("1.25", item.SentenceId.ToString());
                    bundle.SlowAudioUrls = GenerateAudioUrl.AudioUrls("0.75", item.SentenceId.ToString());
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




    }
}
