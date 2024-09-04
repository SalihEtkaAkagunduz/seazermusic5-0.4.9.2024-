using System.Collections.ObjectModel;
using FirebaseMedium;
 
using Microsoft.Maui.Controls;
 
using System.Diagnostics;
using YoutubeExplode;
using YoutubeExplode.Common;
namespace seazermusic5
{
    public partial class Gözat : ContentPage
    {
        
        public ObservableCollection<List<Item>> GroupedItems
        {
            get; set;
        }

        public ObservableCollection<ArsivItem> ArsivItems
        {
            get; set;
        }
        private YoutubeClient youtubeClient;
        public ObservableCollection<AlbumItem> RecentAlbums
        {
            get; set;
        }
        public ObservableCollection<Song5> Binddd
        {
            get; set;
        }
        public ObservableCollection<ImageItem> ImageItems { get; set; }
        public ObservableCollection<ObservableCollection<Item>> ImageGroups
        {
            get; set;
        }
        Crud crud;
        Dictionary<string, Song> ff;
        List<Song> songs;
        public   Gözat()
        {
            InitializeComponent(); Shell.SetNavBarIsVisible(this, false);  crud = new Crud();
        LoadDataAsync(); LoadLists(); youtubeClient = new YoutubeClient();
        RecentAlbums = new ObservableCollection<AlbumItem>
        {
            new AlbumItem { AlbumCover = "sezen_aksu.png", AlbumTitle = "Seni Istiyorum", ArtistName = "Sezen Aksu" },
            new AlbumItem { AlbumCover = "sertab_erener.png", AlbumTitle = "Güle Güle Şekerim", ArtistName = "Sertab Erener" },
             new AlbumItem { AlbumCover = "sezen_aksu.png", AlbumTitle = "Seni Istiyorum", ArtistName = "Sezen Aksu" },
            new AlbumItem { AlbumCover = "sertab_erener.png", AlbumTitle = "Güle Güle Şekerim", ArtistName = "Sertab Erener" },
             new AlbumItem { AlbumCover = "sezen_aksu.png", AlbumTitle = "Seni Istiyorum", ArtistName = "Sezen Aksu" },
            new AlbumItem { AlbumCover = "sertab_erener.png", AlbumTitle = "Güle Güle Şekerim", ArtistName = "Sertab Erener" }
        };  
            ArsivItems = new ObservableCollection<ArsivItem>
        {
            new ArsivItem { Icon = "bl11.png", Title = "Listeler" },
            new ArsivItem { Icon = "bl12.png", Title = "Sanatçılar" },
            new ArsivItem { Icon = "bl9.png", Title = "Albümler" },
            new ArsivItem { Icon = "bl8.png", Title = "Parçalar" },
            new ArsivItem { Icon = "bl7.png", Title = "Size Özel", EditText = "Düzenle", IsEditVisible = true },
            new ArsivItem { Icon = "bl1.png", Title = "İndirilenler" },
            new ArsivItem { Icon = "bl3.png", Title = "TV ve Filmler" },
            new ArsivItem { Icon = "bl4.png", Title = "Video Listeleri" },
            new ArsivItem { Icon = "bl2.png", Title = "Podcast" },
        };

            // Recent Albums (Son Eklenenler)
            Binddd = new ObservableCollection<Song5>();
            ff = crud.LoadBrowseSongs();
            if (songs == null)
            {
                songs = new List<Song>();
            }
            songs = ff.Values.ToList();
            // En fazla 25 şarkı işlenir
            var maxSongs = songs.Take(25).ToList();

            // 5'e tam bölünen kısımları al
            var validSongs = maxSongs.Take((maxSongs.Count / 5) * 5).ToList();

            var song5List = new List<Song5>();

            for (int i = 0; i < validSongs.Count; i += 5)
            {
                var song5 = new Song5
                {
                    audioStreamInfo1 = validSongs[i].audioStreamInfo,
                    ImageUrl1 = validSongs[i].ImageUrl,
                    Title1 = validSongs[i].Title,
                    Artist1 = validSongs[i].Artist,
                    Single1 = validSongs[i].Single,
                    Length1 = validSongs[i].Length,
                    Tag1 = validSongs[i].Tag,
                    YouTubeLink1 = validSongs[i].YouTubeLink,

                    audioStreamInfo2 = validSongs[i + 1].audioStreamInfo,
                    ImageUrl2 = validSongs[i + 1].ImageUrl,
                    Title2 = validSongs[i + 1].Title,
                    Artist2 = validSongs[i + 1].Artist,
                    Single2 = validSongs[i + 1].Single,
                    Length2 = validSongs[i + 1].Length,
                    Tag2 = validSongs[i + 1].Tag,
                    YouTubeLink2 = validSongs[i + 1].YouTubeLink,

                    audioStreamInfo3 = validSongs[i + 2].audioStreamInfo,
                    ImageUrl3 = validSongs[i + 2].ImageUrl,
                    Title3 = validSongs[i + 2].Title,
                    Artist3 = validSongs[i + 2].Artist,
                    Single3 = validSongs[i + 2].Single,
                    Length3 = validSongs[i + 2].Length,
                    Tag3 = validSongs[i + 2].Tag,
                    YouTubeLink3 = validSongs[i + 2].YouTubeLink,

                    audioStreamInfo4 = validSongs[i + 3].audioStreamInfo,
                    ImageUrl4 = validSongs[i + 3].ImageUrl,
                    Title4 = validSongs[i + 3].Title,
                    Artist4 = validSongs[i + 3].Artist,
                    Single4 = validSongs[i + 3].Single,
                    Length4 = validSongs[i + 3].Length,
                    Tag4 = validSongs[i + 3].Tag,
                    YouTubeLink4 = validSongs[i + 3].YouTubeLink,

                    audioStreamInfo5 = validSongs[i + 4].audioStreamInfo,
                    ImageUrl5 = validSongs[i + 4].ImageUrl,
                    Title5 = validSongs[i + 4].Title,
                    Artist5 = validSongs[i + 4].Artist,
                    Single5 = validSongs[i + 4].Single,
                    Length5 = validSongs[i + 4].Length,
                    Tag5 = validSongs[i + 4].Tag,
                    YouTubeLink5 = validSongs[i + 4].YouTubeLink
                };

                song5List.Add(song5);
            }

            Binddd = new ObservableCollection<Song5>(song5List);
         
        ArsivCollectionView22.ItemsSource = ArsivItems;
       
        RecentAlbumsCollectionView.ItemsSource = RecentAlbums; 
        RecentAlbumsCollectionView1.ItemsSource = RecentAlbums;
        RecentAlbumsCollectionView6.ItemsSource = RecentAlbums;
        RecentAlbumsCollectionView3.ItemsSource = RecentAlbums;
        RecentAlbumsCollectionView4.ItemsSource = RecentAlbums;
        RecentAlbumsCollectionView5.ItemsSource = RecentAlbums;
            ImageItems = new ObservableCollection<ImageItem>
            {
                new ImageItem { ImageSource = "l7.jpg",    ImageText = "HOT PLAYLIST" , ImageText2 = "Hot List: Turkish Rap" , ImageText3 = "Seazer Music Hip-Hop/Rap"   },
                new ImageItem { ImageSource = "l6.jpg",    ImageText = "UPDATED PLAYLIST" , ImageText2 = "Today’s Hits" , ImageText3 = "Seazer Music Hits" },
                new ImageItem { ImageSource = "l4.jpg",    ImageText = "LOCAL PLAYLIST" , ImageText2 = "Zirvedekiler Türkiye: Hande Yener" , ImageText3 = "Seazer Music Local" },
                 new ImageItem { ImageSource = "l3.jpg",   ImageText = "LISTEN NOW" , ImageText2 = "Short n' Sweet" , ImageText3 = "Sabrina Carpenter" },
                new ImageItem { ImageSource = "l2.jpg",    ImageText = "UPDATED PLAYLIST" , ImageText2 = "Zirvedekiler: Türkçe Alternative" , ImageText3 = "Seazer Music Alternative" },
                new ImageItem { ImageSource = "l1.jpg",    ImageText = "UPDATED PLAYLIST" , ImageText2 = "New Music Daily" , ImageText3 = "Seazer Music" },
                 new ImageItem { ImageSource = "l5.jpg",   ImageText = "JUST UPDATED" , ImageText2 = "YepYeni Türkçe Pop: Reyhan Karaca " , ImageText3 = "Seazer Music Local" },
                new ImageItem { ImageSource = "l15.jpg",    ImageText = "UPDATED PLAYLIST" , ImageText2 = "YepYeni Türkçe Rap: Allame ve daha fazlası..." , ImageText3 = "Seazer Music Local" },
                new ImageItem { ImageSource = "l9.jpg",    ImageText = "HOT PLAYLIST" , ImageText2 = "Oasis Essentials" , ImageText3 = "Seazer Music Alternative" },
                 new ImageItem { ImageSource = "l14.jpg",  ImageText = "LISTEN NOW" , ImageText2 = "Gazino" , ImageText3 = "Seazer Music" },
                new ImageItem { ImageSource = "l13.jpg",   ImageText = "UPDATED PLAYLIST" , ImageText2 = "Summer Hits Turkish Pop" , ImageText3 = "Seazer Music Pop" },
       
                
            };
           
            BindingContext = this;

             
            // ItemTemplate'i ayarla


            var items = new List<Item>
        {
             new Item { ImageSource = "image1.jpg", ImageText = "Text1" },
                new Item { ImageSource = "image2.jpg", ImageText = "Text2" },
                new Item { ImageSource = "image3.jpg", ImageText = "Text3" },
                new Item { ImageSource = "image4.jpg", ImageText = "Text4" },
                new Item { ImageSource = "image5.jpg", ImageText = "Text5" },
                new Item { ImageSource = "image6.jpg", ImageText = "Text6" },
                new Item { ImageSource = "image7.jpg", ImageText = "Text7" },
                new Item { ImageSource = "image8.jpg", ImageText = "Text8" },
                new Item { ImageSource = "image9.jpg", ImageText = "Text9" },
                new Item { ImageSource = "image10.jpg", ImageText = "Text10" },
                new Item { ImageSource = "image11.jpg", ImageText = "Text11" },
                new Item { ImageSource = "image12.jpg", ImageText = "Text12" },
                new Item { ImageSource = "image13.jpg", ImageText = "Text13" },
                new Item { ImageSource = "image14.jpg", ImageText = "Text14" },
                new Item { ImageSource = "image15.jpg", ImageText = "Text15" },
                new Item { ImageSource = "image16.jpg", ImageText = "Text16" },
                new Item { ImageSource = "image17.jpg", ImageText = "Text17" },
                new Item { ImageSource = "image18.jpg", ImageText = "Text18" },
                new Item { ImageSource = "image19.jpg", ImageText = "Text19" },
                
        };
            GroupedItems = new ObservableCollection<List<Item>>(ListHelper.SplitList(items, 4));
      
            
  
        }
        private async Task LoadDataAsync()
        {
            ff = await Task.Run(() => crud.LoadDataa());
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
        private async void LoadLists()
        {
            try
            {
                Dictionary<string, Listt2> allLists2 = await Task.Run(() => crud.LoadBrowsepodcast());
#if ANDROID
                    RecentAlbumsCollectionView3.ItemsSource = allLists2.Values.ToList();
#endif
                Dictionary<string, Listt> allLists = await Task.Run(() => crud.LoadBrowseListsvoid()); // Tüm listeleri çek
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
        private async void RecentAlbumsCollectionView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection.FirstOrDefault() as Listt;
            if (current != null)
            {
                var listeEkraniPage = new listeekrani(current.Name);
                await Navigation.PushAsync(listeEkraniPage);
                // Veri yüklemeyi yeni ekran açıldıktan sonra başlat
            }
        }
        //private async void RecentAlbumsCollectionView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var current = e.CurrentSelection.FirstOrDefault() as Listt;
        //    if (current != null)
        //    {+
        //        var listeEkraniPage = listeekrani.GetInstanceAsync(current);
        //        await Navigation.PushAsync(await listeEkraniPage);
        //        // Veri yüklemeyi yeni ekran açıldıktan sonra başlat

        //    }
        //} 
        public   void  ConvertSongsToSong5()
        {
            ff =     crud.LoadDataa() ;
            if (songs == null)
            {
                songs = new List<Song>();
            }
            songs = ff.Values.ToList();
            // En fazla 25 şarkı işlenir
            var maxSongs = songs.Take(25).ToList();

            // 5'e tam bölünen kısımları al
            var validSongs = maxSongs.Take((maxSongs.Count / 5) * 5).ToList();

            var song5List = new List<Song5>();

            for (int i = 0; i < validSongs.Count; i += 5)
            {
                var song5 = new Song5
                {
                    audioStreamInfo1 = validSongs[i].audioStreamInfo,
                    ImageUrl1 = validSongs[i].ImageUrl,
                    Title1 = validSongs[i].Title,
                    Artist1 = validSongs[i].Artist,
                    Single1 = validSongs[i].Single,
                    Length1 = validSongs[i].Length,
                    Tag1 = validSongs[i].Tag,
                    YouTubeLink1 = validSongs[i].YouTubeLink,

                    audioStreamInfo2 = validSongs[i + 1].audioStreamInfo,
                    ImageUrl2 = validSongs[i + 1].ImageUrl,
                    Title2 = validSongs[i + 1].Title,
                    Artist2 = validSongs[i + 1].Artist,
                    Single2 = validSongs[i + 1].Single,
                    Length2 = validSongs[i + 1].Length,
                    Tag2 = validSongs[i + 1].Tag,
                    YouTubeLink2 = validSongs[i + 1].YouTubeLink,

                    audioStreamInfo3 = validSongs[i + 2].audioStreamInfo,
                    ImageUrl3 = validSongs[i + 2].ImageUrl,
                    Title3 = validSongs[i + 2].Title,
                    Artist3 = validSongs[i + 2].Artist,
                    Single3 = validSongs[i + 2].Single,
                    Length3 = validSongs[i + 2].Length,
                    Tag3 = validSongs[i + 2].Tag,
                    YouTubeLink3 = validSongs[i + 2].YouTubeLink,

                    audioStreamInfo4 = validSongs[i + 3].audioStreamInfo,
                    ImageUrl4 = validSongs[i + 3].ImageUrl,
                    Title4 = validSongs[i + 3].Title,
                    Artist4 = validSongs[i + 3].Artist,
                    Single4 = validSongs[i + 3].Single,
                    Length4 = validSongs[i + 3].Length,
                    Tag4 = validSongs[i + 3].Tag,
                    YouTubeLink4 = validSongs[i + 3].YouTubeLink,

                    audioStreamInfo5 = validSongs[i + 4].audioStreamInfo,
                    ImageUrl5 = validSongs[i + 4].ImageUrl,
                    Title5 = validSongs[i + 4].Title,
                    Artist5 = validSongs[i + 4].Artist,
                    Single5 = validSongs[i + 4].Single,
                    Length5 = validSongs[i + 4].Length,
                    Tag5 = validSongs[i + 4].Tag,
                    YouTubeLink5 = validSongs[i + 4].YouTubeLink
                };

                song5List.Add(song5);
            }

            Binddd = new ObservableCollection<Song5>(song5List);
        }

        private async void OnMoreOptionsClicked(object sender, EventArgs e)
        {
        }
            private async void RecentAlbumsCollectionView3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection.FirstOrDefault() as Listt2;
            if (current != null)
            {
                var listeEkraniPage = new podcast(current.Rss);
                await Navigation.PushAsync(listeEkraniPage);
                // Veri yüklemeyi yeni ekran açıldıktan sonra başlat
            }
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            var listeEkraniPage = new kayıtlısarki();
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

    public class ImageItem
    {
        public string ImageSource { get; set; }
        public string ImageText { get; set; }
        public string ImageText2 { get; set;  }public string ImageText3 { get; set; }
    }
    public class Item
    {
        public string ImageSource
        {
            get; set;
        }
        public string ImageText
        {
            get; set;
        }
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
    public static class ListHelper
    {
        public static List<List<T>> SplitList<T>(List<T> items, int nSize)
        {
            var list = new List<List<T>>();
            for (int i = 0; i < items.Count; i += nSize)
            {
                list.Add(items.GetRange(i, Math.Min(nSize, items.Count - i)));
            }
            return list;
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
}
