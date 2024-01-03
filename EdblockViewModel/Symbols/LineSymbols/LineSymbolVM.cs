using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockModel.SymbolsModel.LineSymbolsModel;

namespace EdblockViewModel.Symbols.LineSymbols;

public class LineSymbolVM : INotifyPropertyChanged
{
    private double x1;
    public double X1
    {
        get => x1;
        set
        {
            x1 = value;
            _lineSymbolModel.X1 = x1;

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
            _lineSymbolModel.Y1 = y1;

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
            _lineSymbolModel.X2 = x2;

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