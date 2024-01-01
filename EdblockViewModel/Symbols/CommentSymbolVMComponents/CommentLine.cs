using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols.CommentSymbolVMComponents;

public class CommentLine : INotifyPropertyChanged
{
    private double x1;
    public double X1 
    {
        get => x1;
        set
        {
            x1 = value;
            OnPropertyChanged();
        }
    }

    private double y1;
    public double Y1 
    {
        get => y1;
        set
        {
            y1 = value;
            OnPropertyChanged();
        }
    }

    private double x2;
    public double X2
    {
        get => x2;
        set
        {
            x2 = value;
            OnPropertyChanged();
        }
    }

    private double y2;
    public double Y2 
    {
        get => y2;
        set
        {
            y2 = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}