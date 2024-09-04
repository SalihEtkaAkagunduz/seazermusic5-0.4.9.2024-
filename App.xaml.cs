#if WINDOWS
using Windows.ApplicationModel.Background;
#endif
#if ANDROID
using Android.Content.Res;
#endif
namespace seazermusic5
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
     Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) => {
#if ANDROID
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent); 
                #endif 
            });
            }

#if WINDOWS
        protected override void OnStart()
        {
            base.OnStart();
            RegisterBackgroundTask();
        }

        private void RegisterBackgroundTask()
        {
            try
            {
                var builder = new BackgroundTaskBuilder
                {
                    Name = "ToastNotificationHandler",
                    TaskEntryPoint = typeof(ToastNotificationHandler).FullName
                };

                builder.SetTrigger(new ToastNotificationActionTrigger());
                builder.Register();
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayın veya hata ayıklama amacıyla kullanın
                System.Diagnostics.Debug.WriteLine($"Background task registration failed: {ex.Message}");
            }
        }
 
 

        Window window;
        protected override Window CreateWindow(IActivationState activationState)
        {
            window = base.CreateWindow(activationState);
            window.MinimumHeight = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height * 0.4;
            window.MinimumWidth = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width * 0.4;
            window.SizeChanged += Windowd_Created;
            return window;
        }

        private void Windowd_Created(object sender, EventArgs e)
        {
            var pageWidth = window.Width;
            var screenWidth = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
            int gg = Convert.ToInt32(screenWidth * 0.7);

            // Pencere genişliği sayfanın %70'inden büyükse Flyout panelini açık tut
            if (pageWidth > gg)
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
            }
            else
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }
#endif
    }
}
