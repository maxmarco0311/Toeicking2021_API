using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Utilities
{
    public class GenerateAudioUrl
    {
        // 必須是靜態屬性，才可以被類別方法存取
        // API呼叫時，路徑wwwroot記得要拿掉
        public static string BaseUrl { get; set; } = "https://voice.toeicking.com/voice/";
        public static Dictionary<string, string> AudioUrls(string rate, string sentenceId)
        {

            Dictionary<string, string> AudioUrlDic = new Dictionary<string, string> 
            {
                {"USM",$"{BaseUrl}{sentenceId}/{rate}/{sentenceId}_{rate}_US_M.mp3"},
                {"USF",$"{BaseUrl}{sentenceId}/{rate}/{sentenceId}_{rate}_US_F.mp3"},
                {"GBM",$"{BaseUrl}{sentenceId}/{rate}/{sentenceId}_{rate}_GB_M.mp3"},
                {"GBF",$"{BaseUrl}{sentenceId}/{rate}/{sentenceId}_{rate}_GB_F.mp3"},
                {"AUM",$"{BaseUrl}{sentenceId}/{rate}/{sentenceId}_{rate}_AU_M.mp3"},
                {"AUF",$"{BaseUrl}{sentenceId}/{rate}/{sentenceId}_{rate}_AU_F.mp3"},

            };
            return AudioUrlDic;
        }
    }
}
