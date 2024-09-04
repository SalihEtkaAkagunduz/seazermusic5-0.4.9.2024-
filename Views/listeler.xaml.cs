using FirebaseMedium;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace seazermusic5
{
    public partial class listeler : ContentPage
    {
        Crud crud = new Crud();

        public listeler()
        {
            InitializeComponent();
            LoadLists();
        }

        private async void LoadLists()
        {
            try
            {
                Dictionary<string, Listt> allLists = await Task.Run(() => crud.LoadAllListsvoid()); // Tüm listeleri çek
                if (allLists != null)
                {
#if ANDROID
                    collectionView.ItemsSource = allLists.Values.ToList();
#endif
#if WINDOWS
                    foreach (Listt list in allLists.Values)
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

        private void CreateListFrame(Listt listName)
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
            flexLayout.Children.Add(frame);
#endif
        }

        private async Task OnListTapped(Listt listName)
        {
            var listeEkraniPage = new listeekrani(listName);
            await Navigation.PushAsync(listeEkraniPage);
            _ = listeEkraniPage.LoadSongsAsync(); // Veri yüklemeyi yeni ekran açýldýktan sonra baþlat
        }

        private async void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection.FirstOrDefault() as Listt;
            if (current != null)
            {
                var listeEkraniPage = new listeekrani(current.Name);
                await Navigation.PushAsync(listeEkraniPage);
                _ = listeEkraniPage.LoadSongsAsync(); // Veri yüklemeyi yeni ekran açýldýktan sonra baþlat
            }
        }

    }

} 