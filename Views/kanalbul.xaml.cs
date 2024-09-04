using System.Collections.ObjectModel;
using YoutubeExplode;
using System.Linq;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos;
using FirebaseMedium;
using FireSharp;
using FireSharp.Response;
using YoutubeExplode.Common;
using System.Threading;
using System.Threading.Tasks;
using VideoLibrary;

namespace seazermusic5
{
    public partial class kanalbul : ContentPage
    {
        private readonly Crud ccc;
        private readonly connection cc;
        private readonly YoutubeClient youtubeClient;
        public ObservableCollection<KanalItem> KanalItems { get; set; } = new ObservableCollection<KanalItem>();
        private CancellationTokenSource _cancellationTokenSource;

        public kanalbul()
        {
            InitializeComponent();
            cc = new connection();
            ccc = new Crud();
            youtubeClient = new YoutubeClient();
            songsCollectionView.ItemsSource = KanalItems;
        }

        private async Task OnSearchTextChangedAsync(string searchText, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(searchText)) return;

            // Show loading indicator
            Device.BeginInvokeOnMainThread(() =>
            {
                loadingIndicator.IsRunning = true;
                loadingIndicator.IsVisible = true;
            });

            try
            {
                var videos = await youtubeClient.Search.GetChannelsAsync(searchText).Take(10).ToListAsync(cancellationToken);

                Device.BeginInvokeOnMainThread(() =>
                {
                    KanalItems.Clear();
                    foreach (var video in videos)
                    {
                        var thumbnailUrl = video.Thumbnails.OrderByDescending(t => t.Resolution.Area).FirstOrDefault()?.Url;
                        var videoUrl = $"https://www.youtube.com/channel/{video.Id}";

                        KanalItems.Add(new KanalItem
                        {
                            Title = video.Title,
                            id = video.Id,
                            Thumbnail = thumbnailUrl,
                            Url = videoUrl
                        });
                    }
                });
            }
            catch (OperationCanceledException)
            {
                // Operation was canceled, likely due to a new search term being entered
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            finally
            {
                // Hide loading indicator
                Device.BeginInvokeOnMainThread(() =>
                {
                    loadingIndicator.IsRunning = false;
                    loadingIndicator.IsVisible = false;
                });
            }
        }

        private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            try
            {
 await Task.Delay(2500, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
           

            if (!cancellationToken.IsCancellationRequested)
            {
                await OnSearchTextChangedAsync(e.NewTextValue, cancellationToken);
            }
        }

        public async Task<bool> IsTitleExists(string title)
        {
            FirebaseResponse response = await cc.client.GetAsync("users/SalihDeneme/Lists/List1/Songs");
            if (response.Body != "null")
            {
                var songs = response.ResultAs<Dictionary<string, Song>>();
                return songs.Values.Any(song => song.Title == title);
            }
            return false;
        }

        private async void OnPlayClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is VideoItem videoItem)
            {
                var videoUrl = videoItem.Url;

                try
                {
                    var youtubeS = new YoutubeClient();
                    var videoS = await youtubeS.Videos.GetAsync(videoUrl);
                    var streamManifest = await youtubeS.Videos.Streams.GetManifestAsync(videoS.Id);
                    var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                    var song = new Song
                    {
                        Title = videoS.Title,
                        Artist = videoS.Author.ChannelTitle,
                        ImageUrl = videoS.Thumbnails.GetWithHighestResolution()?.Url,
                        Length = videoS.Duration?.ToString(),
                        YouTubeLink = videoUrl,
                        audioStreamInfo = audioStreamInfo.Url,
                        Single = "Single"
                    };

                    if (await IsTitleExists(song.Title))
                    {
                        await DisplayAlert("Uyarý", "Bu video zaten listenize kayýtlý", "Tamam");
                    }
                    else
                    {
                        await DisplayAlert("Ýþlem tamamlandý", "Þarký listenize kaydedildi", "Tamam");
                        ccc.SetData(song);
                    }

                    MessagingCenter.Send(this, "strm", videoUrl);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is VideoItem selectedVideoItem)
            {
                var playlistUrl = selectedVideoItem.Url;

                try
                {
                    var playlist = await youtubeClient.Playlists.GetVideosAsync(playlistUrl);
                    var songs = playlist.ToDictionary(
                        video => video.Id,
                        video => new Song
                        {
                            Title = video.Title,
                            Artist = video.Author.ChannelTitle,
                            ImageUrl = video.Thumbnails.GetWithHighestResolution()?.Url,
                            Length = video.Duration?.ToString(),
                            YouTubeLink = $"https://www.youtube.com/watch?v={video.Id}",
                            audioStreamInfo = "empty",
                            Single = "Single"
                        });

                    var listt = new Listt
                    {
                        Name = selectedVideoItem.Title,
                        Description = "Playlist Description",
                        ImageUrl = selectedVideoItem.Thumbnail,
                       
                    };

                    await Navigation.PushAsync(new listeekrani(listt));
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

        private void OnDownloadClicked(object sender, EventArgs e)
        {
            DisplayAlert("Alarm", "Bu bir alarm mesajýdýr!", "Tamam");
        }

        public class KanalItem
        {
            public string Title
            {
                get; set;
            }
            public string Url
            {
                get; set;
            }
            public string Thumbnail
            {
                get; set;
            }
            public string id
            {
                get; set;
            }
        }
    }
}
