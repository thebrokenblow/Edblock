using System.ComponentModel;
using MVVM.ViewModel.SymbolsViewModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols;

public abstract class Symbol : INotifyPropertyChanged
{
    protected const int defaultWidth = 140;
    protected const int defaultHeigth = 60;

    private int width;
    public int Width
    {
        get => width;
        set
        {
            width = value;
            OnPropertyChanged();
        }
    }

    private int heigth;
    public int Height
    {
        get => heigth;
        set
        {
            heigth = value;
            OnPropertyChanged();
        }
    }

    private int xCoordinate;
    public int XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
            OnPropertyChanged();
        }
    }

    private int yCoordinate;
    public int YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value;
            OnPropertyChanged();
        }
    }

    public TextField TextField { get; init; }

    public Symbol()
    {
        TextField = new();
        width = defaultWidth;
        heigth = defaultHeigth;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}