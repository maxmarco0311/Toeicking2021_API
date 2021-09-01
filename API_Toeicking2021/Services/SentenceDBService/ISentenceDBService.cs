using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Services.SentenceDBService
{
    public interface ISentenceDBService
    {
        Task<ServiceResponse<List<SentenceBundleDto>>> GetSentences(TableQueryFormData FormData);
        Task<ServiceResponse<SentenceBundleDto>> GetSentenceBundleByVocabularyId(WordListParameter parameter);
        Task<ServiceResponse<SentenceBundleDto>> GetSentenceBundleBySentenceId(GetSingleSentenceParameter parameter);
    }
}
