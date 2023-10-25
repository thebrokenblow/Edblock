using System;
using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.Symbols.ScaleRectangles;
using EdblockModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ScaleRectangles;

public class ScaleRectangle : INotifyPropertyChanged
{
    public int Width { get; } = ScaleRectangleModel.Width;
    public int Height { get; } = ScaleRectangleModel.Height;

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
    private readonly BlockSymbol _blockSymbolModel;
    private readonly Cursor _cursorHover;
    private readonly Func<int, int, (int, int)> _getCoordinateScaleRectangle;
    private readonly Func<ScaleData, CanvasSymbolsVM, int>? _getWidthSymbol;
    private readonly Func<ScaleData, CanvasSymbolsVM, int>? _getHeightSymbol;

    public ScaleRectangle(
        CanvasSymbolsVM canvasSymbolsVM, 
        BlockSymbol blockSymbolModel,
        Cursor cursorHover,
        Func<ScaleData, CanvasSymbolsVM, int>? getWidthSymbol,
        Func<ScaleData, CanvasSymbolsVM, int>? getHeightSymbol,
        Func<int, int, (int, int)> getCoordinateScaleRectangle
        )
    {
        _blockSymbolModel = blockSymbolModel;
        _cursorHover = cursorHover;
        _canvasSymbolsVM = canvasSymbolsVM;
        _getWidthSymbol = getWidthSymbol;
        _getHeightSymbol = getHeightSymbol;

        _getCoordinateScaleRectangle = getCoordinateScaleRectangle;
        (XCoordinate, YCoordinate) = getCoordinateScaleRectangle.Invoke(Width, Height);

        EnterCursor = new(Show);
        LeaveCursor = new(Hide);
        ClickScaleRectangle = new(SaveScaleRectangle);
    }

    public void ChangeCoordination()
    {
        (XCoordinate, YCoordinate) = _getCoordinateScaleRectangle.Invoke(Width, Height);
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

    private void Show()
    {
        if (_canvasSymbolsVM.ScaleData == null)
        {
            _canvasSymbolsVM.Cursor = _cursorHover;
            SetColor(ScaleRectangleModel.HexFocusFill, ScaleRectangleModel.HexFocusBorderBrush, _blockSymbolModel.ScaleRectangles);
        }
    }

    private void Hide()
    {
        _canvasSymbolsVM.Cursor = Cursors.Arrow;
        SetColor(ScaleRectangleModel.HexNotFocusFill, ScaleRectangleModel.HexNotFocusBorderBrush, _blockSymbolModel.ScaleRectangles);
    }

    private void SaveScaleRectangle()
    {
        _canvasSymbolsVM.ScaleData = new(_cursorHover, 
                                         _blockSymbolModel, 
                                         _getWidthSymbol, 
                                         _getHeightSymbol, 
                                         _blockSymbolModel.Width, 
                                         _blockSymbolModel.Height,
                                         _blockSymbolModel.XCoordinate, 
                                         _blockSymbolModel.YCoordinate);
        _canvasSymbolsVM.Cursor = _cursorHover;
    }
}