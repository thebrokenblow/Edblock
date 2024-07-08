using System.Windows.Input;
using EdblockViewModel.Core;

namespace EdblockViewModel.Symbols.LinesSymbolVM.Components;

public class MovableRectangleLineVM : ObservableObject
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

    private const double borderThickness = 1.25;
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

    private bool isShow;
    public bool IsShow
    {
        get => isShow;
        set
        {
            isShow = value;
            OnPropertyChanged();
        }
    }

    public LineSymbolVM LinesSymbolVM { get; }
    public MovableRectangleLineVM(LineSymbolVM linesSymbolVM)
    {
        LinesSymbolVM = linesSymbolVM;
        SetCoordinate();
    }

    public void SetCoordinate()
    {
        if (LinesSymbolVM.IsHorizontal())
        {
            XCoordinate = LinesSymbolVM.X1 + ((LinesSymbolVM.X2 - LinesSymbolVM.X1) / 2) - width / 2;
            YCoordinate = LinesSymbolVM.Y1 - ((height + borderThickness * 2) / 2);
        }
        else
        {
            XCoordinate = LinesSymbolVM.X1 - ((width + borderThickness * 2) / 2);
            YCoordinate = LinesSymbolVM.Y1 + ((LinesSymbolVM.Y2 - LinesSymbolVM.Y1) / 2) - height / 2;
        }
    }

    public void EnterCursor()
    {
        if (LinesSymbolVM.X1 == LinesSymbolVM.X2 && LinesSymbolVM.Y1 == LinesSymbolVM.Y2)
        {
            return;
        }

        if (LinesSymbolVM.IsHorizontal())
        {
            LinesSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.Cursor = Cursors.SizeNS;
        }
        else
        {
            LinesSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.Cursor = Cursors.SizeWE;
        }
    }

    public void LeaveCursor()
    {
        if (LinesSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.MovableRectangleLineVM is null)
        {
            LinesSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.Cursor = Cursors.Arrow;
        }
    }

    public void ChangeCoordinateLine(int xCoordinate, int yCoordinate)
    {
        var linesSymbolVM = LinesSymbolVM.DrawnLineSymbolVM.LinesSymbolVM;
        var currentIndexLineVM = linesSymbolVM.IndexOf(LinesSymbolVM);

        var previousLineVM = LinesSymbolVM.DrawnLineSymbolVM.LinesSymbolVM[currentIndexLineVM - 1];
        var nextLineVM = LinesSymbolVM.DrawnLineSymbolVM.LinesSymbolVM[currentIndexLineVM + 1];

        if (LinesSymbolVM.X1 == LinesSymbolVM.X2 && LinesSymbolVM.Y1 == LinesSymbolVM.Y2)
        {
            return;
        }

        if (LinesSymbolVM.IsHorizontal())
        {
            LinesSymbolVM.Y1 = yCoordinate;
            LinesSymbolVM.Y2 = yCoordinate;

            previousLineVM.Y2 = LinesSymbolVM.Y1;
            nextLineVM.Y1 = LinesSymbolVM.Y1;
        }
        else
        {
            LinesSymbolVM.X1 = xCoordinate;
            LinesSymbolVM.X2 = xCoordinate;

            previousLineVM.X2 = LinesSymbolVM.X1;
            nextLineVM.X1 = LinesSymbolVM.X1;
        }

        foreach (var movableRectangleLineVM in LinesSymbolVM.DrawnLineSymbolVM.MovableRectanglesLineVM)
        {
            movableRectangleLineVM.SetCoordinate();
        }
    }

    public void SubscribeToChangeCoordinates()
    {
        LinesSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.SelectedDrawnLinesVM.Add(LinesSymbolVM.DrawnLineSymbolVM);
        LinesSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.MovableRectangleLineVM = this;
    }

    public void UnsubscribeToChangeCoordinates()
    {
        LinesSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.MovableRectangleLineVM = null;
        LinesSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.ClearSelectedDrawnLinesVM();
    }
}