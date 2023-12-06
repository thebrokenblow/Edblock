using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.Symbols.LineSymbols;

public class MovableRectangleLine : INotifyPropertyChanged
{
    private const double width = 4;
    public static double Width
    {
        get => width;
    }

    private const double height = 4;
    public static double Height 
    {
        get => height; 
    }

    private const double borderThickness = 1;
    public static double BorderThickness 
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
    public DelegateCommand MouseEnter { get; init; }
    public DelegateCommand MouseLeave { get; init; }

    private readonly DrawnLineSymbolVM _drawnLineSymbolVM;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly LineSymbolVM _lineSymbolVM;
    public MovableRectangleLine(DrawnLineSymbolVM drawnLineSymbolVM, LineSymbolVM lineSymbolVM)
    {
        _drawnLineSymbolVM = drawnLineSymbolVM;
        _canvasSymbolsVM = drawnLineSymbolVM.CanvasSymbolsVM;

        _lineSymbolVM = lineSymbolVM;

        Click = new(ChangeCoordinate);
        MouseEnter = new(SetCursor);
        MouseLeave = new(SetBaseCursorCursor);

        SetCoordinate();
    }

    private void ChangeCoordinate()
    {
        int index = _drawnLineSymbolVM.LinesSymbol.IndexOf(_lineSymbolVM);
        SetCoordinate();
        _drawnLineSymbolVM.CanvasSymbolsVM.Redraw(index);
    }

    private void SetCursor()
    {
        _canvasSymbolsVM.Cursor = Cursors.SizeNS;

        if (_lineSymbolVM.X1 == _lineSymbolVM.X2)
        {
            _canvasSymbolsVM.Cursor = Cursors.SizeWE;
        }
    }

    private void SetBaseCursorCursor()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;
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