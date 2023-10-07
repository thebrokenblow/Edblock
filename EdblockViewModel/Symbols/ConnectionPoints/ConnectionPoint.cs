using System;
using Prism.Commands;
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

        (XCoordinate, YCoordinate) = getCoordinate.Invoke();
    }

    public void ChangeCoordination()
    {
        (XCoordinate, YCoordinate) = GetCoordinate.Invoke();
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
            int xCoordinate = connectionPoint.XCoordinate;
            int yCoordinate = connectionPoint.YCoordinate;

            var positionConnectionPoint = connectionPoint.PositionConnectionPoint;
            var blockSymbolModel = connectionPoint.BlockSymbol.BlockSymbolModel;

            var lineSymbolModel = new LineSymbolModel(positionConnectionPoint);
            lineSymbolModel.SetStarCoordinate(xCoordinate, yCoordinate, blockSymbolModel);
            _canvasSymbolsVM.DrawLine(lineSymbolModel, BlockSymbol);
        }
        else
        {
            int xCoordinate = connectionPoint.XCoordinate;
            int yCoordinate = connectionPoint.YCoordinate;

            (xCoordinate, yCoordinate) = LineSymbolModel.ChangeStartCoordinateLine(xCoordinate, yCoordinate, connectionPoint.PositionConnectionPoint);
            xCoordinate += connectionPoint.BlockSymbol.XCoordinate;
            yCoordinate += connectionPoint.BlockSymbol.YCoordinate;

            if (connectionPoint.PositionConnectionPoint == PositionConnectionPoint.Bottom)
            {
                xCoordinate += Diameter;
            }
            else if (connectionPoint.PositionConnectionPoint == PositionConnectionPoint.Top)
            {
                yCoordinate -= Diameter;
            }

            var arror = _canvasSymbolsVM.CurrentLines.ArrowSymbol;
            arror.ChangeOrientationArrow(xCoordinate, yCoordinate, connectionPoint.PositionConnectionPoint);

            _canvasSymbolsVM.CurrentLines = null;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}