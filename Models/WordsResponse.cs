using System.Collections.Generic;
using Newtonsoft.Json;

namespace spelling_words.Models {
    public class WordsResponse {
        public List<string> Words { get; set; }
    }
}