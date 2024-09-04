using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seazermusic5;
internal class podcastclass
{
    public class PodcastChannel
    {
        public string RssUrl
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Image
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public string Link
        {
            get; set;
        }
        public List<PodcastItem> Items { get; set; } = new List<PodcastItem>();
    }

    public class PodcastItem
    {
        public string Image
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public string Link
        {
            get; set;
        }
        public string PubDate
        {
            get; set;
        }
        public string EnclosureUrl
        {
            get; set;
        }
    
        public string ChannelTitle
        {
            get; set;
        }
    }
}
