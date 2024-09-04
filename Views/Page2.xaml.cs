using System;
using Microsoft.Maui.Controls;

namespace seazermusic5
{
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
          
        }

        private async void OnPlayClicked(object sender, EventArgs e)
        {
            await webView.EvaluateJavaScriptAsync("playVideo();");
        }

        private async void OnStopClicked(object sender, EventArgs e)
        {
            await webView.EvaluateJavaScriptAsync("stopVideo();");
        }
    }
}
