using System.Collections.ObjectModel;
using YoutubeExplode;
using System.Collections.ObjectModel;
using System.Linq;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using VideoLibrary;
using YoutubeExplode.Videos;
using FirebaseMedium;
using FireSharp;
using FireSharp.Response;

using System.Diagnostics;
using YoutubeExplode.Common;
namespace seazermusic5;

public partial class listebul : ContentPage
{
   
    connection cc;
    private readonly YoutubeClient youtubeClient;
    public ObservableCollection<VideoItem> VideoItems { get; set; } = new ObservableCollection<VideoItem>(); public listebul()
    {
        InitializeComponent(); cc = new connection();  
        youtubeClient = new YoutubeClient();
#if WINDOWS
 songsCollectionView.ItemsSource = VideoItems;
#elif ANDROID
 songsListView1.ItemsSource = VideoItems;
#endif


    }
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue)) return;

        var videos = await youtubeClient.Search.GetPlaylistsAsync(e.NewTextValue).Take(10).ToListAsync();

        VideoItems.Clear();
        foreach (var video in videos)
        {
            // Thumbnail URL'sini almak için bir yöntem seçin
            var thumbnailUrl = video.Thumbnails.OrderByDescending(t => t.Resolution.Area).FirstOrDefault()?.Url;

            // Video URL'sini oluþturun
            var videoUrl = $"https://www.youtube.com/playlist?list={video.Id}";


            VideoItems.Add(new VideoItem
            {
                Title = video.Title,
                Author = video.Author.ChannelTitle,
                Thumbnail = thumbnailUrl, // Burada seçilen thumbnail URL'sini kullanýn
                Url = videoUrl // Video URL'sini burada ayarlayýn
            });
            string mesaj = "Bu bir test mesajýdýr.";


        }
    }
    public async Task<bool> IsTitleExists(string title)
    {
        // "songs" düðümü altýnda "Title" özelliðine göre sorgulama yapýn
        // Bu örnekte, "songs" verilerinizi sakladýðýnýz düðümün adýdýr
        FirebaseResponse response = await cc.client.GetAsync("users/SalihDeneme/Lists/List1/Songs");
        if (response.Body != "null")
        {
            Dictionary<string, Song> songs = response.ResultAs<Dictionary<string, Song>>();
            foreach (var song in songs)
            {
                if (song.Value.Title == title)
                {
                    // Eðer Title zaten varsa, true dön
                    return true;
                }
            }
        }
        // Eðer Title bulunamazsa, false dön
        return false;
    }


    private async void OnPlauyClicked(object sender, EventArgs e)
    {
        // 'sender' nesnesini Button olarak kabul edin
        var button = sender as Button;
        if (button == null) return;

        // Button'un baðlý olduðu VideoItem nesnesini alýn
        var videoItem = button.BindingContext as VideoItem;
        if (videoItem == null) return;

        // VideoItem'dan YouTube video URL'sini alýn
        var videoUrl = videoItem.Url;
        Song s = new Song();
        var youtube = YouTube.Default;
        var video = youtube.GetVideo(videoUrl);
        s.Title = video.Title;
        s.Artist = video.Info.Author;



















        var youtubeS = new YoutubeClient();

        // Video bilgilerini al
        var videoS = await youtubeS.Videos.GetAsync(videoUrl);

        // Ses akýþýný al
        var streamManifest = await youtubeS.Videos.Streams.GetManifestAsync(videoS.Id);
        var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
        s.audioStreamInfo = audioStreamInfo.Url;

        s.YouTubeLink = videoUrl;
        var videor = await youtubeClient.Videos.GetAsync(videoUrl);
        var aad = videor.Thumbnails.GetWithHighestResolution()?.Url;
        s.ImageUrl = aad;
        s.Length = video.Info.LengthSeconds.ToString();
        s.Single = "Single";

        if (await IsTitleExists(s.Title))
        {
            DisplayAlert("Uyarý", "Bu video zaten listenize kayýtlý", "Tamam");
        }
        else
        {
            // Eðer Title yoksa, veriyi ekleyin
            DisplayAlert("Ýþlem tamamlandý", "Þarký listenize kaydedildi", "Tamam");
            
        }

        MessagingCenter.Send<listebul, string>(this, "strm", videoUrl);
        // URL'yi kullanarak istediðiniz iþlemi yapýn
        // Örneðin, bir mesaj gösterin veya videoyu oynatýn


        // Ýsteðe baðlý olarak, MessagingCenter aracýlýðýyla URL'yi gönderin


    }
    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedVideoItem = e.CurrentSelection.FirstOrDefault() as VideoItem;
        if (selectedVideoItem == null) return;

        var playlistUrl = selectedVideoItem.Url;

        // Playlist'teki videolarý alýn
        var playlist = await youtubeClient.Playlists.GetVideosAsync(playlistUrl);
        var songs = new Dictionary<string, Song>();

        foreach (var video in playlist)
        {
          

            var song = new Song
            {
                Title = video.Title,
                Artist = video.Author.ChannelTitle,
                ImageUrl = video.Thumbnails.GetWithHighestResolution()?.Url,
                Length = video.Duration?.ToString(),
                YouTubeLink = $"https://www.youtube.com/watch?v={video.Id}",
                audioStreamInfo ="empty",
                Single = "Single"
            };

            songs.Add(video.Id, song);
        }

        var listt = new Listt
        {
            Name = selectedVideoItem.Title,
            Description = "Playlist Description", // Ýsteðe baðlý olarak açýklama ekleyin
            ImageUrl = selectedVideoItem.Thumbnail,
            Songs = songs
        };

        // Ýsteðe baðlý olarak, MessagingCenter aracýlýðýyla Listt nesnesini gönderin
       

        // podcast.xaml.cs dosyasýný aç
        await Navigation.PushAsync(new listeekrani(listt));
    }

    private void OnDownloadClicked(object sender, EventArgs e)
    {
        DisplayAlert("Alarm", "Bu bir alarm mesajýdýr!", "Tamam");

    }

}

 

//using System.Collections.ObjectModel;
//using System.Linq;
//using YoutubeExplode;
//using YoutubeExplode.Videos.Streams;

//public partial class sarkibul : ContentPage
//{
//    private readonly YoutubeClient youtubeClient;
//    public ObservableCollection<VideoItem> VideoItems { get; set; } = new ObservableCollection<VideoItem>();

//    public sarkibul()
//    {
//        InitializeComponent();
//        youtubeClient = new YoutubeClient();
//        songsCollectionView.ItemsSource = VideoItems;
//    }

//    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
//    {
//        if (string.IsNullOrWhiteSpace(e.NewTextValue)) return;

//        var videos = await youtubeClient.Search.GetVideosAsync(e.NewTextValue).Take(10).ToListAsync();

//        VideoItems.Clear();
//        foreach (var video in videos)
//        {
//            // Thumbnail URL'sini almak için bir yöntem seçin
//            var thumbnailUrl = video.Thumbnails.OrderByDescending(t => t.Resolution.Area).FirstOrDefault()?.Url;

//            VideoItems.Add(new VideoItem
//            {
//                Title = video.Title,
//                Author = video.Author.ChannelTitle,
//                Thumbnail = thumbnailUrl // Burada seçilen thumbnail URL'sini kullanýn
//            });
//        }
//    }


//    private void OnPlayClicked(object sender, EventArgs e)
//    {
//        // Oynatma iþlevselliði burada
//    }

//    private void OnDownloadClicked(object sender, EventArgs e)
//    {
//        // Ýndirme iþlevselliði burada
//    }
//}

//public class VideoItem
//{
//    public string Title { get; set; }
//    public string Author { get; set; }
//    public string Thumbnail { get; set; }
//}