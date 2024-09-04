using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.LifecycleEvents;
using The49.Maui.BottomSheet;


namespace seazermusic5
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBottomSheet()
                  .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("AnnieUseYourTelescope-Regular.ttf", "AnnieUseYourTelescopeRegular");
                    fonts.AddFont("Condiment-Regular.ttf", "CondimentRegular");
                    fonts.AddFont("Farsan-Regular.ttf", "FarsanRegular");
                    fonts.AddFont("FiraSans-Medium.ttf", "FiraSansMedium");
                    fonts.AddFont("GrapeNuts-Regular.ttf", "GrapeNutsRegular");
                    fonts.AddFont("Kanit-Bold.ttf", "KanitBold");
                    fonts.AddFont("Kanit-Medium.ttf", "KanitMedium");
                    fonts.AddFont("Lugrasimo-Regular.ttf", "LugrasimoRegular");
                    fonts.AddFont("MPLUSRounded1c-Bold.ttf", "MPLUSRounded1cBold");
                    fonts.AddFont("MPLUSRounded1c-Medium.ttf", "MPLUSRounded1cMedium");
                    fonts.AddFont("Rubik-VariableFont_wght.ttf", "RubikVariableFontWght");
                    fonts.AddFont("ShadowsIntoLightTwo-Regular.ttf", "ShadowsIntoLightTwoRegular");
                    fonts.AddFont("Whisper-Regular.ttf", "WhisperRegular");

                });
            

Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
{
#if ANDROID
 
  
 
    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
});

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
