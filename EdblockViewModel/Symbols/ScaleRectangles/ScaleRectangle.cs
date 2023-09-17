using System;
using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ScaleRectangles;

public class ScaleRectangle : INotifyPropertyChanged
{
    private const int width = 4;
    public int Width 
    { 
        get => width;
    }

    private const int height = 4;
    public int Height 
    {
        get => height;
    }

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

    private Brush? borderBrush = Brushes.Transparent;
    public Brush? BorderBrush
    {
        get => borderBrush;
        set
        {
            borderBrush = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbol _symbol;
    private readonly Cursor _cursorHover;
    public ScaleRectangle(CanvasSymbolsVM canvasSymbolsVM, BlockSymbol symbol, Func<int, int, Point> getCoordinateScaleRectangle, Cursor cursorHover)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _symbol = symbol;
        _cursorHover = cursorHover;
        Coordinate = getCoordinateScaleRectangle.Invoke(width, height);
        EnterCursor = new(ShowAuxiliaryElements);
        LeaveCursor = new(HideAuxiliaryElements);
    }

    private void ShowAuxiliaryElements()
    {
        _canvasSymbolsVM.Cursor = _cursorHover;
        ColorScaleRectangle.Show(_symbol.ScaleRectangles);
    }

    private void HideAuxiliaryElements()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;
        ColorScaleRectangle.Hide(_symbol.ScaleRectangles);

    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}