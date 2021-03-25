using System;
using System.Collections.Generic;

namespace SovTech.Models
{
    public class StarWarsPerson
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string Hair_color { get; set; }
        public string Skin_color { get; set; }
        public string Eye_color { get; set; }
        public string Birth_year { get; set; }
        public string Gender { get; set; }
        public string Homeworld { get; set; }
        public List<string> Films { get; set; }
        public List<string> Species { get; set; }
        public List<string> Vehicles { get; set; }
        public List<string> Starship { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string Url { get; set; }
    }

    public class SwapiModel
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<StarWarsPerson> Results { get; set; }
    }
}