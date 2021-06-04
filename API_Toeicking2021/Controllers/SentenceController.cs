using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        // 對應到flutter的http.get()方法
        // url：domain/Character/GetAll
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
