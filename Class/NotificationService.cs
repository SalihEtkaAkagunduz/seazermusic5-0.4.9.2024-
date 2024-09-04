using System;

#if WINDOWS
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
#endif

namespace seazermusic5
{
    public class NotificationService
    {
        private const string ToastTag = "musicNotification";

        public void ShowNotification(string title, string message, string imageUrl)
        {
#if WINDOWS
            var content = new ToastContentBuilder()
                .AddText(title)
                .AddText(message)
                
                .AddButton(new ToastButton()
                    .SetContent("Geri")
                    .AddArgument("action", "previous")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("Oynat/Duraklat")
                    .AddArgument("action", "playpause")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("İleri")
                    .AddArgument("action", "next")
                    .SetBackgroundActivation())
                .GetToastContent();

            var toast = new ToastNotification(content.GetXml())
            {
                Tag = ToastTag,
                ExpirationTime = null, // Bildirimin kalıcı olmasını sağlar
                SuppressPopup = false // Bildirimin ekranda pop-up olarak görünmesini sağlar
            };

            // Bildirimin genişlemiş şekilde görünmesini sağlamak için
            toast.Group = "expandedGroup";

            ToastNotificationManager.CreateToastNotifier().Show(toast);
#else
            // Diğer platformlar için bildirim desteği yok
            throw new PlatformNotSupportedException("Bu özellik sadece Windows platformunda desteklenmektedir.");
#endif
        }

        public void UpdateNotification(string title, string message, string imageUrl)
        {
#if WINDOWS
            var content = new ToastContentBuilder()
                .AddText(title)
                .AddText(message)
                .AddInlineImage(new Uri(imageUrl))
                .AddButton(new ToastButton()
                    .SetContent("Geri")
                    .AddArgument("action", "previous")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("Oynat/Duraklat")
                    .AddArgument("action", "playpause")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("İleri")
                    .AddArgument("action", "next")
                    .SetBackgroundActivation())
                .GetToastContent();

            var toast = new ToastNotification(content.GetXml())
            {
                Tag = ToastTag,
                ExpirationTime = null, // Bildirimin kalıcı olmasını sağlar
                SuppressPopup = false // Bildirimin ekranda pop-up olarak görünmesini sağlar
            };

            // Bildirimin genişlemiş şekilde görünmesini sağlamak için
            toast.Group = "expandedGroup";

            ToastNotificationManager.CreateToastNotifier().Show(toast);
#else
            // Diğer platformlar için bildirim desteği yok
            throw new PlatformNotSupportedException("Bu özellik sadece Windows platformunda desteklenmektedir.");
#endif
        }
    }
}
