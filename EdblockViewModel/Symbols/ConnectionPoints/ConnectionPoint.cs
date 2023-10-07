using System;
using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.LineSymbols;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockViewModel.Symbols.ConnectionPoints;

public class ConnectionPoint : INotifyPropertyChanged
{
    //TODO: По хорошему заменить Point на int x, y;
    private Point coordinate;
    public Point Coordinate
    {
        get => coordinate;
        set
        {
            coordinate = value;
            OnPropertyChanged();
        }
    }

    public int Diameter { get; init; } = ConnectionPointModel.diametr;

    private Brush? fill = Brushes.Transparent;
    public Brush? Fill
    {
        get => fill;
        set
        {
            fill = value;
            OnPropertyChanged();
        }
    }

    private Brush? stroke = Brushes.Transparent;
    public Brush? Stroke
    {
        get => stroke;
        set
        {
            stroke = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public BlockSymbol BlockSymbol { get; init; }
    public DelegateCommand<ConnectionPoint> ClickConnectionPoint { get; init; }
    public Func<(int, int)> GetCoordinate { get; init; }
    public PositionConnectionPoint PositionConnectionPoint { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    public ConnectionPoint(CanvasSymbolsVM canvasSymbolsVM, 
                           BlockSymbol blockSymbol, 
                           Func<(int, int)> getCoordinate, 
                           PositionConnectionPoint positionConnectionPoint)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        GetCoordinate = getCoordinate;

        PositionConnectionPoint = positionConnectionPoint;
        BlockSymbol = blockSymbol;
        
        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
        ClickConnectionPoint = new(DrawLine);

        var coordinate = getCoordinate.Invoke();
        Coordinate = CoordinateSymbols.GetPointCoordinate(coordinate);
    }

    public void ChangeCoordination()
    {
        var coordinate = GetCoordinate.Invoke();
        Coordinate = CoordinateSymbols.GetPointCoordinate(coordinate);
    }

    public void ShowStroke()
    {
        if (_canvasSymbolsVM.ScaleData == null)
        {
            _canvasSymbolsVM.Cursor = Cursors.Hand;
            ColorConnectionPoint.Show(BlockSymbol.ConnectionPoints);
            ColorConnectionPoint.ShowStroke(this);
        }
    }

    public void HideStroke()
    {
        if (_canvasSymbolsVM.ScaleData == null)
        {
            _canvasSymbolsVM.Cursor = Cursors.Arrow;
            ColorConnectionPoint.Hide(BlockSymbol.ConnectionPoints);
            ColorConnectionPoint.HideStroke(this);
        }
    }

    public void DrawLine(ConnectionPoint connectionPoint)
    {
        if (_canvasSymbolsVM.CurrentLines == null)
        {
            var coordinate = connectionPoint.Coordinate;
            var positionConnectionPoint = connectionPoint.PositionConnectionPoint;
            var blockSymbolModel = connectionPoint.BlockSymbol.BlockSymbolModel;

            var lineSymbolModel = new LineSymbolModel(positionConnectionPoint);
            lineSymbolModel.SetStarCoordinate((int)coordinate.X, (int)coordinate.Y, blockSymbolModel);
            _canvasSymbolsVM.DrawLine(lineSymbolModel, BlockSymbol);
        }
        else
        {
            var coordinate = connectionPoint.Coordinate;
            (int currentX, int currentY) = LineSymbolModel.ChangeStartCoordinateLine((int)coordinate.X, (int)coordinate.Y, connectionPoint.PositionConnectionPoint);
            currentX += connectionPoint.BlockSymbol.XCoordinate;
            currentY += connectionPoint.BlockSymbol.YCoordinate;

            if (connectionPoint.PositionConnectionPoint == PositionConnectionPoint.Bottom)
            {
                currentY += Diameter;
            }
            else if (connectionPoint.PositionConnectionPoint == PositionConnectionPoint.Top)
            {
                currentY -= Diameter;
            }

            var arror = _canvasSymbolsVM.CurrentLines.ArrowSymbol;
            arror.ChangeOrientationArrow(currentX, currentY, connectionPoint.PositionConnectionPoint);

            _canvasSymbolsVM.CurrentLines = null;
        }
    }

    private static (int, int) ChangeEndCoordinateLine(PositionConnectionPoint positionConnectionPoint, int x, int y)
    {
        if (positionConnectionPoint == PositionConnectionPoint.Bottom)
        {
            y -= ConnectionPointModel.offsetPosition;
        }
        else if (positionConnectionPoint == PositionConnectionPoint.Top)
        {
            y += ConnectionPointModel.offsetPosition ;
        }
        else if (positionConnectionPoint == PositionConnectionPoint.Right)
        {
            x -= ConnectionPointModel.offsetPosition;
        }
        else
        {
            x += ConnectionPointModel.offsetPosition;
        }

        return (x, y);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}