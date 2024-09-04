using FirebaseMedium;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Common;
namespace seazermusic5;

public partial class MainMenu : ContentPage
{ public ObservableCollection<ArsivItem> ArsivItems                                                             
    {
        get; set;
    }
    private   YoutubeClient youtubeClient;
    public ObservableCollection<AlbumItem> RecentAlbums
    {                                                                                                            
        get; set;
    }
  
    public   MainMenu()
	{                                                                   
		InitializeComponent(); crud = new Crud();
        LoadDataAsync(); LoadLists(); youtubeClient = new YoutubeClient();
        RecentAlbums = new ObservableCollection<AlbumItem>
        {
            new AlbumItem { AlbumCover = "sezen_aksu.png", AlbumTitle = "Seni Istiyorum", ArtistName = "Sezen Aksu" },
            new AlbumItem { AlbumCover = "sertab_erener.png", AlbumTitle = "Güle Güle Þekerim", ArtistName = "Sertab Erener" },
             new AlbumItem { AlbumCover = "sezen_aksu.png", AlbumTitle = "Seni Istiyorum", ArtistName = "Sezen Aksu" },
            new AlbumItem { AlbumCover = "sertab_erener.png", AlbumTitle = "Güle Güle Þekerim", ArtistName = "Sertab Erener" },
             new AlbumItem { AlbumCover = "sezen_aksu.png", AlbumTitle = "Seni Istiyorum", ArtistName = "Sezen Aksu" },
            new AlbumItem { AlbumCover = "sertab_erener.png", AlbumTitle = "Güle Güle Þekerim", ArtistName = "Sertab Erener" }
        };

        RecentAlbumsCollectionView.ItemsSource = RecentAlbums; 
        RecentAlbumsCollectionView1.ItemsSource = RecentAlbums;
        RecentAlbumsCollectionView6.ItemsSource = RecentAlbums;
        RecentAlbumsCollectionView3.ItemsSource = RecentAlbums;
        RecentAlbumsCollectionView4.ItemsSource = RecentAlbums;
        RecentAlbumsCollectionView5.ItemsSource = RecentAlbums;
    }       
    Crud crud;
    Dictionary<string, Song> ff;
    List<Song> songs;
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var listeEkraniPage = new Ayarlar();
        await Navigation.PushAsync(listeEkraniPage);
    }  private async void LoadLists()
    {
        try
        {
            Dictionary<string, Listt2> allLists2 = await Task.Run(() => crud.LoadAllpodcast());
#if ANDROID
                    RecentAlbumsCollectionView3.ItemsSource = allLists2.Values.ToList();
#endif
            Dictionary<string, Listt> allLists = await Task.Run(() => crud.LoadAllListsvoid()); // Tüm listeleri çek
            if (allLists != null)
            {
#if ANDROID
                    RecentAlbumsCollectionView1.ItemsSource = allLists.Values.ToList();
#endif
#if WINDOWS
                    foreach (Listt list in allLists.Values)
                        {
                         
                        }
#endif
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading lists: {ex.Message}");
        }
    }

    private async void AvatarView_Tapped(object sender, EventArgs e)
    {
        var listeEkraniPage = new User();
        await Navigation.PushAsync(listeEkraniPage);
    }
    private async Task LoadDataAsync()
    { 
        ff = await Task.Run(() => crud.LoadDataa());
        if (songs == null)
        {
            songs = new List<Song>();
        }
        songs = ff.Values.ToList();
        RecentAlbumsCollectionView.ItemsSource = songs;
        RecentAlbumsCollectionView1.ItemsSource = songs;
        
        RecentAlbumsCollectionView4.ItemsSource = songs;
        RecentAlbumsCollectionView5.ItemsSource = songs; RecentAlbumsCollectionView6.ItemsSource = songs;
    }
    public class ArsivItem
    {
        public string Icon
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string EditText
        {
            get; set;
        }
        public bool IsEditVisible
        {
            get; set;
        }
    }

    public class AlbumItem
    {
        public string AlbumCover
        {
            get; set;
        }
        public string AlbumTitle
        {
            get; set;
        }
        public string ArtistName
        {
            get; set;
        }
    }

    private async void RecentAlbumsCollectionView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection.FirstOrDefault() as Listt;
        if (current != null)
        {
            var listeEkraniPage = new listeekrani(current.Name);
            await Navigation.PushAsync(listeEkraniPage);
            // Veri yüklemeyi yeni ekran açýldýktan sonra baþlat
        }
    }
    //private async void RecentAlbumsCollectionView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    var current = e.CurrentSelection.FirstOrDefault() as Listt;
    //    if (current != null)
    //    {
    //        var listeEkraniPage = listeekrani.GetInstanceAsync(current);
    //        await Navigation.PushAsync(await listeEkraniPage);
    //        // Veri yüklemeyi yeni ekran açýldýktan sonra baþlat

    //    }
    //}

    private async void RecentAlbumsCollectionView3_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection.FirstOrDefault() as Listt2;
        if (current != null)
        {
            var listeEkraniPage = new podcast(current.Rss);
            await Navigation.PushAsync(listeEkraniPage);
            // Veri yüklemeyi yeni ekran açýldýktan sonra baþlat
        }
    }

    private async void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        var listeEkraniPage = new kayýtlýsarki();
        await Navigation.PushAsync(listeEkraniPage);
    }

    private async void ImageButton_Clicked_2(object sender, EventArgs e)
    {
        var listeEkraniPage = new listeler();
        await Navigation.PushAsync(listeEkraniPage);
    }

    private async void ImageButton_Clicked_3(object sender, EventArgs e)
    {
        var listeEkraniPage = new podcastlist();
        await Navigation.PushAsync(listeEkraniPage);
    }

    private async void ImageButton_Clicked_4(object sender, EventArgs e)
    {

    }

    private async void ImageButton_Clicked_5(object sender, EventArgs e)
    {

    }

    private async void ImageButton_Clicked_6(object sender, EventArgs e)
    {

    }
}