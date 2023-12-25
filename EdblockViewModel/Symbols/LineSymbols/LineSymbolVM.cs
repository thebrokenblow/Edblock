using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockModel.Symbols.LineSymbols;
using EdblockModel.SymbolsModel.LineSymbols;

namespace EdblockViewModel.Symbols.LineSymbols;

public class LineSymbolVM : INotifyPropertyChanged
{
    private int x1;
    public int X1
    {
        get => x1;
        set
        {
            x1 = value;
            _lineSymbolModel.X1 = x1;

            OnPropertyChanged();
        }
    }

    private int y1;
    public int Y1
    {
        get => y1;
        set
        {
            y1 = value;
            _lineSymbolModel.Y1 = y1;

            OnPropertyChanged();
        }
    }

    private int x2;
    public int X2
    {
        get => x2;
        set
        {
            x2 = value;
            _lineSymbolModel.X2 = x2;

            OnPropertyChanged();
        }
    }

    private int y2;

    public int Y2
    {
        get => y2;
        set
        {
            y2 = value;
            _lineSymbolModel.Y2 = y2;

            OnPropertyChanged();
        }
    }

    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            OnPropertyChanged();
        }
    }

    private readonly LineSymbolModel _lineSymbolModel;
    public LineSymbolVM(LineSymbolModel lineSymbolModel)
    {
        _lineSymbolModel = lineSymbolModel;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}