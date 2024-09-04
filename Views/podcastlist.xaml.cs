using FirebaseMedium;
using System.Diagnostics;

namespace seazermusic5
{
    public partial class podcastlist : ContentPage
    {
        Crud crud = new Crud();

        public podcastlist()
        {
            InitializeComponent();
            LoadLists();
        }

        private async void LoadLists()
        {
            try
            {
                Dictionary<string, Listt2> allLists = await Task.Run(() => crud.LoadAllpodcast()); // Tüm listeleri çek
                if (allLists != null)
                {
#if ANDROID
                    collectionView.ItemsSource = allLists.Values.ToList();
#endif
#if WINDOWS
                    foreach (Listt2 list in allLists.Values)
                        {
                            CreateListFrame(list); // Sadece liste adýný kullan
                        }
#endif         
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading lists: {ex.Message}");
            }
        }

        private async void CreateListFrame(Listt2 listName)
        {
#if WINDOWS
            var frame = new Frame
            {
                BorderColor = Colors.Gray,
                CornerRadius = 10,
                Padding = 10,
                Margin = new Thickness(10),
                HeightRequest = 295,
                WidthRequest = 260,
                Content = new VerticalStackLayout
                {
                    Children =
                    {
                        new Image { Source = listName.ImageUrl, HeightRequest = 240, WidthRequest = 240, Aspect = Aspect.AspectFill },
                        new Label { Text = listName.Name, FontFamily = "RubikVariableFontWght", VerticalOptions = LayoutOptions.Center, FontSize = 20, TextColor = Colors.WhiteSmoke, HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 10, 0, 0) }
                    }
                }
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) => await OnListTapped(listName);
            frame.GestureRecognizers.Add(tapGestureRecognizer);

        // FlexLayout'a ekle
        (Content as ScrollView).Content.FindByName<FlexLayout>("flexLayout").Children.Add(frame);
#endif
        }
        private async void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection.FirstOrDefault() as Listt2;
            if (current != null)
            {
                var listeEkraniPage = new podcast(current.Rss);
                await Navigation.PushAsync(listeEkraniPage);
               // Veri yüklemeyi yeni ekran açýldýktan sonra baþlat
            }
        }
        private async Task OnListTapped(Listt2 listName)
        {
            var listeEkraniPage = new podcast(listName.Rss);
            await Navigation.PushAsync(listeEkraniPage);
             // Veri yüklemeyi yeni ekran açýldýktan sonra baþlat
        }
    }
}
