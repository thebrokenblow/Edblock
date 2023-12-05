using Prism.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols.LineSymbols;

public class MovableRectangleLine : INotifyPropertyChanged
{
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

    private bool isShow = false;
    public bool IsShow
    {
        get => isShow;
        set
        {
            isShow = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public DelegateCommand Click { get; init; }
    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private readonly LineSymbolVM _lineSymbolVM;
    public MovableRectangleLine(DrawnLineSymbolVM drawnLineSymbolVM, LineSymbolVM lineSymbolVM)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;
        _lineSymbolVM = lineSymbolVM;
        SetCoordinate();
        Click = new(Click1);
    }

    public void Click1()
    {
        
    }

    private void SetCoordinate()
    {
        if (_lineSymbolVM.Y1 == _lineSymbolVM.Y2)
        {
            YCoordinate = _lineSymbolVM.Y2;
            XCoordinate = _lineSymbolVM.X1 + (_lineSymbolVM.X2 - _lineSymbolVM.X1) / 2;
        }
        else
        {
            YCoordinate = _lineSymbolVM.Y1 + (_lineSymbolVM.Y2 - _lineSymbolVM.Y1) / 2;
            XCoordinate = _lineSymbolVM.X2;
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}