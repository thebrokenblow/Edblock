using System;
using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.Symbols.ScaleRectangles;

public class ScaleRectangle : INotifyPropertyChanged
{
    public int Width { get; } = 4;
    public int Height { get; } = 4;

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

    private string? fill = ScaleRectangleModel.HexNotFocusFill;
    public string? Fill
    {
        get => fill;
        set
        {
            fill = value;
            OnPropertyChanged();
        }
    }

    private string? borderBrush = ScaleRectangleModel.HexNotFocusBorderBrush;
    public string? BorderBrush
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
    public event PropertyChangedEventHandler? PropertyChanged;
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
        Coordinate = getCoordinateScaleRectangle.Invoke(Width, Height);

        EnterCursor = new(ShowAuxiliaryElements);
        LeaveCursor = new(HideAuxiliaryElements);
        ClickScaleRectangle = new(SaveScaleRectangle);
    }

    public void ChangeCoordination()
    {
        Coordinate = _getCoordinateScaleRectangle.Invoke(Width, Height);
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public static void SetColor(string? hexFill, string hexBorderBrush, List<ScaleRectangle> scaleRectangles)
    {
        foreach (var scaleRectangle in scaleRectangles)
        {
            scaleRectangle.Fill = hexFill;
            scaleRectangle.BorderBrush = hexBorderBrush;
        }
    }

    private void ShowAuxiliaryElements()
    {
        if (_canvasSymbolsVM.ScaleData == null)
        {
            _canvasSymbolsVM.Cursor = _cursorHover;
            SetColor(ScaleRectangleModel.HexFocusFill, ScaleRectangleModel.HexFocusBorderBrush, _blockSymbol.ScaleRectangles);
        }
    }

    private void HideAuxiliaryElements()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;
        SetColor(ScaleRectangleModel.HexNotFocusFill, ScaleRectangleModel.HexNotFocusBorderBrush, _blockSymbol.ScaleRectangles);
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
}