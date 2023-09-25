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
    public DelegateCommand ClickScaleRectangle { get; init; }
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly BlockSymbol _blockSymbol;
    private readonly Cursor _cursorHover;
    private readonly Func<int, int, Point> _getCoordinateScaleRectangle;
    private readonly Func<ScaleData, CanvasSymbolsVM, int>? _getWidthSymbol;
    private readonly Func<ScaleData, CanvasSymbolsVM, int>? _getHeightSymbol;

    public ScaleRectangle(
        CanvasSymbolsVM canvasSymbolsVM, 
        BlockSymbol blockSymbol,
        Cursor cursorHover,
        Func<ScaleData, CanvasSymbolsVM, int>? getWidthSymbol,
        Func<ScaleData, CanvasSymbolsVM, int>? getHeightSymbol,
        Func<int, int, Point> getCoordinateScaleRectangle
        )
    {
        _blockSymbol = blockSymbol;
        _cursorHover = cursorHover;
        _canvasSymbolsVM = canvasSymbolsVM;
        _getWidthSymbol = getWidthSymbol;
        _getHeightSymbol = getHeightSymbol;

        _getCoordinateScaleRectangle = getCoordinateScaleRectangle;
        Coordinate = getCoordinateScaleRectangle.Invoke(width, height);

        EnterCursor = new(ShowAuxiliaryElements);
        LeaveCursor = new(HideAuxiliaryElements);
        ClickScaleRectangle = new(SaveScaleRectangle);
    }

    public void ChangeCoordination()
    {
        Coordinate = _getCoordinateScaleRectangle.Invoke(width, height);
    }

    private void ShowAuxiliaryElements()
    {
        if (_canvasSymbolsVM.ScaleData == null)
        {
            _canvasSymbolsVM.Cursor = _cursorHover;
            ColorScaleRectangle.Show(_blockSymbol.ScaleRectangles);
        }
    }

    private void HideAuxiliaryElements()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;
        ColorScaleRectangle.Hide(_blockSymbol.ScaleRectangles);
    }

    private void SaveScaleRectangle()
    {
        _canvasSymbolsVM.ScaleData = new(_cursorHover, 
                                         _blockSymbol, 
                                         _getWidthSymbol, 
                                         _getHeightSymbol, 
                                         _blockSymbol.Width, 
                                         _blockSymbol.Height,
                                         _blockSymbol.XCoordinate, 
                                         _blockSymbol.YCoordinate);
        _canvasSymbolsVM.Cursor = _cursorHover;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}