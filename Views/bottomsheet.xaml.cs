using System.Collections.ObjectModel;
using System.Windows.Input;
using The49.Maui.BottomSheet;
namespace seazermusic5.Views;
public class ListAction
{
    public string Title
    {
        get; set;
    }
    public ICommand Command
    {
        get; set;
    }
}

public partial class bottomsheet : BottomSheet
{
    public ObservableCollection<ListAction> Actions => new()
    {
        new ListAction
        {
            Title = "Share",
            Command = new Command(() => { }),
        },
        new ListAction
        {
            Title = "Copy",
            Command = new Command(() => { }),
        },
        new ListAction
        {
            Title = "Open in browser",
            Command = new Command(() => { }),
        },
         new ListAction
        {
            Title = "Resize",
            Command = new Command(Resize),
        },
        new ListAction
        {
            Title = "Dismiss",
            Command = new Command(() => DismissAsync()),
        }
    };
    public bottomsheet()
    {
        InitializeComponent(); BindingContext = this; // Bu sayfanýn veri baðlamýný
    }

    void Resize()
    {
#if ANDROID
      
    
#endif
        }



    
}
 