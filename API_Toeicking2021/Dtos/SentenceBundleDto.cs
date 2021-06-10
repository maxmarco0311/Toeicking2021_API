using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Dtos
{
    public class SentenceBundleDto
    {
        public SentenceDto Sentence { get; set; }
        public List<VocabularyDto> Vocabularies { get; set; }
        public List<GADto> GAs { get; set; }
        public List<VADto> VAs { get; set; }
        public Dictionary<string, string> NormalAudioUrls { get; set; }
        public Dictionary<string, string> FastAudioUrls { get; set; }
        public Dictionary<string, string> SlowAudioUrls { get; set; }

    }
}
