using System;
using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using EdblockViewModel.Symbols;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.ConnectionPointViewModel;

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

    public double Diameter { get; init; }

    private Brush? fill;
    public Brush? Fill
    {
        get => fill;
        set
        {
            fill = value;
            OnPropertyChanged();
        }
    }

    private Brush? stroke;
    public Brush? Stroke
    {
        get => stroke;
        set
        {
            stroke = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand EnterCursor { get; set; }
    public DelegateCommand LeaveCursor { get; set; }
    private readonly Symbol _symbol;
    private const double diameter = 8.5;
    private const int offset = 10;
    public ConnectionPoint(Symbol symbol, Func<double, int, Point> getCoordinate)
    {
        _symbol = symbol;
        Diameter = diameter;
        Coordinate = getCoordinate.Invoke(diameter, offset);
        Fill = Brushes.Transparent;
        Stroke = Brushes.Transparent;
        EnterCursor = new(ShowStroke);
        LeaveCursor = new(HideStroke);
    }

    public void ShowStroke()
    {
        ColorConnectionPoint.Show(_symbol.ConnectionPoints);
        ColorConnectionPoint.ShowStroke(this);
    }

    public void HideStroke()
    {
        ColorConnectionPoint.Hide(_symbol.ConnectionPoints);
        ColorConnectionPoint.HideStroke(this);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}