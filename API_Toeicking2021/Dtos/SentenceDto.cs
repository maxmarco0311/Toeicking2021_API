using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Dtos
{
    public class SentenceDto
    {
        public int SentenceId { get; set; }
        public string Sen { get; set; }
        public string Chinesese { get; set; }
        //public Dictionary<string,string> NormalAudioUrls
        //{
        //    get
        //    {
        //        string baseUrl = "https://api.toeicking.com/wwwroot/voice/";
        //        string rate = "1.0";
        //        NormalAudioUrls.Add("USM", $"{baseUrl}{SentenceId}/{rate}/{SentenceId}_{rate}_US_M.mp3");
        //        NormalAudioUrls.Add("USF", $"{baseUrl}{SentenceId}/{rate}/{SentenceId}_{rate}_US_F.mp3");
        //        NormalAudioUrls.Add("GBM", $"{baseUrl}{SentenceId}/{rate}/{SentenceId}_{rate}_GB_M.mp3");
        //        NormalAudioUrls.Add("GBF", $"{baseUrl}{SentenceId}/{rate}/{SentenceId}_{rate}_GB_F.mp3");
        //        NormalAudioUrls.Add("AUM", $"{baseUrl}{SentenceId}/{rate}/{SentenceId}_{rate}_AU_M.mp3");
        //        NormalAudioUrls.Add("AUF", $"{baseUrl}{SentenceId}/{rate}/{SentenceId}_{rate}_AU_F.mp3");
        //        return NormalAudioUrls;
        //    }
        //}

    }
}
