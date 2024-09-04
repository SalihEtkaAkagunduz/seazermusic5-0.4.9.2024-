using System.ComponentModel;
using System.Collections.ObjectModel;

public class FooterViewModel : INotifyPropertyChanged
{
    private string _songTitle;
    public string SongTitle
    {
        get => _songTitle;
        set
        {
            _songTitle = value;
            OnPropertyChanged(nameof(SongTitle));
        }
    }

    private string _artistName;
    public string ArtistName
    {
        get => _artistName;
        set
        {
            _artistName = value;
            OnPropertyChanged(nameof(ArtistName));
        }
    }

    private bool _isPlaying;
    public bool IsPlaying
    {
        get => _isPlaying;
        set
        {
            _isPlaying = value;
            OnPropertyChanged(nameof(IsPlaying));
        }
    }

    private double _currentPosition;
    public double CurrentPosition
    {
        get => _currentPosition;
        set
        {
            _currentPosition = value;
            OnPropertyChanged(nameof(CurrentPosition));
        }
    }

    private double _duration;
    public double Duration
    {
        get => _duration;
        set
        {
            _duration = value;
            OnPropertyChanged(nameof(Duration));
        }
    }

    // Diğer özellikler ve metodlar

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
