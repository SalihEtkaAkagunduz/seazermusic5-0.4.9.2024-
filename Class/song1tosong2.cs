using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace seazermusic5
{
    static class song1tosong2
    {
        static public Song2 convert(Song aa, bool s)
        {
            if (true)
            {

            }
            Song a = aa;
            Song2 dd = new Song2();
            String aasf = "";
            try
            {
                if (Convert.ToInt32(a.Length) <= 3600)
                {
                    aasf = $"{Math.Round((Double)Convert.ToInt32(a.Length) / 60)}:{((Convert.ToInt32(a.Length) % 60) < 10 ? "0" + (Convert.ToInt32(a.Length) % 60) : (Convert.ToInt32(a.Length) % 60))}";
                }
                else
                {
                    aasf = $"{Math.Round((Double)Convert.ToInt32(a.Length) / 3600)}:{Math.Round((Double)Convert.ToInt32(a.Length) / 60) - (Math.Round((Double)Convert.ToInt32(a.Length) / 3600) * 60)}:{((Convert.ToInt32(a.Length) % 3600) < 10 ? "0" + (Convert.ToInt32(a.Length) % 60) : (Convert.ToInt32(a.Length) % 60))}";
                }

                a.Length = aasf;

                dd.Artist = a.Artist;
                dd.Title = a.Title;
                dd.audioStreamInfo = a.audioStreamInfo;
                dd.Length = a.Length;
                dd.ImageUrl = a.ImageUrl;
                dd.YouTubeLink = a.YouTubeLink;
                dd.Single = a.Single;
                dd.Tag = a.Tag;
                dd.varmi = s;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(a.Length);
                dd.Artist = a.Artist;
                dd.Title = a.Title;
                dd.Length = a.Length;
                dd.ImageUrl = a.ImageUrl;
                dd.audioStreamInfo=a.audioStreamInfo;
                dd.YouTubeLink = a.YouTubeLink;
                dd.Single = a.Single;
                dd.Tag = a.Tag;
                dd.varmi = s;
            }
            return dd;
        }


        static public List<Song> Convert2(List<Song2> aa)
        {
            List<Song> s = new List<Song>();
            foreach (Song2 item in aa)
            {
                Song ss= new Song();
                ss.Artist = item.Artist;
                ss.Title = item.Title;  
                ss.Length = item.Length;
                ss.audioStreamInfo= item.audioStreamInfo;
                ss.ImageUrl = item.ImageUrl;
                ss.Single = item.Single;
                ss.Tag = item.Tag;
                ss.YouTubeLink= item.YouTubeLink;
                s.Add(ss);
            }
            return s; 
        }
        static public  Song Convert3( Song2 item)
        {
             
                Song ss = new Song();
                ss.Artist = item.Artist;
                ss.Title = item.Title;
                ss.Length = item.Length;
                ss.ImageUrl = item.ImageUrl;
            ss.audioStreamInfo= item.audioStreamInfo;
                ss.Single = item.Single;
                ss.Tag = item.Tag;
                ss.YouTubeLink = item.YouTubeLink;
               
          
            return ss;
        }
    }

}

