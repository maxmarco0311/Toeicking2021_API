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

        public async Task<ServiceResponse<List<SentenceBundleDto>>> GetSentences(TableQueryFormData FormData)
        {
            // 將要查詢資料表物件AsQueryable()
            var source = _context.Sentences.AsQueryable();
            // 將表單資料處理成動態where條件式
            var predicate = DynamicPredicateHelper.SentenceDynamicPredicate(FormData);
            // predicate不為null代表有進行where條件篩選，反之沒有做任何篩選
            if (predicate != null)
            {
                // 使用Where(predicate)篩選IQueryable<T>物件，送到分頁的方法裡查出那一頁的資料
                source = source.Where(predicate);
            }
            // 取得所要的sentence
            List<Sentence> sentences = await source.ToListAsync();
            // 宣告ServiceResponse的T
            List<SentenceBundleDto> data = new List<SentenceBundleDto>();
            // 將所有的sentence跑迴圈，每跑一次就要完成一個SentenceBundleDto物件
            foreach (var item in sentences)
            {
                // 宣告一個空的SentenceBundleDto物件
                SentenceBundleDto bundle = new SentenceBundleDto();
                // 將Sentence物件轉成SentenceDto物件，並放進SentenceBundleDto物件
                bundle.Sentence=_mapper.Map<SentenceDto>(item);
                // 取得該sentence的所有vocabulary
                List<Vocabulary> vocabulariesTemp = await _context.Vocabularies.Where(v => v.SentenceId == item.SentenceId).ToListAsync();
                // 宣告List<VocabularyDto>
                List<VocabularyDto> vocabularies = new List<VocabularyDto>();
                // 將每個Vocabulary物件轉成VocabularyDto物件
                foreach (var vocabulary in vocabulariesTemp)
                {
                    vocabularies.Add(_mapper.Map<VocabularyDto>(vocabulary));
                }
                // 將List<VocabularyDto>放進該SentenceBundleDto物件
                bundle.Vocabularies = vocabularies;
                // 取得該sentence的所有GA
                List<GA> grammarsTemp = await _context.GAs.Where(g => g.SentenceId == item.SentenceId).ToListAsync();
                // 宣告List<GADto>
                List<GADto> grammars = new List<GADto>();
                // 將每個GA物件轉成GADto物件
                foreach (var grammar in grammarsTemp)
                {
                    grammars.Add(_mapper.Map<GADto>(grammar));
                }
                // 將List<GADto>放進該SentenceBundleDto物件
                bundle.GAs = grammars;
                // 取得該sentence的所有VA
                List<VA> vocAnalysesTemp = await _context.VAs.Where(va => va.SentenceId == item.SentenceId).ToListAsync();
                // 宣告List<VADto>
                List<VADto> vocAnalyses = new List<VADto>();
                // 將每個VA物件轉成VADto物件
                foreach (var voc in vocAnalysesTemp)
                {
                    vocAnalyses.Add(_mapper.Map<VADto>(voc));
                }
                // 將List<VADto>放進一個SentenceBundleDto物件
                bundle.VAs = vocAnalyses;
                // 將該SentenceBundleDto物件放進List<SentenceBundleDto>
                data.Add(bundle);
            }
            // 宣告回傳serviceResponse
            ServiceResponse<List<SentenceBundleDto>> serviceResponse = new ServiceResponse<List<SentenceBundleDto>>();
            // 將List<SentenceBundleDto>放進serviceResponse.Data
            serviceResponse.Data = data;
            return serviceResponse;
        }
    }
}
