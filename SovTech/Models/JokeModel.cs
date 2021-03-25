using System;
using System.Collections.Generic;

namespace SovTech.Models
{
    public class JokeModel
    {
        public List<string> Categories { get; set; }
        public DateTime Created_at { get; set; }
        public string Icon_url { get; set; }
        public string Id { get; set; }
        public DateTime Updated_at { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }
    }

    public class JokeSearchModel
    {
        public int Total { get; set; }
        public List<JokeModel> Result { get; set; }
    }
}
