using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Laerdal;
using Laerdal.FFmpeg;
using Microsoft.Maui.Controls;
using VideoLibrary;
using Plugin;
using YoutubeExplode;
 
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using NReco.VideoConverter;
using System.Diagnostics;

using Plugin.Maui.Audio;
using YoutubeExplode.Videos;

using FirebaseMedium;
 
 
using System.Text;
namespace seazermusic5
{
  
    public partial class FooterView : ContentView
    {  String directoryPath = "";
        #region Deðiþkenler
        string str;
        int tek = 0; List<Song> songs;
        String videon = "";
        bool isExecuted = true;
        string sarii = "";
 
        private bool isPlaying = false;
        Song song;
        Dictionary<string, Song> ff;
        Crud crud;
        private System.Timers.Timer playbackTimer;
        int bb = 0;
        int aa = 0;
   

        #endregion
        public FooterView()
        {
            InitializeComponent();
            BindingContext = new FooterViewModel();
#if WINDOWS
   directoryPath = "C:\\ProgramData\\Seazer Software\\Seazer Music\\";
#elif ANDROID
            
            directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

#endif
            MessagingCenter.Subscribe<listeekrani, List<Song>>(this, "                                              gmessage", (sender, arg) =>
            {
                songs = HelperClass.ConvertSongLengthsToSeconds(arg);

            });

            MessagingCenter.Subscribe<kayýtlýsarki, List<Song>>(this, "Songmessage", (sender, arg) =>
            {
                songs = HelperClass.ConvertSongLengthsToSeconds(arg);

            });
            MessagingCenter.Subscribe<listeekrani, int>(this, "integerMessagxe", (sender, arg) =>
            {
                int uu = (int)arg;
                Song g = songs[arg];

                sarii = g.Title.Replace(' ', '-');
                song = g;
                g.Length = HelperClass.ConvertToSeconds(g.Length).ToString();
                videon = song.Title.Replace(' ', '-');
#if WINDOWS
        sarkislider.Maximum = Convert.ToDouble(song.Length);
        sarkislider.IsEnabled = true;
        geributon.IsEnabled = true;
        ileributon.IsEnabled = true;
        oynat.IsEnabled = true;
        albmresim.Source = g.ImageUrl;
        sarkiadi.Text = g.Title;
        yazaradi.Text = g.Artist;
#endif
#if ANDROID
                albmresim1.WidthRequest = 305;
                albmresim1.HeightRequest = 75;
                ileributon1.IsEnabled = true;
                oynat1.IsEnabled = true;
                albmresim1.Source = g.ImageUrl;
                sarkiadi1.Text = g.Title;
                yazaradi1.Text = g.Artist;
#endif
                if (audioPlayer2 != null)
                {
                    endaudio();
                }
            });
            MessagingCenter.Subscribe<kayýtlýsarki, int>(this, "integerMessage", (sender, arg) =>
            {
                int uu = (int)arg;
                Song g = songs[arg];

                sarii = g.Title.Replace(' ', '-');
                song = g;
                g.Length = HelperClass.ConvertToSeconds(g.Length).ToString();
                videon = song.Title.Replace(' ', '-');
#if WINDOWS
        sarkislider.Maximum = Convert.ToDouble(song.Length);
        sarkislider.IsEnabled = true;
        geributon.IsEnabled = true;
        ileributon.IsEnabled = true;
        oynat.IsEnabled = true;
        albmresim.Source = g.ImageUrl;
        sarkiadi.Text = g.Title;
        yazaradi.Text = g.Artist;
#endif
#if ANDROID
                albmresim1.WidthRequest = 305;
                albmresim1.HeightRequest = 75;
                ileributon1.IsEnabled = true;
                oynat1.IsEnabled = true;
                albmresim1.Source = g.ImageUrl;
                sarkiadi1.Text = g.Title;
                yazaradi1.Text = g.Artist;
#endif
                if (audioPlayer2 != null)
                {
                    endaudio();
                }
            });
   
        }

        private async Task InitializeFooterViewAsync()
        {
            #region simple  
            

#if WINDOWS
    sarkislider.IsEnabled = false;
    geributon.IsEnabled = false;
    ileributon.IsEnabled = false;
    oynat.IsEnabled = false;
#endif
#if ANDROID
            ileributon1.IsEnabled = false;
            oynat1.IsEnabled = false;
#endif
            #endregion

            #region MessagingCenter
            MessagingCenter.Subscribe<listeekrani, int>(this, "integerMessagxe", (sender, arg) =>
            {
                int uu = (int)arg;
                Song g = songs[arg];

                sarii = g.Title.Replace(' ', '-');
                song = g;
                g.Length = HelperClass.ConvertToSeconds(g.Length).ToString();
                videon = song.Title.Replace(' ', '-');
#if WINDOWS
        albmresim.Source = g.ImageUrl;
        sarkiadi.Text = g.Title;
        yazaradi.Text = g.Artist;
        sarkislider.Maximum = Convert.ToDouble(song.Length);
        sarkislider.IsEnabled = true;
        geributon.IsEnabled = true;
        ileributon.IsEnabled = true;
        oynat.IsEnabled = true;
        if (audioPlayer2 != null)
        {
            endaudio();
        }
#endif
#if ANDROID
                albmresim1.Source = g.ImageUrl;
                albmresim1.WidthRequest = 305;
                albmresim1.HeightRequest = 75;
                sarkiadi1.Text = g.Title;
                yazaradi1.Text = g.Artist;
                ileributon1.IsEnabled = true;
                oynat1.IsEnabled = true;
                if (audioPlayer2 != null)
                {
                    endaudio();
                }
#endif
            });

            
            #endregion
        }

        #region PlaybackandSeek
        private void SeekAudio(double seconds)
        {
            if (audioPlayer2 != null)
            {
                audioPlayer2.Seek(Convert.ToDouble(TimeSpan.FromSeconds(seconds)));
            }

        }
        private void InitializePlaybackTimer()
        {
            playbackTimer = new System.Timers.Timer(1000); // 1 saniyede bir tetiklenir
            playbackTimer.Elapsed += PlaybackTimer_Elapsed;
            playbackTimer.AutoReset = true;
        }
      
            private void PlaybackTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {


            Device.BeginInvokeOnMainThread(() =>
            {

                if (audioPlayer2 != null)
                {

                    try
                    {
#if WINDOWS
                      if (File.Exists(str))
{
    sarkislider.Value = audioPlayer2.CurrentPosition;
}
else
{
    sarkislider.Value = audioPlayer2.CurrentPosition;
}

#endif
#if ANDROID

#endif
                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        // Log the exception or handle it appropriately
                        Console.WriteLine($"Error getting audio position: {ex.Message}");
                    }
                }
            });
        }
#endregion
        #region Sliders
        private void OnSesSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (audioPlayer2 != null)
            {
                if (IsAudioPlaying())
                {
                    var yeniSesSeviyesi = e.NewValue;
                    Slider aas = (Slider)sender;
                    int aaas = Convert.ToInt32(aas.Value);
                    // Ses seviyesini ayarlamak için platforma özgü API'yi burada çaðýrýn
                    // Örneðin: Android için AudioManager, iOS için AVAudioSession kullanýlabilir
                    audioPlayer2.Volume = (float)aas.Value;
                }
            }


        }
        private void OnSliderValueChanged(object sender, EventArgs e)
        {
            bb = aa;
            Slider sas = (Slider)sender;
            if (bb != 0)
            {

            }
            aa = (int)sas.Value;

            if (Math.Abs(aa - bb) > 2)
            {
                if (File.Exists(str))
                {
                    audioPlayer2.Seek(Convert.ToDouble(TimeSpan.FromSeconds(aa)));
                }
                else
                {
                    audioPlayer2.Seek(Convert.ToDouble(TimeSpan.FromSeconds(aa)));
                }



            }

        }
        #endregion
        #region PlayerSettings

        AudioPlayer audioPlayer2;

        async void makeplayer(string str)
        {
         
   var audioStream = await FileSystem.OpenAppPackageFileAsync(str);
                audioPlayer2 = (AudioPlayer)AudioManager.Current.CreatePlayer(audioStream);
            
         
            
            IsExecuted();

#if WINDOWS
        audioPlayer2.Volume = (float)sesSlider.Value;
#endif
#if ANDROID

#endif
        }


        void playaudio()
        {
            if (audioPlayer2 == null || !audioPlayer2.IsPlaying)
            {
                makeplayer(str);
# if WINDOWS
      if (audioPlayer2 != null)
{
   audioPlayer2.Seek(sarkislider.Value);
}
else
{
    Debug.WriteLine("audioPlayer2 nesnesi null.");
}
#endif
#if ANDROID

#endif
            }
#if WINDOWS
    oynat.ImageSource = "a19.png";
#endif
#if ANDROID
            oynat1.Source = "a19.png";
#endif

            if (playbackTimer == null)
            {
                InitializePlaybackTimer();
            }
            playbackTimer.Start();
            isPlaying = true;
            
            audioPlayer2.Play();
        }

        void stopaudio()
        {
            audioPlayer2.Stop();
#if WINDOWS
    oynat.ImageSource = "qqq.png";
#endif
#if ANDROID
            oynat1.Source = "qqq.png";
#endif
            isPlaying = false;
            playbackTimer.Stop();
        }

        void endaudio()
        {
            playbackTimer.Close();
            audioPlayer2.Stop();
#if WINDOWS
    sarkislider.Value = 0;
    oynat.ImageSource = "qqq.png";
    sarkislider.Value = 0;
    isPlaying = false;
#endif
#if ANDROID
            oynat1.Source = "qqq.png";
#endif
        }

        void sarkiverupdate(Song nextSong)
        {
#if WINDOWS
    albmresim.Source = nextSong.ImageUrl;
    sarkiadi.Text = nextSong.Title;
    yazaradi.Text = nextSong.Artist;
#endif
#if ANDROID
            albmresim1.Source = nextSong.ImageUrl;
            sarkiadi1.Text = nextSong.Title;
            yazaradi1.Text = nextSong.Artist;
#endif
            sarii = nextSong.Title.Replace(' ', '-');
            song = nextSong;
            videon = song.Title.Replace(' ', '-');
        }

        void nextsong()
        {
            List<Song> ss = songs;
            var selectedSong = song;
            int selectedIndex = 0;
            if (selectedSong != null)
            {
                int index = 0;
                foreach (Song item in songs)
                {
                    index++;
                    if (item.Title == selectedSong.Title)
                    {
                        selectedIndex = ss.IndexOf(item);
                        index = index - 1;
                        break;
                    }
                }
                Song nextSong = null;
                if (index < ss.Count - 1)
                {
                    nextSong = songs[index + 1];
                    sarkiverupdate(nextSong);
                    str = directoryPath + videon + ".mp3";
                }
            }
        }

        void previussong()
        {
            List<Song> ss = songs;
            var selectedSong = song;
            int selectedIndex = 0;
            if (selectedSong != null)
            {
                int index = 0;
                foreach (Song item in songs)
                {
                    index++;
                    if (item.Title == selectedSong.Title)
                    {
                        selectedIndex = ss.IndexOf(item);
                        index = index - 1;
                        break;
                    }
                }
                Song nextSong = null;
                if (index > 0)
                {
                    nextSong = songs[index - 1];
                    sarkiverupdate(nextSong);
                    str = directoryPath + videon + ".mp3";
                }
            }
        }

        public async Task Playontheinternet()
        {
            try
            {
                var audioStream = await FileSystem.OpenAppPackageFileAsync(song.audioStreamInfo);
                audioPlayer2 = (AudioPlayer)AudioManager.Current.CreatePlayer(audioStream);

#if WINDOWS
        oynat.ImageSource = "a19.png";
#endif
#if ANDROID
                oynat1.Source = "a19.png";
#endif
                audioPlayer2.Play();

                // Timer'ý baþlat
                if (playbackTimer == null)
                {
                    InitializePlaybackTimer();
                }
                playbackTimer.Start();

                while (audioPlayer2.IsPlaying)
                {
                    await Task.Delay(1000);
                }

                // Timer'ý durdur
                playbackTimer.Stop();
            }
            catch (Exception ex)
            {
                // Hata iþleme
                Debug.WriteLine(ex.Message);
            }
        }


        #endregion
        #region Buttons
        private async void OnQqqButtonClicked(object sender, EventArgs e)
        {

            if (isPlaying)
            {
                stopaudio();
#if WINDOWS
                oynat.ImageSource = "qqq.png";
#endif
#if ANDROID
oynat1.Source = "qqq.png";
#endif
            }
            else
            {

                str = directoryPath +"/"+ videon + ".mp3";

                 if (File.Exists(str))
                {
                    if (audioPlayer2 == null)
                    {
                        makeplayer(str);
                        playaudio();

                    }
                    else
                    {

                        playaudio();
                    }
                }
                else
                {

                }

            }
        }
        #region eski buton
        //private async void OnQqqBucttonClicked(object sender, EventArgs e)
        //{
        //    str = directoryPath + videon + ".mp3";
        //    if (tek == 0)
        //    {
        //        if (isPlaying)
        //        {
        //            if (mf != null)
        //            {
        //                mf.Close();
        //            }

        //            sarkislider.Value = 0;
        //            audioPlayer2.Pause();
        //            oynat.ImageSource = "qqq.png";

        //            // Müzik durdurma kodu buraya eklenecek
        //        }
        //        else
        //        {
        //            string str = directoryPath + videon + ".mp3";
        //            asdfg = str;
        //            if (File.Exists(str))
        //            {

        //                sarkislider.Maximum = Convert.ToInt32(song.Length);


        //                audioPlayer2 = new WasapiOut();
        //                audioFileReader = new AudioFileReader(str);
        //                audioPlayer2.Init(audioFileReader); oynat.ImageSource = "a19.png"; audioPlayer2.Play(); audioPlayer2.Volume = (float)sesSlider.Value; sarkislider.Value = 0;





        //            }
        //            else
        //            {
        //                int sdg = Convert.ToInt32(song.Length);


        //                try
        //                {


        //                    using (mf = new MediaFoundationReader(song.audioStreamInfo))
        //                    using (audioPlayer2 = new WasapiOut())
        //                    {
        //                        oynat.ImageSource = "a19.png";
        //                        audioPlayer2.Init(mf);
        //                        audioPlayer2.Play();

        //                        // Timer'ý baþlat
        //                        if (playbackTimer == null)
        //                        {
        //                            InitializePlaybackTimer();
        //                        }
        //                        playbackTimer.Start();

        //                        while (audioPlayer2.PlaybackState == PlaybackState.Playing)
        //                        {
        //                            await Task.Delay(1000);
        //                        }

        //                        // Timer'ý durdur
        //                        playbackTimer.Stop();
        //                    }



        //                }
        //                catch (Exception ex)
        //                {
        //                    // Hata iþleme
        //                    Debug.WriteLine(ex.Message);
        //                }
        //                // sarkislider.Value = song. Length;



        //            }

        //            // Müzik çalma iþlemi

        //            // Müzik çalma kodu buraya eklenecek
        //        }
        //        isPlaying = !isPlaying;
        //    }
        //    else
        //    {
        //        tek = 0;
        //        sarkislider.Value = 0;
        //        audioPlayer2.Pause();
        //        if (mf != null)
        //        {
        //            mf.Close();
        //        }

        //        string str = directoryPath + videon.Substring(0, videon.Length - 4) + ".mp3";
        //        if (File.Exists(str))
        //        {

        //            // Dosya mevcut olduðunda yapýlacak iþlemler
        //            audioPlayer2 = new WasapiOut();
        //            audioFileReader = new AudioFileReader(str);
        //            audioPlayer2.Init(audioFileReader); oynat.ImageSource = "jjj.png"; audioPlayer2.Play(); audioPlayer2.Volume = (float)sesSlider.Value; sarkislider.Value = 0;
        //        }
        //        else
        //        {



        //            int sdg = Convert.ToInt32(song.Length);


        //            try
        //            {



        //                using (mf = new MediaFoundationReader(song.audioStreamInfo))
        //                using (audioPlayer2 = new WasapiOut())
        //                {
        //                    oynat.ImageSource = "a19.png";
        //                    audioPlayer2.Init(mf);
        //                    audioPlayer2.Play();

        //                    // Timer'ý baþlat
        //                    if (playbackTimer == null)
        //                    {
        //                        InitializePlaybackTimer();
        //                    }
        //                    playbackTimer.Start();

        //                    while (audioPlayer2.PlaybackState == PlaybackState.Playing)
        //                    {
        //                        await Task.Delay(1000);
        //                    }

        //                    // Timer'ý durdur
        //                    playbackTimer.Stop();
        //                }



        //            }
        //            catch (Exception ex)
        //            {
        //                // Hata iþleme
        //                Debug.WriteLine(ex.Message);
        //            }


        //        }
        //    }

        //}
        #endregion
        private void OnPppButtonClicked(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                endaudio();
                nextsong();

                playaudio();
                str = directoryPath + videon + ".mp3";

            }
            else
            {

                str = directoryPath + videon + ".mp3";

                if (File.Exists(str))
                {

                    nextsong();
#if WINDOWS
                    sarkislider.Value = 0;
#endif
#if ANDROID

#endif

                }
                else
                {

                }

            }

        }

        private void OnEeButtonClicked(object sender, EventArgs e)
        {
#if WINDOWS
            sesSlider.IsVisible = !sesSlider.IsVisible;
#endif
#if ANDROID

#endif
            // Slider görünürse, aralýðý artýr; deðilse, sýfýrla
            // sliderColumn.Width = sesSlider.IsVisible ? new GridLength(1, GridUnitType.Star) : new GridLength(0);

        }

        private void OnTttButtonClicked(object sender, EventArgs e)
        {
            // Event handler kodu
        }

        private void OnHhhButtonClicked(object sender, EventArgs e)
        {
            // Event handler kodu
        }
        private void OnSssButtonClicked(object sender, EventArgs e)
        {
            #region eski
            //List<Song> ss = songs;
            //var selectedSong = song;
            //int selectedIndex = 0;
            //if (selectedSong != null)
            //{
            //    int index = 0;
            //    foreach (Song item in songs)
            //    {
            //        index++;
            //        if (item.Title == selectedSong.Title)
            //        {
            //            selectedIndex = ss.IndexOf(item);
            //            index = index - 1;
            //            break;
            //            index = index - 1;
            //            int dd = ss.LastIndexOf(item);
            //        }

            //    }


            //    // Bir sonraki þarkýyý al
            //    Song nextSong = null;
            //    if (index < ss.Count - 1) // Son þarký deðilse
            //    {
            //        nextSong = songs[index - 1];

            //        albmresim.Source = Path.Combine(FileSystem.AppDataDirectory, $"{nextSong.Title.Replace(' ', '-')}.jpg");
            //        sarkiadi.Text = nextSong.Title;
            //        yazaradi.Text = nextSong.Artist;
            //        sarii = nextSong.Title.Replace(' ', '-'); ;
            //        song = nextSong;

            //        videon = song.Title.Replace(' ', '-');
            //        sarkislider.Maximum = Convert.ToDouble(song.Length);
            //        sarkislider.IsEnabled = true;
            //        geributon.IsEnabled = true;
            //        ileributon.IsEnabled = true;
            //        oynat.IsEnabled = true;

            //        if (isPlaying != null)
            //        {
            //            if (isPlaying == true)
            //            {
            //                tek = 1;
            //                OnQqqButtonClicked(song, new EventArgs());


            //            }

            //        }
            //        else
            //        {
            //            audioPlayer2.Pause();
            //        }
            //    }

            //    // Bir önceki þarkýyý al


            //    // Bir sonraki ve bir önceki þarký ile ilgili iþlemler
            //    // Örneðin, bilgilerini yazdýrabilirsiniz


            //}
            #endregion
            if (isPlaying)
            {
                endaudio();
                previussong();

                playaudio();
                str = directoryPath + videon + ".mp3";

            }
            else
            {

                str = directoryPath + videon + ".mp3";

                if (File.Exists(str))
                {

                    previussong();
#if WINDOWS
                    sarkislider.Value = 0;
#endif
#if ANDROID

#endif

                }
                else
                {

                }

            }
        }
#endregion
        
        #region controls
        private bool IsAudioPlaying()
        {
            return audioPlayer2 != null && audioPlayer2.IsPlaying;
        }
        void IsExecuted()
        {
            isExecuted = true;
        }
        #endregion
        #region DownloadVideoorImage
        private async Task DownloadVideoAsync(string videoUrl)
        {
#if WINDOWS
   directoryPath = "C:\\ProgramData\\Seazer Software\\Seazer Music\\";
#elif ANDROID
            directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));

#endif
            try
            {

                videon = (song.Title + ".mp3").Replace(' ', '-');
                var client = new HttpClient();
                long? totalByte = 0;

                string filePath;
                var platform = Microsoft.Maui.Devices.DeviceInfo.Platform; // Platform bilgisini güncellenmiþ API ile alýn
                if (platform == Microsoft.Maui.Devices.DevicePlatform.Android)
                {
                    // Android için dosya yolu
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), videon);
                }
                else
                {
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    filePath = Path.Combine(directoryPath, videon);
                }

                using (var output = File.OpenWrite(filePath))
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Head, song.YouTubeLink))
                    {
                        var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        totalByte = response.Content.Headers.ContentLength;
                    }

                    using (var input = await client.GetStreamAsync(song.YouTubeLink))
                    {
                        byte[] buffer = new byte[16 * 1024]; // 16KB buffer
                        int read;
                        long totalRead = 0;
                        while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, read);
                            totalRead += read;
                            var progress = totalRead * 100 / totalByte;
                            // UI güncellemeleri için Dispatcher kullanýn
                            Dispatcher.Dispatch(() => Debug.WriteLine($"Downloading {progress}% ..."));
                        }
                        Dispatcher.Dispatch(() => Debug.WriteLine("Download Complete"));



                        var audioStream = await FileSystem.OpenAppPackageFileAsync(directoryPath + videon.Substring(0, videon.Length - 4) + ".mp3");
                        audioPlayer2 = (AudioPlayer)AudioManager.Current.CreatePlayer(audioStream);



#if WINDOWS
                        oynat.ImageSource = "a19.png"; audioPlayer2.Volume = (float)sesSlider.Value;
#endif
#if ANDROID
                        oynat1.Source = "a19.png"; 
#endif
                        audioPlayer2.Play();
                    }
                }

                // Platform kontrolü ve dosya dönüþtürme iþlemleri
                if (platform == Microsoft.Maui.Devices.DevicePlatform.Android)
                {
                    // await ConvertVideoToAudioAsync(filePath); // await ekleyerek asenkron çaðrýyý bekleyin
                }
                else
                {
                    var convertVideo = new NReco.VideoConverter.FFMpegConverter();
                    convertVideo.ConvertMedia(filePath, filePath.Replace(".mp4", ".mp3"), "mp3");
                }

                File.Delete(filePath); // Orijinal video dosyasýný dönüþtürme sonrasý silin
            }
            catch (Exception ex)
            {
                Dispatcher.Dispatch(() => Debug.WriteLine($"Download error: {ex.Message}"));
            }
        }
#endregion
    }
}
