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

    public DelegateCommand ButtonDown { get; init; }
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

        ButtonDown = new(SetMovableRectangleLine);
        MouseEnter = new(SetDirectionMovementCursor);
        MouseLeave = new(SetBaseCursor);

        SetCoordinate();
    }

    internal void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    internal void SetCoordinate()
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

    internal void ChangeCoordinateLine((int x, int y) currentCoordinate)
    {
        var linesSymbolVM = _drawnLineSymbolVM.LinesSymbolVM;

        int indexCurrentLine = linesSymbolVM.IndexOf(_lineSymbolVM);

        var previousLineVM = linesSymbolVM[indexCurrentLine - 1];
        var nextLineVM = linesSymbolVM[indexCurrentLine + 1];

        if (_lineSymbolVM.X1 == _lineSymbolVM.X2)
        {
            previousLineVM.X2 = currentCoordinate.x;

            _lineSymbolVM.X1 = currentCoordinate.x;
            _lineSymbolVM.X2 = currentCoordinate.x;

            nextLineVM.X1 = currentCoordinate.x;
        }
        else
        {
            previousLineVM.Y2 = currentCoordinate.y;

            _lineSymbolVM.Y1 = currentCoordinate.y;
            _lineSymbolVM.Y2 = currentCoordinate.y;

            nextLineVM.Y1 = currentCoordinate.y;
        }

        _drawnLineSymbolVM.RedrawMovableRectanglesLine();

        SetCoordinate();
    }

    private void SetMovableRectangleLine()
    {
        _canvasSymbolsVM.MovableRectangleLine = this;
    }

    private void SetDirectionMovementCursor()
    {
        var currentCursor = _canvasSymbolsVM.Cursor;

        if (_lineSymbolVM.X1 == _lineSymbolVM.X2 && currentCursor == Cursors.Arrow)
        {
            _canvasSymbolsVM.Cursor = Cursors.SizeWE;
        }

        if (_lineSymbolVM.Y1 == _lineSymbolVM.Y2 && currentCursor == Cursors.Arrow)
        {
            _canvasSymbolsVM.Cursor = Cursors.SizeNS;
        }
    }

    private void SetBaseCursor()
    {
        var currentMovableRectangleLine = _canvasSymbolsVM.MovableRectangleLine;

        if (currentMovableRectangleLine == null)
        {
            _canvasSymbolsVM.Cursor = Cursors.Arrow;
        }
    }

    private static double GetMiddleCoordinateLine(double firstCoordinate, double secondCoordinate)
    {
        double middleCoordinate = firstCoordinate + (secondCoordinate - firstCoordinate) / 2;

        return middleCoordinate;
    }

    private static double GetCoordinateMovableRectangle(double coordinateLine, double sizeMovableRectangle)
    {
        double coordinate = coordinateLine - sizeMovableRectangle / 2 - BorderThickness;

        return coordinate;
    }
}