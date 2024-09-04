using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using seazermusic5;


namespace FirebaseMedium
{
    class Crud
    {

        connection conn = new connection();
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "sy3XV4C4MoNK62SVdm2r9yWwxwl4DeYOfNfOxJdR",
            BasePath = "https://seazer-music-default-rtdb.firebaseio.com/"
        };



        //database'e veri ekleme işlemi
        public void SetData(string? ImageUrl, string? Title, string? Artist, string? Single, string? Length, String Tag, string? YouTubeLink, string? audioStreamInfo)
        {
            try
            {
                Song set = new Song()
                {
                    ImageUrl = ImageUrl,
                    Title = Title,
                    Artist = Artist,
                    Single = Single,
                    Length = Length,
                    audioStreamInfo = audioStreamInfo,
                    Tag = Tag,
                    YouTubeLink = YouTubeLink
                };

                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = "sy3XV4C4MoNK62SVdm2r9yWwxwl4DeYOfNfOxJdR",
                    BasePath = "https://seazer-music-default-rtdb.firebaseio.com/"
                };
            }
            catch (Exception)
            {
                Debug.WriteLine("bir hata ile karşılaşıldı");
            }

        }
        public void SetData(Song s)
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

            try
            {
                string userId = aa;
                Song set = new Song()
                {
                    ImageUrl = s.ImageUrl,
                    Title = s.Title,
                    Artist = s.Artist,
                    Single = s.Single,
                    Length = s.Length,
                    audioStreamInfo = s.audioStreamInfo,
                    Tag = s.Tag,
                    YouTubeLink = s.YouTubeLink
                };
                IFirebaseClient client = new FireSharp.FirebaseClient(config);

                if (client == null)
                {
                    Debug.WriteLine("Connection failed");
                    return;
                }
                var response = client.Get($"users/{userId}/AllMusics/Songs");

                if (response.Body == "null") // Eğer konum yoksa
                {
                    // Konumu oluştur
                    var SetData = conn.client.Set($"users/{userId}/AllMusics/Songs", set);
                }
                else
                {
                    // Konum varsa güncelle
                    var SetData = conn.client.Push($"users/{userId}/AllMusics/Songs", set);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
        //verileri güncelleme işlemi
        public void UpdateData(string? ImageUrl, string? Title, string? Artist, string? Single, string? Length, String Tag, string? YouTubeLink, string? audioStreamInfo)
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

            try
            {
                string userId = aa;
                Song set = new Song()
                {
                    ImageUrl = ImageUrl,
                    Title = Title,
                    Artist = Artist,
                    Single = Single,
                    Length = Length,
                    audioStreamInfo = audioStreamInfo,
                    Tag = Tag,
                    YouTubeLink = YouTubeLink
                };

                var SetData = conn.client.Update($"users/{userId}/Lists/List1/Songs", set);
            }
            catch (Exception)
            {
                Debug.WriteLine("bir hata ile karşılaşıldı");
            }
        }
        public void AddPodcast(string listName, string songs)
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

            try
            {
                string userId = aa;
                IFirebaseClient client = new FireSharp.FirebaseClient(config);

                if (client == null)
                {
                    Debug.WriteLine("Connection failed");
                    return;
                }

                var response = client.Get($"podcast/{listName}");

                if (response.Body == "null") // Eğer konum yoksa
                {
                    // Konumu oluştur ve listeyi ekle
                    var setData = client.Set($"podcast/{listName}", songs);
                }
                else
                {
                    // Konum varsa listeyi güncelle
                    var setData = client.Update($"podcast/{listName}", songs);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        public void AddList(Listt aaa)
        {
            

String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";



                                              
            try
            {
                string userId = aa;
                IFirebaseClient client = new FireSharp.FirebaseClient(config);

                if (client == null)
                {
                    Debug.WriteLine("Connection failed");
                    return;
                }

                Listt list = aaa;

                // Sanitize the list name to remove invalid characters
                string sanitizedListName = SanitizeFirebaseKey(list.Name);

                if (string.IsNullOrEmpty(sanitizedListName))
                {
                    Debug.WriteLine("Invalid list name");
                    return;
                }

                var response = client.Get($"users/{userId}/Lists/{sanitizedListName}");

                if (response.Body == "null")
                {
                    var setData = client.Set($"users/{userId}/Lists/{sanitizedListName}", list);
                }
                else
                {
                    var setData = client.Update($"users/{userId}/Lists/{sanitizedListName}", list);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private string SanitizeFirebaseKey(string key)
        {
            // Replace invalid characters with an underscore
            return key.Replace(".", "_")
                      .Replace("$", "_")
                      .Replace("#", "_")
                      .Replace("[", "_")
                      .Replace("]", "_")
                      .Replace("/", "_");
        }
        public void AddPodcast(Listt2 songs)
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";
            
            try
            {
                string userId = aa;
                IFirebaseClient client = new FireSharp.FirebaseClient(config);

                if (client == null)
                {
                    Debug.WriteLine("Connection failed");
                    return;
                }
                var sanitizedSongName = SanitizeFirebaseKey(songs.Name);
                var response = client.Get($"users/{userId}/podcast/{sanitizedSongName}/");

                 

                if (response.Body == "null") // Eğer konum yoksa
                {
                    // Konumu oluştur ve listeyi ekle
                    var setData = client.Set($"users/{userId}/podcast/{sanitizedSongName}", songs);
                }
                else
                {
                    // Konum varsa listeyi güncelle
                    var setData = client.Update($"users/{userId}/podcast/{sanitizedSongName}", songs);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
        public Dictionary<string, Listt2> LoadAllpodcast()
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

            try
            {
                string userId = aa;
                FirebaseResponse response = conn.client.Get($"users/{userId}/podcast/");
                var allLists = JsonConvert.DeserializeObject<Dictionary<string, Listt2>>(response.Body.ToString());
                return allLists;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"bir hata ile karşılaşıldı: {ex.Message}");
                return null;
            }
        }
        public Dictionary<string, Listt2> LoadBrowsepodcast()
        {
            

            try
            {
               
                FirebaseResponse response = conn.client.Get($"browse/Podcasts/");
                var allLists = JsonConvert.DeserializeObject<Dictionary<string, Listt2>>(response.Body.ToString());
                return allLists;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"bir hata ile karşılaşıldı: {ex.Message}");
                return null;
            }
        }
        public Dictionary<string, Listt> LoadAllLists()
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

            try
            {
                string userId = aa;
                FirebaseResponse response = conn.client.Get($"users/{userId}/Lists");
                var allLists = JsonConvert.DeserializeObject<Dictionary<string, Listt>>(response.Body.ToString());
                return allLists;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"bir hata ile karşılaşıldı: {ex.Message}");
                return null;
            }
        }

        //Veri silme
        public void DeleteTeam(string Name)
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

            try
            {
                string userId = aa;
                // Öncelikle verinin anahtarını bulun
                var allData = LoadDataa();
                var keyToDelete = allData.FirstOrDefault(x => x.Value.Title == Name).Key; // Örneğin, Title'a göre arama yaparak
                if (!string.IsNullOrEmpty(keyToDelete))
                {
                    // Anahtar bulunduysa, veriyi sil
                    conn.client.Delete($"users/{userId}/Lists/List1/Songs" + keyToDelete);
                    Debug.WriteLine($"{Name} başarıyla silindi.");
                }
                else
                {
                    Debug.WriteLine($"{Name} bulunamadı.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Bir hata ile karşılaşıldı: " + e.Message);
            }
        }

        public List<Listt> LoadAllListNames(string listName)
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

            try
            {
                string userId = aa;
                FirebaseResponse response = conn.client.Get($"users/{userId}/Lists/{listName}");
                var allLists = JsonConvert.DeserializeObject<Dictionary<string, Listt>>(response.Body.ToString()).Values.ToList();

                // Songs özelliğini null yaparak sadece liste adlarını ve diğer bilgileri alıyoruz
                

                return allLists;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"bir hata ile karşılaşıldı: {ex.Message}");
                return null;
            }
        }
        public Dictionary<string, Listt> LoadBrowseListsvoid()
        {


            try
            {

                FirebaseResponse response = conn.client.Get($"browse/Lists/");
                var allLists = JsonConvert.DeserializeObject<Dictionary<string, Listt>>(response.Body.ToString());

                // Songs özelliğini null yaparak sadece liste adlarını ve diğer bilgileri alıyoruz
                foreach (var list in allLists.Values)
                {
                    list.Songs = null;
                }

                return allLists;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"bir hata ile karşılaşıldı: {ex.Message}");
                return null;
            }
        }
        public Dictionary<string, Listt> LoadAllListsvoid()
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

            try
            {
                string userId = aa;
                FirebaseResponse response = conn.client.Get($"users/{userId}/Lists");
                var allLists = JsonConvert.DeserializeObject<Dictionary<string, Listt>>(response.Body.ToString());

                // Songs özelliğini null yaparak sadece liste adlarını ve diğer bilgileri alıyoruz
                foreach (var list in allLists.Values)
                {
                    list.Songs = null;
                }

                return allLists;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"bir hata ile karşılaşıldı: {ex.Message}");
                return null;
            }
        }
        
        //verilerin hepsini listeleme
        public Dictionary<string, Song> LoadData()
        {
            try
            {
                FirebaseResponse al = conn.client.Get("users/" + "SalihDeneme/Lists/List1/Songs");
                Dictionary<string, Song> ListData = JsonConvert.DeserializeObject<Dictionary<string, Song>>(al.Body.ToString());
                return ListData;
            }
            catch (Exception)
            {
                Debug.WriteLine("bir hata ile karşılaşıldı");
                return null;
            }
        }
        public Dictionary<string, Song> LoadDataa()
        {
            String aa = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";

          
            try
            {
                string userId = aa;
                FirebaseResponse al = conn.client.Get($"users/{userId}/AllMusics/Songs");
                Dictionary<string, Song> ListData = JsonConvert.DeserializeObject<Dictionary<string, Song>>(al.Body.ToString());
                return ListData;
            }
            catch (Exception)
            {
                Debug.WriteLine("bir hata ile karşılaşıldı");
                return null;
            }
        }
        public Dictionary<string, Song> LoadBrowseSongs()
        { 


            try
            {
                
                FirebaseResponse al = conn.client.Get($"browse/Songsa/");
                Dictionary<string, Song> ListData = JsonConvert.DeserializeObject<Dictionary<string, Song>>(al.Body.ToString());
                return ListData;
            }
            catch (Exception)
            {
                Debug.WriteLine("bir hata ile karşılaşıldı");
                return null;
            }
        }
    }
}