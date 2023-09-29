using System;
using EdblockModel;
using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ConnectionPoints;

public enum OrientationConnectionPoint
{
    Vertical,
    Horizontal
}

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

    private const double diameter = 8.5;
    public double Diameter { get; init; } = diameter;

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

    public OrientationConnectionPoint OrientationConnectionPoint { get; init; }
    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public BlockSymbol BlockSymbol { get; init; }
    public DelegateCommand<ConnectionPoint> ClickConnectionPoint { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly Func<double, int, Point> _getCoordinate; 
    private const int offset = 10;
    public ConnectionPoint(CanvasSymbolsVM canvasSymbolsVM, 
                           BlockSymbol symbol, 
                           Func<double, int, Point> getCoordinate, 
                           OrientationConnectionPoint orientationConnectionPoint)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        BlockSymbol = symbol;
        _getCoordinate = getCoordinate; 
        Coordinate = getCoordinate.Invoke(diameter, offset);
        OrientationConnectionPoint = orientationConnectionPoint;
        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
        ClickConnectionPoint = new(CreateLine);
    }

    public void ChangeCoordination()
    {
        Coordinate = _getCoordinate.Invoke(diameter, offset);
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
        int x = (int)(connectionPoint.Coordinate.X + connectionPoint.BlockSymbol.XCoordinate + diameter / 2);
        x = CanvasSymbols.ChangeCoordinateSymbol(x);

        int y = (int)connectionPoint.Coordinate.Y + connectionPoint.BlockSymbol.YCoordinate - offset;
        y = CanvasSymbols.ChangeCoordinateSymbol(y);

        var coordinateConnectionPoint = new Point(x, y);
        _canvasSymbolsVM.DrawLine(coordinateConnectionPoint);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}