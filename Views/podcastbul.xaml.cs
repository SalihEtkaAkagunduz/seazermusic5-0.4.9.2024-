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
using iTunesPodcastFinder.Models;
using iTunesPodcastFinder;

namespace seazermusic5;

public partial class podcastbul : ContentPage
{
   
    connection cc; PodcastFinder finder = new PodcastFinder();
    private readonly YoutubeClient youtubeClient;
    public ObservableCollection<VideoItem> VideoItems { get; set; } = new ObservableCollection<VideoItem>();
    public podcastbul()
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
        try
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue)) return;
#if WINDOWS
loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
#elif ANDROID
loadingIndicator1.IsRunning = true;
            loadingIndicator1.IsVisible = true;
#endif
            

            await Task.Run(async () =>
            {
                var results = await finder.SearchPodcastsAsync(e.NewTextValue, 10);

                Device.BeginInvokeOnMainThread(() =>
                {
                    VideoItems.Clear();
                    foreach (var podcast in results)
                    {
                        // Thumbnail URL'sini almak için bir yöntem seçin
                        var thumbnailUrl = podcast.ArtWork; // Doðru özelliði kullanýn
                        
                        // Podcast URL'sini oluþturun
                        var podcastUrl = podcast.FeedUrl;
                   
                        VideoItems.Add(new VideoItem
                        {
                            Title = podcast.Name,
                            Author = podcast.Editor,
                            Thumbnail = thumbnailUrl, // Burada seçilen thumbnail URL'sini kullanýn
                            Url = podcastUrl // Podcast URL'sini burada ayarlayýn
                        });
                    }
 
 
                   
                });
            });
        }
        
        catch
        {

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

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedPodcast = e.CurrentSelection.FirstOrDefault() as VideoItem;
        if (selectedPodcast == null) return;

        var rssUrl = selectedPodcast.Url;

        // podcast.xaml.cs dosyasýný aç
        await Navigation.PushAsync(new podcast(rssUrl));
    }
    private async void OnPlayClicked(object sender, EventArgs e)
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
           
        }

        MessagingCenter.Send<podcastbul, string>(this, "strm", videoUrl);
        // URL'yi kullanarak istediðiniz iþlemi yapýn
        // Örneðin, bir mesaj gösterin veya videoyu oynatýn


        // Ýsteðe baðlý olarak, MessagingCenter aracýlýðýyla URL'yi gönderin


    }

    private void OnDownloadClicked(object sender, EventArgs e)
    {
        DisplayAlert("Alarm", "Bu bir alarm mesajýdýr!", "Tamam");

    }
} 