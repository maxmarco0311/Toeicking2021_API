using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Toeicking2021.Models;
using API_Toeicking2021.Services.SentenceDBService;
using API_Toeicking2021.Services.UserDBService;
using Microsoft.AspNetCore.Mvc;

namespace API_Toeicking2021.Controllers
{
    // 將一般controller變成api controller的設定：
    // 2.[ApiController]
    [ApiController]
    // 3.[Route("[controller]")]-->代表這個api controller的url路徑為：domain/controller名稱
    [Route("[controller]")]
    // 1.此controller繼承ControllerBase類別
    public class SentenceController : ControllerBase
    {
        private readonly ISentenceDBService _sentenceDBService;
        private readonly IUserDBService _UserDBService;

        public SentenceController(ISentenceDBService sentenceDBService, IUserDBService UserDBService)
        {
            _sentenceDBService = sentenceDBService;
            _UserDBService = UserDBService;
        }

        // 取得所篩選的句子(url：domain/Sentence/GetSentences)
        // ***GET參數是複雜(自訂)型別，所以一定要加[FromQuery]，否則會報錯***
        [HttpGet("GetSentences")]
        public async Task<IActionResult> Get([FromQuery] GetSentencesParameter parameter)
        {
            // parameter中的Email是用來做檢查進行api請求時user是否valid
            var response = await _sentenceDBService.GetSentences(parameter.FormData);
            return Ok(response);
        }

        // 依字彙編號取得句子(url：domain/Sentence/GetSentenceByVocabularyId)
        [HttpGet("GetSentenceByVocabularyId")]
        public async Task<IActionResult> GetSentenceByVocabularyId([FromQuery] WordListParameter parameter)
        {
            var response = await _sentenceDBService.GetSentenceBundleByVocabularyId(parameter);
            return Ok(response);
        }
        // 依句子編號取得句子(url：domain/Sentence/GetSentenceBySentenceId)
        [HttpGet("GetSentenceBySentenceId")]
        public async Task<IActionResult> GetSentenceBySentenceId([FromQuery] GetSingleSentenceParameter parameter)
        {
            var response = await _sentenceDBService.GetSentenceBundleBySentenceId(parameter);
            return Ok(response);
        }


    }
}
