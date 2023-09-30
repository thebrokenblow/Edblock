using System;
using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols;
using EdblockModel.Symbols.LineSymbols;

namespace EdblockViewModel.Symbols.ConnectionPoints;

public class ConnectionPoint : INotifyPropertyChanged
{
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

    public double Diameter { get; init; } = ConnectionPointModel.DIAMETR;

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

    public Orientation Orientation { get; init; }
    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public BlockSymbol BlockSymbol { get; init; }
    public DelegateCommand<ConnectionPoint> ClickConnectionPoint { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly Func<double, int, Point> _getCoordinate;
    public ConnectionPoint(CanvasSymbolsVM canvasSymbolsVM, 
                           BlockSymbol symbol, 
                           Func<double, int, Point> getCoordinate, 
                           Orientation orientation)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        BlockSymbol = symbol;
        _getCoordinate = getCoordinate; 
        Coordinate = getCoordinate.Invoke(ConnectionPointModel.DIAMETR, ConnectionPointModel.OFFSET);
        Orientation = orientation;
        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
        ClickConnectionPoint = new(CreateLine);
    }

    public void ChangeCoordination()
    {
        Coordinate = _getCoordinate.Invoke(ConnectionPointModel.DIAMETR, ConnectionPointModel.OFFSET);
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

    public void CreateLine(ConnectionPoint connectionPoint)
    {
        var coordinate = connectionPoint.Coordinate;
        var orientation = connectionPoint.Orientation;
        var blockSymbolModel = connectionPoint.BlockSymbol.BlockSymbolModel;

        var lineSymbolModel = new LineSymbolModel(orientation);
        lineSymbolModel.SetStarCoordinate((int)coordinate.X, (int)coordinate.Y, blockSymbolModel);
        _canvasSymbolsVM.DrawLine(lineSymbolModel);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}