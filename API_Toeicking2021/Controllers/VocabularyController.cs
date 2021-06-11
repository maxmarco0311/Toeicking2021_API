using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Toeicking2021.Models;
using API_Toeicking2021.Services.VocabularyDBService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Toeicking2021.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private readonly IVocabularyDBService _vocabularyDBService;

        public VocabularyController(IVocabularyDBService vocabualryDBService)
        {
            _vocabularyDBService = vocabualryDBService;
        }

        // 取得我的字彙列表(url：domain/Vocabulary/GetVocabularies)
        // POST時即便是複雜(自訂)型別，也可以不用加[FromBody]
        [HttpPost("GetVocabularies")]
        public async Task<IActionResult> Get(GetWordListParameter parameter)
        {
            var response = await _vocabularyDBService.GetVocabularies(parameter);
            return Ok(response);
        }
    }
}
