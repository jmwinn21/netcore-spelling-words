using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using spelling_words.Models;

namespace spelling_words.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase {

        private static WordsResponse _currentWords { get; set; }
        private static WordsResponse _allWords { get; set; }

        public WordsController () {
            using (StreamReader r = new StreamReader ("Data/all.json")) {
                string json = r.ReadToEnd ();
                _allWords = JsonConvert.DeserializeObject<WordsResponse> (json);
            }

            using (StreamReader r = new StreamReader ("Data/current.json")) {
                string json = r.ReadToEnd ();
                _currentWords = JsonConvert.DeserializeObject<WordsResponse> (json);
            }
        }

        // GET api/words
        [Route ("current")]
        [HttpGet]
        public JsonResult GetCurrent () {
            _currentWords.Words.Shuffle ();
            return new JsonResult (_currentWords);
        }

        [Route ("all")]
        [HttpGet]
        public JsonResult GetAll () {
            _allWords.Words.Shuffle ();
            return new JsonResult (_allWords);
        }

        [Route ("random")]
        [HttpGet]
        public JsonResult GetRandon () {
            _allWords.Words.Shuffle ();
            WordsResponse resp = new WordsResponse {
                Words = _allWords.Words.Take (10).ToList ()
            };
            return new JsonResult (resp);
        }
    }
}