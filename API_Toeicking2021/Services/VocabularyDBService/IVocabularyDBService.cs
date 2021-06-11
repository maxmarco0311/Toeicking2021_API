using API_Toeicking2021.Dtos;
using API_Toeicking2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Services.VocabularyDBService
{
    public interface IVocabularyDBService
    {
        Task<ServiceResponse<List<VocabularyDto>>> GetVocabularies(GetWordListParameter parameter);
    }
}
