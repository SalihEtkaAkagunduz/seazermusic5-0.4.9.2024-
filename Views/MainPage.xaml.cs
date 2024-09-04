using Microsoft.Maui.Controls;

namespace seazermusic5
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSssButtonClicked(object sender, EventArgs e)
        {
            // sss.png butonuna tıklanınca yapılacak işlemler
            DisplayAlert("Button Clicked", "sss.png button clicked", "OK");
        }

        private void OnQqqButtonClicked(object sender, EventArgs e)
        {
            // qqq.png butonuna tıklanınca yapılacak işlemler
            DisplayAlert("Button Clicked", "qqq.png button clicked", "OK");
        }

        private void OnPppButtonClicked(object sender, EventArgs e)
        {
            // ppp.png butonuna tıklanınca yapılacak işlemler
            DisplayAlert("Button Clicked", "ppp.png button clicked", "OK");
        }

        private void OnEeButtonClicked(object sender, EventArgs e)
        {
            // ee.png butonuna tıklanınca yapılacak işlemler
            DisplayAlert("Button Clicked", "ee.png button clicked", "OK");
        }

        private void OnTttButtonClicked(object sender, EventArgs e)
        {
            // ttt.png butonuna tıklanınca yapılacak işlemler
            DisplayAlert("Button Clicked", "ttt.png button clicked", "OK");
        }

        private void OnHhhButtonClicked(object sender, EventArgs e)
        {
            // hhh.png butonuna tıklanınca yapılacak işlemler
            DisplayAlert("Button Clicked", "hhh.png button clicked", "OK");
        }
    }
}

