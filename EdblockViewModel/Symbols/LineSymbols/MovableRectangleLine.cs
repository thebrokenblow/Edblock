using Prism.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols.LineSymbols;

public class MovableRectangleLine : INotifyPropertyChanged
{
    private const double width = 4;
    public double Width
    {
        get => width;
    }

    private const double height = 4;
    public double Height 
    {
        get => height; 
    }

    private const double borderThickness = 1;
    public double BorderThickness 
    {
        get => borderThickness;
    }

    private double xCoordinate;
    public double XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
            OnPropertyChanged();
        }
    }

    private double yCoordinate;
    public double YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value;
            OnPropertyChanged();
        }
    }

    private bool isShow = true;
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

        Click = new(ChangeCoordinate);

        SetCoordinate();
    }

    public void ChangeCoordinate()
    {
        int index = _drawnLineSymbolVM.LinesSymbol.IndexOf(_lineSymbolVM);
        SetCoordinate();
        _drawnLineSymbolVM.CanvasSymbolsVM.Redraw(index);
    }

    private void SetCoordinate()
    {
        double yMiddleCoordinate = GetMiddleCoordinateLine(_lineSymbolVM.Y1, _lineSymbolVM.Y2);
        double yCoordinate = GetCoordinateMovableRectangle(yMiddleCoordinate, Height);
        double xCoordinate = GetCoordinateMovableRectangle(_lineSymbolVM.X2, Width);

        if (_lineSymbolVM.Y1 == _lineSymbolVM.Y2)
        {
            yCoordinate = GetCoordinateMovableRectangle(_lineSymbolVM.Y2, Height);
            double xMiddleCoordinate = GetMiddleCoordinateLine(_lineSymbolVM.X1, _lineSymbolVM.X2);
            xCoordinate = GetCoordinateMovableRectangle(xMiddleCoordinate, Width);
        }

        YCoordinate = yCoordinate;
        XCoordinate = xCoordinate;
    }

    private double GetMiddleCoordinateLine(double firstCoordinate, double secondCoordinate)
    {
        double middleCoordinate = firstCoordinate + (secondCoordinate - firstCoordinate) / 2;

        return middleCoordinate;
    }

    private double GetCoordinateMovableRectangle(double coordinateLine, double sizeMovableRectangle)
    {
        double coordinate = coordinateLine - sizeMovableRectangle / 2 - BorderThickness;

        return coordinate;
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}