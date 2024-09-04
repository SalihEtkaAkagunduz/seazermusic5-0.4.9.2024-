using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seazermusic5
{
    class connection
    {
        //firebase bağlantısını ayarlayan sınıf
        IFirebaseConfig fc = new FirebaseConfig
        {
            AuthSecret = "sy3XV4C4MoNK62SVdm2r9yWwxwl4DeYOfNfOxJdR",
            BasePath = "https://seazer-music-default-rtdb.firebaseio.com/"
        };

        public IFirebaseClient client;
        //class çağırıldığı zaman eğer bağlanamıyorsa console'a uyarı vericek kod.
        public connection()
        {
            try
            {
                client = new FireSharp.FirebaseClient(fc);
            }
            catch (Exception)
            {
                Console.WriteLine("sunucuya bağlanılamadı");
            }
        }
    }
}