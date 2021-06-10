using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Toeicking2021.Models;
using API_Toeicking2021.Services.SentenceDBService;
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

        public SentenceController(ISentenceDBService sentenceDBService)
        {
            _sentenceDBService = sentenceDBService;
        }

        // 取得所篩選的句子(url：domain/Sentence/GetSentences)
        // ***因為參數是複雜(自訂)型別，所以一定要加[FromQuery]，否則會報錯***
        [HttpGet("GetSentences")]
        public async Task<IActionResult> Get([FromQuery] TableQueryFormData formData)
        {
            var response = await _sentenceDBService.GetSentences(formData);
            return Ok(response);
        }
 




    }
}
