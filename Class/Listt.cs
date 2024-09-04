using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seazermusic5
{
    public class Listt
    {
        public string? Name
        {
            get; set;
        }
        public string? Description
        {
            get; set;
        }
        public string? ImageUrl
        {
            get; set;
        }
        public Dictionary<string, Song>? Songs
        {
            get; set;
        }
    }
    public class Listt2
    {
        public string? Name
        {
            get; set;
        }
        public string? Rss
        {
            get; set;
        }
        public string? ImageUrl
        {
            get; set;
        }
        
    }
}
