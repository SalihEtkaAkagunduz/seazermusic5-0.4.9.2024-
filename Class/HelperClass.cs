using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seazermusic5;
internal class HelperClass
{
    public static List<Song> ConvertSongLengthsToSeconds(List<Song> songs)
    {List<Song> temp = new List<Song>();
        foreach (var song in songs)
        {
            if (song.Length.Equals(":"))
            {
song.Length = ConvertToSeconds(song.Length).ToString(); temp.Add(song);
            }
            else
            {
 temp.Add(song);
            }
            
           
              
        }return temp;  
    }

    public static string ConvertToSeconds(string time)
    {
        if (time.Contains(":"))
        {
if (string.IsNullOrEmpty(time))
        {
            throw new ArgumentException("Time string cannot be null or empty", nameof(time));
        }

        string[] parts = time.Split(':');
        if (parts.Length == 2)
        {
                int minutes = int.Parse(parts[0]);
                int seconds = int.Parse(parts[1]);

                return ((minutes * 60) + seconds).ToString();
            }
            if (parts.Length == 3)
            {
                int hourses = int.Parse(parts[0]);
                int minutes = int.Parse(parts[1]);
                int seconds = int.Parse(parts[1]);
                return ((hourses*3600)+(minutes * 60) + seconds).ToString();
            }
            else { 
                throw new FormatException("Time string must be in the format mm:ss");
            }
            
        }
        else
        {
            return time;
        }
        
    }
}
