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

using Plugin.Maui.Audio;
using FirebaseMedium;


namespace seazermusic5;

public partial class indirilenler : ContentPage
{
    Crud crud;
    Dictionary<string, Song> ff;
    public indirilenler()
	{
		InitializeComponent(); crud = new Crud();

#if WINDOWS
   directoryPath = "C:\\ProgramData\\Seazer Software\\Seazer Music\\";
#elif ANDROID
        directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

#endif

        ff = crud.LoadDataa();
        songs = ff.Values.ToList();
        LoadSongsAsync();
	}
    private async Task LoadSongsAsync()
    {    var songs = await GetSongsAsync();
#if ANDROID
LoadingIndicator1.IsRunning = true;
        LoadingIndicator1.IsVisible = true; LoadingIndicator1.IsRunning = false;
        LoadingIndicator1.IsVisible = false;SongsCollectionView1.ItemsSource = songs;
#elif WINDOWS
LoadingIndicator.IsRunning = true;
        LoadingIndicator.IsVisible = true; LoadingIndicator.IsRunning = false;
        LoadingIndicator.IsVisible = false;SongsCollectionView.ItemsSource = songs;
#endif
        

    
        

       
    }
    private async Task<List<Song>> GetSongsAsync()
    {
        List<Song> songss = new List<Song>();
#if WINDOWS
   directoryPath = "C:\\ProgramData\\Seazer Software\\Seazer Music\\";
#elif ANDROID
        directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

#endif
        for (int i = 0; i < songs.Count; i++)
        {
            String ssd = directoryPath +"/"+ songs[i].Title.Replace(' ', '-') + ".mp3";
            if (File.Exists(ssd)){
                   songss.Add(songs[i]); songs[i].Tag =  songs[i].ImageUrl ;
            }
         
        }

        return songss;
    }
     
    List<Song> songs;
    private async void OnMoreOptionsClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var action = await DisplayActionSheet("Seçenekler", "Ýptal", null,  "Dosya Bilgileri", "Cihazdan Sil");

        
       
          if (action == "Cihazdan Sil")
        {
            // await DownloadVideoAsync(songs[sender.ta]);
            var buttonm = (Button)sender;
            Song item = (Song)buttonm.BindingContext;
            var youtube = YouTube.Default;
            var video = youtube.GetVideo(item.YouTubeLink);
            videon = video.Title.Replace(' ', '-') + ".mp3";
            if (File.Exists(directoryPath + videon))
            {



                File.Delete(directoryPath + videon); await DisplayAlert("Uyarý", "Cihazdan silme iþlemi baþarýlý", "Tamam");
            }
            else
            {
                await DisplayAlert("Uyarý", "Bu müzik zaten indirili deðil", "Tamam");
            }



        }
         
    }
    string directoryPath  ;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Verileri yenileme iþlemini burada gerçekleþtirin
        await LoadSongsAsync();
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
            var platform = Microsoft.Maui.Devices.DeviceInfo.Platform; // Platform bilgisini güncellenmiþ API ile alýn
            if (platform == Microsoft.Maui.Devices.DevicePlatform.Android)
            {
                // Android için dosya yolu
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
    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedSong = e.CurrentSelection.FirstOrDefault() as Song;
        if (selectedSong != null)
        {
            int selectedIndex = songs.IndexOf(selectedSong); // Örnek bir int deðer
            MessagingCenter.Send(this, "integerMessage", selectedIndex);


            // Seçili þarkýnýn indeksini bul


            // Bir sonraki þarkýyý al
            Song nextSong = null;
            if (selectedIndex < songs.Count - 1) // Son þarký deðilse
            {
                nextSong = songs[selectedIndex + 1];
            }

            // Bir önceki þarkýyý al
            Song previousSong = null;
            if (selectedIndex > 0) // Ýlk þarký deðilse
            {
                previousSong = songs[selectedIndex - 1];
            }

            // Bir sonraki ve bir önceki þarký ile ilgili iþlemler
            // Örneðin, bilgilerini yazdýrabilirsiniz
            if (nextSong != null)
            {
                Debug.WriteLine($"Bir sonraki þarký: {nextSong.Title}");
            }
            if (previousSong != null)
            {
                Debug.WriteLine($"Bir önceki þarký: {previousSong.Title}");
            }
        }


    }

    private async Task ConvertVideoToAudioAsync(string filePath)
    {
        try
        {
            string a = filePath.Substring(0, filePath.Length - 4) + ".mp4";
            string b = filePath.Substring(0, filePath.Length - 4) + ".mp3";
            int status = await Task.Run(() => FFmpeg.Execute($"-i {a} {b}"));
            if (status == 0)
            {
                Dispatcher.Dispatch(() => Debug.WriteLine("Success"));
            }
            else
            {
                Dispatcher.Dispatch(() => Debug.WriteLine($"FFmpeg failed with status code {status}"));
            }
        }
        catch (Exception e)
        {
            Dispatcher.Dispatch(() => Debug.WriteLine(e.Message.ToString()));
        }
    }
}