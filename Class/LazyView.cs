using Microsoft.Maui.Controls;

namespace seazermusic5
{
    public class LazyView : ContentView
    {
        private bool _isLoaded;

        public static readonly BindableProperty ContentTemplateProperty =
            BindableProperty.Create(nameof(ContentTemplate), typeof(DataTemplate), typeof(LazyView), propertyChanged: OnContentTemplateChanged);

        public DataTemplate ContentTemplate
        {
            get => (DataTemplate)GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }

        private static void OnContentTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is LazyView lazyView && newValue is DataTemplate)
            {
                lazyView.LoadContent();
            }
        }

        private void LoadContent()
        {
            if (_isLoaded || ContentTemplate == null)
                return;

            Content = (View)ContentTemplate.CreateContent();
            _isLoaded = true;
        }
    }
}
