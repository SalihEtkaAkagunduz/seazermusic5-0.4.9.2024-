 
using FirebaseMedium;
using FireSharp.Response;
using iTunesPodcastFinder;
using System.Collections.ObjectModel;
using System.Drawing;
using VideoLibrary;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos.Streams;

namespace seazermusic5;

public partial class ogebul : ContentPage
{
    Crud ccc;   PodcastFinder finder = new PodcastFinder();
    connection cc;public ObservableCollection<VideoItem2> VideoItems2 { get; set; } = new ObservableCollection<VideoItem2>();
    private readonly YoutubeClient youtubeClient;public ObservableCollection<VideoItem> VideoItems { get; set; } = new ObservableCollection<VideoItem>();
    public ObservableCollection<VideoItem3> VideoItems3 { get; set; } = new ObservableCollection<VideoItem3>();
    public ogebul() 
	{
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
        youtubeClient = new YoutubeClient();
#if WINDOWS

             songsListView2.ItemsSource=VideoItems2;
 songsListView1.ItemsSource = VideoItems;
 
#elif ANDROID
        songsListView1.ItemsSource = VideoItems;songsListView2.ItemsSource=VideoItems2;songsListView3.ItemsSource=VideoItems3;Shell.SetNavBarIsVisible(this, false);
#endif
    }
    public async Task  textchan( String txt)
    {
        if (string.IsNullOrWhiteSpace(txt)) return;

#if ANDROID
    // Göstergeyi baþlat
    lbl1.IsVisible = true;
    lbl2.IsVisible = true;
    lbl3.IsVisible = true; 
    songsListView1.ItemsSource = null;
    songsListView2.ItemsSource = null;
    songsListView3.ItemsSource = null;
    loadingIndicator1a.IsRunning = true;
    loadingIndicator1a.IsVisible = true;
    loadingIndicator2.IsRunning = true;
    loadingIndicator2.IsVisible = true;
    loadingIndicator3.IsRunning = true;
    loadingIndicator3.IsVisible = true;
#endif
#if ANDROID || WINDOWS
        var searchText = txt;    //var task1 = Task.Run(async () =>
        //{
        //    var videos2 = await youtubeClient.Search.GetPlaylistsAsync(searchText).Take(10).ToListAsync();
           
        //        VideoItems2.Clear();
        //        foreach (var video2 in videos2)
        //        {
        //            var thumbnailUrl = video2.Thumbnails.OrderByDescending(t => t.Resolution.Area).FirstOrDefault()?.Url;
        //            var videoUrl = $"https://www.youtube.com/playlist?list={video2.Id}";

        //            VideoItems2.Add(new VideoItem2
        //            {
        //                Title = video2.Title,
        //                Author = video2.Author.ChannelTitle,
        //                Thumbnail = thumbnailUrl,
        //                Url = videoUrl
        //            });
        //              loadingIndicator2.IsRunning = false;
        //                loadingIndicator2.IsVisible = false;
        //        }
             
        //        songsListView2.ItemsSource = VideoItems2;
        //        songsListView2.IsVisible = true;
        //});
        var task1 = Task.Run(async () =>
        {
            try
            {
                var videos2 = await youtubeClient.Search.GetPlaylistsAsync(searchText).Take(10).ToListAsync();

                VideoItems2.Clear();
                foreach (var video2 in videos2)
                {
                    var thumbnailUrl = video2.Thumbnails.OrderByDescending(t => t.Resolution.Area).FirstOrDefault()?.Url;
                    var videoUrl = $"https://www.youtube.com/playlist?list={video2.Id}";

                    VideoItems2.Add(new VideoItem2
                    {
                        Title = video2.Title,
                        Author = video2.Author.ChannelTitle,
                        Thumbnail = thumbnailUrl,
                        Url = videoUrl
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    loadingIndicator2.IsRunning = false;
                    loadingIndicator2.IsVisible = false;
                    songsListView2.ItemsSource = VideoItems2;
                    songsListView2.IsVisible = true;
                });
            }
            catch (Exception ex)
            {
                // Hata iþleme
                await DisplayAlert("Hata", $"Bir hata oluþtu: {ex.Message}", "Tamam");
            }
        });
    

        var task2 = Task.Run(async () =>
        {
            var videos = await youtubeClient.Search.GetVideosAsync(searchText).Take(10).ToListAsync();
            var tasks = videos.Select(async video =>
            {
                var thumbnailUrl = video.Thumbnails.OrderByDescending(t => t.Resolution.Area).FirstOrDefault()?.Url;
                var videoUrl = $"https://www.youtube.com/watch?v={video.Id}";

                return new VideoItem
                {
                    Title = video.Title,
                    Author = video.Author.ChannelTitle,
                    Thumbnail = thumbnailUrl,
                    Url = videoUrl
                };
            });

            var videoItems = await Task.WhenAll(tasks);
            Device.BeginInvokeOnMainThread(() =>
            {
                VideoItems.Clear();
                foreach (var item in videoItems)
                {
                    VideoItems.Add(item);
                }
                songsListView1.ItemsSource = VideoItems;
                songsListView1.IsVisible = true;loadingIndicator1a.IsRunning = false;
    loadingIndicator1a.IsVisible = false;
            } );
        });

        var task3 = Task.Run(async () =>
        {
            try
            {
                var results = await finder.SearchPodcastsAsync(searchText, 10);
                Device.BeginInvokeOnMainThread(() =>
                {
                    VideoItems3.Clear();
                    foreach (var podcast in results)
                    {
                        var thumbnailUrl = podcast.ArtWork;
                        var podcastUrl = podcast.FeedUrl;

                        VideoItems3.Add(new VideoItem3
                        {
                            Title = podcast.Name,
                            Author = podcast.Editor,
                            Thumbnail = thumbnailUrl,
                            Url = podcastUrl
                        });
                    }
                    songsListView3.ItemsSource = VideoItems3;
                songsListView3.IsVisible = true; loadingIndicator3.IsRunning = false; loadingIndicator3.IsVisible = false;
                });
                
            }
            catch
            {
                // Hata iþleme
            }
        });

        await Task.WhenAll(task1, task2, task3); 
       
#endif
#if ANDROID
    // Göstergeyi durdur
    songsListView3.IsVisible = true;
   songsListView2.ItemsSource = VideoItems2;
                songsListView2.IsVisible = true;
    songsListView2.IsVisible = true;
 songsListView1.IsVisible = true;
    
#endif
    }
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        textchan(e.NewTextValue);
    }


    public async Task<bool> IsTitleExists(string title)
    {
        // "songs" düðümü altýnda "Title" özelliðine göre sorgulama yapýn
        // Bu örnekte, "songs" verilerinizi sakladýðýnýz düðümün adýdýr
        
                                                                                                                                                                                                                                   
        String userId = SecureStorage.GetAsync("user_token").Result ?? "SalihDeneme";
       connection cd=new connection();
        cc=cd;
        FirebaseResponse response = await cc.client.GetAsync($"users/{userId}/AllMusics/Songs");
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
            DisplayAlert("Ýþlem tamamlandý", "Þarký listenize kaydedildi", "Tamam");
            ccc = new Crud();
            ccc.SetData(s);
        }

        MessagingCenter.Send<ogebul, string>(this, "strm", videoUrl);
        // URL'yi kullanarak istediðiniz iþlemi yapýn
        // Örneðin, bir mesaj gösterin veya videoyu oynatýn


        // Ýsteðe baðlý olarak, MessagingCenter aracýlýðýyla URL'yi gönderin


    }
    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedPodcast = e.CurrentSelection.FirstOrDefault() as VideoItem;
        if (selectedPodcast == null) return;

        var rssUrl = selectedPodcast.Url;

        // podcast.xaml.cs dosyasýný aç
        await Navigation.PushAsync(new podcast(rssUrl));
    }

    private void OnDownloadClicked(object sender, EventArgs e)
    {
        DisplayAlert("Alarm", "Bu bir alarm mesajýdýr!", "Tamam");

    }

 


private async void songsListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
    }

    private async void songsListView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedVideoItem = e.CurrentSelection.FirstOrDefault() as VideoItem2;
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
                audioStreamInfo = "empty",
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

    private async void songsListView3_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
var selectedPodcast = e.CurrentSelection.FirstOrDefault() as VideoItem3;
        if (selectedPodcast == null) return;

        var rssUrl = selectedPodcast.Url;

        // podcast.xaml.cs dosyasýný aç
        await Navigation.PushAsync(new podcast(rssUrl));
        }
        catch(Exception ex) {
            DisplayAlert("Hata", $"Beklenmeyen bir sorun oluþtu({ex.Message})", "Tamam");
        }

    }

    private async void Frame_Focused(object sender, FocusEventArgs e)
    {
#if ANDROID
 if (!string.IsNullOrWhiteSpace(searchEntry.Text))
    {
         lbl1.IsVisible = true;

            lbl2.IsVisible = true;
            lbl3.IsVisible = true;  
            songsListView1.IsVisible = true;
            songsListView2.IsVisible = true;
            songsListView3.IsVisible = true;
    }
  else{
   lbl1.IsVisible = false;

            lbl2.IsVisible = false;
            lbl3.IsVisible = false; 
            songsListView1.IsVisible = false;
            songsListView2.IsVisible = false;
            songsListView3.IsVisible = false;
  }
 backButton.IsVisible = true;
            Grid.SetColumnSpan(searchEntry, 1); asaa.IsVisible = false;
          lkl.IsVisible = false;
 
             
 
       
        scc.IsVisible = true;
        scr.IsVisible = false;
#endif

    }




    private async void OnPlayCldicked(object sender, EventArgs e)
    {
        try
        {
var button = sender as ImageButton;
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
            var streamManifest = await Task.Run(() => youtubeS.Videos.Streams.GetManifestAsync(videoS.Id));
       var manifestResult = await streamManifest;
            var audioStreamInfo = manifestResult.GetAudioOnlyStreams().GetWithHighestBitrate();

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
                ccc=new Crud();
            ccc.SetData(s);
        }

        MessagingCenter.Send<ogebul, string>(this, "strm", videoUrl);
        }
        catch (Exception ex)
        {

            DisplayAlert("Hata", "Beklenmeyen bir sorun oluþtu", "Tamam");
        }
        // 'sender' nesnesini Button olarak kabul edin
        
        // URL'yi kullanarak istediðiniz iþlemi yapýn
        // Örneðin, bir mesaj gösterin veya videoyu oynatýn


        // Ýsteðe baðlý olarak, MessagingCenter aracýlýðýyla URL'yi gönderin


    }


    private void OnBackButtonClicked(object sender, EventArgs e)
    {
#if ANDROID
 searchEntry.Unfocus();

#endif
       
        }
    
    private void searchEntry_Unfocused(object sender, FocusEventArgs e)
    {
#if ANDROID
   backButton.IsVisible = false;
            Grid.SetColumnSpan(searchEntry, 2);
        lbl1.IsVisible = false;
        lbl2.IsVisible = false; 
        asaa.IsVisible = true;
        lbl3.IsVisible = false;
        lkl.IsVisible = true;
        scc.IsVisible = false;
        songsListView1.IsVisible = false;
        songsListView2.IsVisible = false;
        songsListView3.IsVisible = false;
        scr.IsVisible = true;
        var context = Platform.AppContext;
        
#endif

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
#if ANDROID
 
         lbl1.IsVisible = true;

        btn.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#73736c"); 
        btn1.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
         btn2.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
          btn3.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
        lbl2.IsVisible = true;
            lbl3.IsVisible = true;  
            songsListView1.IsVisible = true;
            songsListView2.IsVisible = true;
            songsListView3.IsVisible = true;
#endif
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
#if ANDROID
 
         lbl1.IsVisible = true;
         btn.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38"); 
        btn1.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#73736c");
         btn2.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
          btn3.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
            lbl2.IsVisible = false;
            lbl3.IsVisible = false;  
            songsListView1.IsVisible = true;
            songsListView2.IsVisible = false;
            songsListView3.IsVisible = false;
#endif
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
#if ANDROID
 btn.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38"); 
        btn1.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
         btn2.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#73736c");
          btn3.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
         lbl1.IsVisible = false;

            lbl2.IsVisible = true;
            lbl3.IsVisible = false;  
            songsListView1.IsVisible = false;
            songsListView2.IsVisible = true;
            songsListView3.IsVisible = false;
#endif
    }

    private void Button_Clicked_3(object sender, EventArgs e)
    {
#if ANDROID
 btn.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38"); 
        btn1.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
         btn2.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#3b3a38");
          btn3.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex("#73736c");
         lbl1.IsVisible = false;

            lbl2.IsVisible = false;
            lbl3.IsVisible = true;  
            songsListView1.IsVisible = false;
            songsListView2.IsVisible = false;
            songsListView3.IsVisible = true;
#endif
    }

    private void searchEntry_Completed(object sender, EventArgs e)
    {
        var entry = sender as Entry;
        if (entry != null)
        {
            string searchText = entry.Text;
            // Arama iþlemini burada gerçekleþtirin
            textchan(searchText);
        }
        
    }
}
public class VideoItem2// YouTube video URL'si
{   
    public string Title { get; set; }
    public string Author { get; set; }
    public string Thumbnail { get; set; }
    public string Url
    {
        get; set;
    } // YouTube video URL'si
}
public class VideoItem3// YouTube video URL'si
{   
    public string Title { get; set; }
    public string Author { get; set; }
    public string Thumbnail { get; set; }
    public string Url
    {
        get; set;
    } // YouTube video URL'si
}