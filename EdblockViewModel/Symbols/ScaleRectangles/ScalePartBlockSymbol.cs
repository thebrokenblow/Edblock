using System;
using System.Windows.Input;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ScaleRectangles;

public class ScalePartBlockSymbol
{
    public BlockSymbolVM ScalingBlockSymbol { get; init; }
    public int InitialWidthBlockSymbol { get; init; }
    public int InitialHeigthBlockSymbol { get; init; }
    public int InitialXCoordinateBlockSymbol { get; init; }
    public int InitialYCoordinateBlockSymbol { get; init; }

    private readonly Cursor _cursorWhenScaling;
    private readonly Func<ScalePartBlockSymbol, CanvasSymbolsVM, int>? _getWidthBlockSymbol;
    private readonly Func<ScalePartBlockSymbol, CanvasSymbolsVM, int>? _getHeigthBlockSymbol;

    public ScalePartBlockSymbol(
        BlockSymbolVM scalingBlockSymbol,
        Cursor cursorWhenScaling,
        Func<ScalePartBlockSymbol, CanvasSymbolsVM, int>? getWidthBlockSymbol,
        Func<ScalePartBlockSymbol, CanvasSymbolsVM, int>? getHeigthBlockSymbol)
    {
        ScalingBlockSymbol = scalingBlockSymbol;

        InitialWidthBlockSymbol = scalingBlockSymbol.Width;
        InitialHeigthBlockSymbol = scalingBlockSymbol.Height;

        InitialXCoordinateBlockSymbol = scalingBlockSymbol.XCoordinate;
        InitialYCoordinateBlockSymbol = scalingBlockSymbol.YCoordinate;

        _cursorWhenScaling = cursorWhenScaling;

        _getWidthBlockSymbol = getWidthBlockSymbol; 
        _getHeigthBlockSymbol = getHeigthBlockSymbol;
    }

    public void SetWidthBlockSymbol(CanvasSymbolsVM canvasSymbolsVM)
    {
        canvasSymbolsVM.SetCurrentRedrawLines(ScalingBlockSymbol);

        if (_getWidthBlockSymbol == null)
        {
            return;
        }

        int width = _getWidthBlockSymbol.Invoke(this, canvasSymbolsVM);

        ScalingBlockSymbol.SetWidth(width);

        ScalingBlockSymbol.TextField.Cursor = _cursorWhenScaling;
        canvasSymbolsVM.Cursor = _cursorWhenScaling;
    }

    public void SetHeightBlockSymbol(CanvasSymbolsVM canvasSymbolsVM)
    {
        canvasSymbolsVM.SetCurrentRedrawLines(ScalingBlockSymbol);

        if (_getHeigthBlockSymbol == null)
        {
            return;
        }

        int height = _getHeigthBlockSymbol.Invoke(this, canvasSymbolsVM);

        ScalingBlockSymbol.SetHeight(height);

        ScalingBlockSymbol.TextField.Cursor = _cursorWhenScaling;
        canvasSymbolsVM.Cursor = _cursorWhenScaling;
    }
}