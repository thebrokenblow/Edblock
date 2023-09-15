using System;
using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using EdblockViewModel.Symbols;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.ConnectionPointVM;

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
    private readonly Symbol _symbol;
    private const int offset = 10;
    public ConnectionPoint(CanvasSymbolsVM canvasSymbolsVM, Symbol symbol, Func<double, int, Point> getCoordinate)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _symbol = symbol;
        Coordinate = getCoordinate.Invoke(diameter, offset);
        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
    }

    public void ShowStroke()
    {
        _canvasSymbolsVM.Cursor = Cursors.Hand;
        ColorConnectionPoint.Show(_symbol.ConnectionPoints);
        ColorConnectionPoint.ShowStroke(this);
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