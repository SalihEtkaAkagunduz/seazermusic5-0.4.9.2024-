using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Net.Http;
using System.Threading.Tasks;
using Laerdal;
using Laerdal.FFmpeg;
using Microsoft.Maui.Controls;
using VideoLibrary;
using Plugin;
 
using NReco.VideoConverter;
using System.Diagnostics;
using The49.Maui.BottomSheet;
using Plugin.Maui.Audio;
using FirebaseMedium;
using seazermusic5.Views;



namespace seazermusic5
{
    public partial class kayıtlısarki : ContentPage
    {
        //альтернативный вариант
        Crud crud; private NotificationService _notificationService;
        string directoryPath = "";

        Dictionary<string, Song> ff;
        public kayıtlısarki()
        {
            InitializeComponent();
#if WINDOWS
   directoryPath = "C:\\ProgramData\\Seazer Software\\Seazer Music\\";
#elif ANDROID
            directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));

#endif
            crud = new Crud();



            ff = crud.LoadDataa();
            if (songs != null)
            {
                songs = new List<Song>();
            }
            else
            {
                songs = new List<Song>();
                songs = ff.Values.ToList();
            }
            GetSongsAsync();

            MessagingCenter.Subscribe<kayıtlısarki, string>(this, "DisplayAlert", async (sender, arg) =>
            {
                await DisplayAlert("Müzik İndirili durumda değil indiriliyor...", arg, "Tamam");
            });
            _notificationService = new NotificationService();

        }
        private void OnShowNotificationClicked(object sender, EventArgs e)
        {

        }

        private void OnUpdateNotificationClicked(object sender, EventArgs e)
        {
            _notificationService.UpdateNotification("Yeni Başlık", "Yeni Mesaj", "https://example.com/newimage.jpg");
        }

        List<Song> songs;
        private async Task<List<Song>> GetSongsAsync()
        {
            String aasf = "";
            List<Song> songsss = songs;
            for (int i = 0; i < songsss.Count; i++)
            {
                try
                {
                    if (Convert.ToInt32(songsss[i].Length) <= 3600)
                    {
                        aasf = $"{Math.Round((Double)Convert.ToInt32(songsss[i].Length) / 60)}:{((Convert.ToInt32(songsss[i].Length) % 60) < 10 ? "0" + (Convert.ToInt32(songsss[i].Length) % 60) : (Convert.ToInt32(songsss[i].Length) % 60))}";
                    }
                    else
                    {
                        aasf = $"{Math.Round((Double)Convert.ToInt32(songsss[i].Length) / 3600)}:{Math.Round((Double)Convert.ToInt32(songsss[i].Length) / 60) - (Math.Round((Double)Convert.ToInt32(songsss[i].Length) / 3600) * 60)}:{((Convert.ToInt32(songsss[i].Length) % 3600) < 10 ? "0" + (Convert.ToInt32(songsss[i].Length) % 60) : (Convert.ToInt32(songsss[i].Length) % 60))}";
                    }

                    songsss[i].Length = aasf;
                    songsss[i].Tag = songsss[i].ImageUrl;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(songsss[i].Length);
                }

            }

#if ANDROID
 
 
 
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;


            SongsCollectionView.ItemsSource = songsss;

            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
#endif
#if WINDOWS
            LoadingIndicator1.IsRunning = true;
             LoadingIndicator1.IsVisible = true;
             SongsCollectionView1.ItemsSource = songsss;
             LoadingIndicator1.IsRunning = false;
            LoadingIndicator1.IsVisible = false;
#endif
            MessagingCenter.Send<kayıtlısarki, List<Song>>(this, "Songmessage", songsss);
            return songsss;
        }


        private async void OnMoreOptionsClicked(object sender, EventArgs e)
        {
#if WINDOWS
var button = (  Button)sender;
            var action = await DisplayActionSheet("Seçenekler", "İptal", null, "Paylaş", "Listeye Ekle", "İndir","Listeden Sil","Cihazdan Sil");

            if (action == "İndir")
            {
                // await DownloadVideoAsync(songs[sender.ta]);
                var buttonm = (Button)sender;
                Song item = (Song)buttonm.BindingContext;
                ;
                videon = item.Title.Replace(' ', '-')+".mp3";
                if (File.Exists(directoryPath + videon))
                {
                 
                    await DisplayAlert("Uyarı", "Bu müzik zaten indirili", "Tamam");
                }
                else
                {
                    await DownloadVideoAsync(item.YouTubeLink);
             }   
                
            }
            else if (action == "Listeden Sil")
            {
                // await DownloadVideoAsync(songs[sender.ta]);
                var buttonm = (Button)sender;
                Song item = (Song)buttonm.BindingContext;
                 
                   
                   
                    crud.DeleteTeam(item.Title);
                     await DisplayAlert("Uyarı", "Listeden silme işlemi başarılı", "Tamam");
                
               
                
 

            }
            else if (action == "Cihazdan Sil")
            {
                // await DownloadVideoAsync(songs[sender.ta]);
                var buttonm = (Button)sender;
                Song item = (Song)buttonm.BindingContext;
                var youtube = YouTube.Default;
                var video = youtube.GetVideo(item.YouTubeLink);
                videon = video.Title.Replace(' ', '-') + ".mp3";
                if (File.Exists(directoryPath + videon))
                {


                    
                    File.Delete(directoryPath + videon); await DisplayAlert("Uyarı", "Cihazdan silme işlemi başarılı", "Tamam");
                }
                else
                {
                    await DisplayAlert("Uyarı", "Bu müzik zaten indirili değil", "Tamam");
                }



            }
            else if (action == "Paylaş")
            {
                // Paylaş işlemini burada gerçekleştirin
            }
            else if (action == "Listeye Ekle")
            {
                // Listeye ekleme işlemini burada gerçekleştirin
            }
#elif ANDROID
            var button = (ImageButton)sender;
            var action = await DisplayActionSheet("Seçenekler", "İptal", null, "Paylaş", "Listeye Ekle", "İndir", "Listeden Sil", "Cihazdan Sil");

            if (action == "İndir")
            {
                // await DownloadVideoAsync(songs[sender.ta]);
                var buttonm = (ImageButton)sender;
                Song item = (Song)buttonm.BindingContext;
                ;
                videon = item.Title.Replace(' ', '-') + ".mp3";
                if (File.Exists(directoryPath + videon))
                {

                    await DisplayAlert("Uyarı", "Bu müzik zaten indirili", "Tamam");
                }
                else
                {
                    await DownloadVideoAsync(item.YouTubeLink);
                }

            }
            else if (action == "Listeden Sil")
            {
                // await DownloadVideoAsync(songs[sender.ta]);
                var buttonm = (ImageButton)sender;
                Song item = (Song)buttonm.BindingContext;



                crud.DeleteTeam(item.Title);
                await DisplayAlert("Uyarı", "Listeden silme işlemi başarılı", "Tamam");





            }
            else if (action == "Cihazdan Sil")
            {
                // await DownloadVideoAsync(songs[sender.ta]);
                var buttonm = (ImageButton)sender;
                Song item = (Song)buttonm.BindingContext;
                var youtube = YouTube.Default;
                var video = youtube.GetVideo(item.YouTubeLink);
                videon = video.Title.Replace(' ', '-') + ".mp3";
                if (File.Exists(directoryPath + videon))
                {



                    File.Delete(directoryPath + videon); await DisplayAlert("Uyarı", "Cihazdan silme işlemi başarılı", "Tamam");
                }
                else
                {
                    await DisplayAlert("Uyarı", "Bu müzik zaten indirili değil", "Tamam");
                }



            }
            else if (action == "Paylaş")
            {
                // Paylaş işlemini burada gerçekleştirin
            }
            else if (action == "Listeye Ekle")
            {
                // Listeye ekleme işlemini burada gerçekleştirin
            }
#endif

        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Verileri yenileme işlemini burada gerçekleştirin
            //await GetSongsAsync();
        }
        string videon;

        private async Task DownloadVideoAsync(string videoUrl)
        {
            try
            {
                var youtube = YouTube.Default;
                var video = youtube.GetVideo(videoUrl);
                videon = video.FullName.Replace(' ', '-');
                var client = new HttpClient();
                long? totalByte = 0;

                string filePath;
                var platform = Microsoft.Maui.Devices.DeviceInfo.Platform;
                if (platform == Microsoft.Maui.Devices.DevicePlatform.Android)
                {
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), videon);
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
                    using (var request = new HttpRequestMessage(HttpMethod.Head, video.Uri))
                    {
                        var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        totalByte = response.Content.Headers.ContentLength;
                    }

                    using (var input = await client.GetStreamAsync(video.Uri))
                    {
                        byte[] buffer = new byte[16 * 1024];
                        int read;
                        long totalRead = 0;
                        while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, read);
                            totalRead += read;
                            var progress = totalByte.HasValue ? (totalRead * 100 / totalByte.Value) : -1;
                            Dispatcher.Dispatch(() => Debug.WriteLine(progress >= 0 ? $"Downloading {progress}% ..." : $"Downloading... {totalRead} bytes read"));
                        }
                        Dispatcher.Dispatch(() => Debug.WriteLine("Download Complete"));
                    }
                }

                if (platform == Microsoft.Maui.Devices.DevicePlatform.Android)
                {
                    try
                    {
                        await ConvertVideoToAudioAsync(filePath);
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Dispatch(() => Debug.WriteLine($"Conversion error: {ex.Message}"));
                    }
                }
                else
                {
                    var convertVideo = new NReco.VideoConverter.FFMpegConverter();
                    convertVideo.ConvertMedia(filePath, filePath.Replace(".mp4", ".mp3"), "mp3");
                }

                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Dispatcher.Dispatch(() => Debug.WriteLine($"Download error: {ex.Message}"));
            }
        }



        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSong = e.CurrentSelection.FirstOrDefault() as Song;
            if (selectedSong != null)
            {
                int selectedIndex = songs.IndexOf(selectedSong); // Örnek bir int değerd
                MessagingCenter.Send(this, "integerMessage", selectedIndex);


                // Seçili şarkının indeksini bul


                // Bir sonraki şarkıyı al
                Song nextSong = null;
                if (selectedIndex < songs.Count - 1) // Son şarkı değilse
                {
                    nextSong = songs[selectedIndex + 1];
                }

                // Bir önceki şarkıyı al
                Song previousSong = null;
                if (selectedIndex > 0) // İlk şarkı değilse
                {
                    previousSong = songs[selectedIndex - 1];
                }

                // Bir sonraki ve bir önceki şarkı ile ilgili işlemler
                // Örneğin, bilgilerini yazdırabilirsiniz
                if (nextSong != null)
                {
                    Debug.WriteLine($"Bir sonraki şarkı: {nextSong.Title}");
                }
                if (previousSong != null)
                {
                    Debug.WriteLine($"Bir önceki şarkı: {previousSong.Title}");
                }
            }


        }

        private async Task ConvertVideoToAudioAsync(string filePath)
        {
            try
            {
                string a = Path.ChangeExtension(filePath, ".mp4");
                string b = Path.ChangeExtension(filePath, ".mp3");

                int status = await Task.Run(() => FFmpeg.Execute($"-i \"{a}\" \"{b}\""));
                if (status == 0)
                {
                    MainThread.BeginInvokeOnMainThread(() => Debug.WriteLine("Success"));
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => Debug.WriteLine($"FFmpeg failed with status code {status}"));
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Debug.WriteLine("An error occurred:");
                    Debug.WriteLine(ex.ToString());
                });
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var sheet = new bottomsheet();

           
            // Show the sheet
            sheet.ShowAsync();
        }
    }

    public class Song
    {
        public string?  audioStreamInfo
        {
            get; set;
        }
        public string? ImageUrl
        {
            get; set;
        }
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Single { get; set; }
        public string? Length { get; set; }
        
        public String Tag { get; set; }
        public string? YouTubeLink { get; set; }
    }
    public class Song5
    {
        public string? audioStreamInfo1
        {
            get; set;
        }
        public string? ImageUrl1
        {
            get; set;
        }
        public string? Title1
        {
            get; set;
        }
        public string? Artist1
        {
            get; set;
        }
        public string? Single1
        {
            get; set;
        }
        public string? Length1
        {
            get; set;
        }

        public String Tag1
        {
            get; set;
        }
        public string? YouTubeLink1
        {
            get; set;
        }
        public string? audioStreamInfo2
        {
            get; set;
        }
        public string? ImageUrl2
        {
            get; set;
        }
        public string? Title2
        {
            get; set;
        }
        public string? Artist2
        {
            get; set;
        }
        public string? Single2
        {
            get; set;
        }
        public string? Length2
        {
            get; set;
        }
        public String Tag2
        {
            get; set;
        }
        public string? YouTubeLink2
        {
            get; set;
        }
        public string? audioStreamInfo3
        {
            get; set;
        }
        public string? ImageUrl3
        {
            get; set;
        }
        public string? Title3
        {
            get; set;
        }
        public string? Artist3
        {
            get; set;
        }
        public string? Single3
        {
            get; set;
        }
        public string? Length3
        {
            get; set;
        }
        public String Tag3
        {
            get; set;
        }
        public string? YouTubeLink3
        {
            get; set;
        }
        public string? audioStreamInfo4
        {
            get; set;
        }
        public string? ImageUrl4
        {
            get; set;
        }
        public string? Title4
        {
            get; set;
        }
        public string? Artist4
        {
            get; set;
        }
        public string? Single4
        {
            get; set;
        }
        public string? Length4
        {
            get; set;
        }
        public String Tag4
        {
            get; set;
        }
        public string? YouTubeLink4
        {
            get; set;
        }
        public string? audioStreamInfo5
        {
            get; set;
        }
        public string? ImageUrl5
        {
            get; set;
        }
        public string? Title5
        {
            get; set;
        }
        public string? Artist5
        {
            get; set;
        }
        public string? Single5
        {
            get; set;
        }
        public string? Length5
        {
            get; set;
        }
        public String Tag5
        {
            get; set;
        }
        public string? YouTubeLink5
        {
            get; set;
        }
    }
    public class Song2
    {
        public string? audioStreamInfo
        {
            get; set;
        }
        public string? ImageUrl
        {
            get; set;
        }
        public string? Title
        {
            get; set;
        }
        public string? Artist
        {
            get; set;
        }
        public string? Single
        {
            get; set;
        }
        public string? Length
        {
            get; set;
        }

        public String Tag
        {
            get; set;
        }
        public string? YouTubeLink
        {
            get; set;
        }
        public Boolean varmi
        {
            get; set;
        }
    }
}
