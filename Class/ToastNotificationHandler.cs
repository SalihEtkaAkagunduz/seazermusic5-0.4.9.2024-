using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
 
namespace seazermusic5
{

    public sealed class ToastNotificationHandler : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var details = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;
            var arguments = details.Argument;

            if (arguments.Contains("action=previous"))
            {
                // Geri butonuna tıklama işlemi
            }
            else if (arguments.Contains("action=playpause"))
            {
                // Oynat/Duraklat butonuna tıklama işlemi
            }
            else if (arguments.Contains("action=next"))
            {
                // İleri butonuna tıklama işlemi
            }
        }
    }
}
#endif
