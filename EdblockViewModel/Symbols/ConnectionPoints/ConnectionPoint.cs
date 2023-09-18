using System;
using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel;

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

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbol _symbol;
    private readonly Func<double, int, Point> _getCoordinate;
    private const int offset = 10;
    public ConnectionPoint(CanvasSymbolsVM canvasSymbolsVM, BlockSymbol symbol, Func<double, int, Point> getCoordinate)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _symbol = symbol;
        _getCoordinate = getCoordinate; 
        Coordinate = getCoordinate.Invoke(diameter, offset);
        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
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
            ColorConnectionPoint.Show(_symbol.ConnectionPoints);
            ColorConnectionPoint.ShowStroke(this);
        }
    }

    public void HideStroke()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;
        ColorConnectionPoint.Hide(_symbol.ConnectionPoints);
        ColorConnectionPoint.HideStroke(this);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}