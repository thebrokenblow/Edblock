using System;
using System.Windows.Input;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.Abstraction;
using System.Collections.ObjectModel;

namespace EdblockViewModel.Symbols.ScaleRectangles;

public class ScalePartBlockSymbol
{
    public BlockSymbolVM ScalingBlockSymbol { get; init; }
    public int InitialWidthBlockSymbol { get; init; }
    public int InitialHeigthBlockSymbol { get; init; }
    public int InitialXCoordinateBlockSymbol { get; init; }
    public int InitialYCoordinateBlockSymbol { get; init; }

    private readonly ScaleAllSymbolVM _scaleAllSymbolVM;
    private readonly Cursor _cursorWhenScaling;
    private readonly ObservableCollection<SymbolVM> _symbols;
    private readonly Func<ScalePartBlockSymbol, CanvasSymbolsVM, int>? _getWidthBlockSymbol;
    private readonly Func<ScalePartBlockSymbol, CanvasSymbolsVM, int>? _getHeigthBlockSymbol;

    public ScalePartBlockSymbol(
        BlockSymbolVM scalingBlockSymbol,
        Cursor cursorWhenScaling,
        Func<ScalePartBlockSymbol, CanvasSymbolsVM, int>? getWidthBlockSymbol,
        Func<ScalePartBlockSymbol, CanvasSymbolsVM, int>? getHeigthBlockSymbol,
        ScaleAllSymbolVM scaleAllSymbolVM,
        ObservableCollection<SymbolVM> symbolsVM)
    {
        ScalingBlockSymbol = scalingBlockSymbol;
        InitialWidthBlockSymbol = scalingBlockSymbol.Width;
        InitialHeigthBlockSymbol = scalingBlockSymbol.Height;
        InitialXCoordinateBlockSymbol = scalingBlockSymbol.XCoordinate;
        InitialYCoordinateBlockSymbol = scalingBlockSymbol.YCoordinate;
        _cursorWhenScaling = cursorWhenScaling;
        _getWidthBlockSymbol = getWidthBlockSymbol; 
        _getHeigthBlockSymbol = getHeigthBlockSymbol;
        _scaleAllSymbolVM = scaleAllSymbolVM;
        _symbols = symbolsVM;
    }

    public void SetWidthBlockSymbol(CanvasSymbolsVM canvasSymbolsVM)
    {
        canvasSymbolsVM.SetCurrentRedrawLines(ScalingBlockSymbol);

        if (_getWidthBlockSymbol == null)
        {
            return;
        }

        int width = _getWidthBlockSymbol.Invoke(this, canvasSymbolsVM);

        if (_scaleAllSymbolVM.IsScaleAllSymbolVM)
        {
            foreach (var symbol in _symbols)
            {
                if (symbol is BlockSymbolVM blockSymbolVM)
                {
                    blockSymbolVM.Width = width;
                }
            }
        }
        else
        {
            ScalingBlockSymbol.Width = width;
        }

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

        if (_scaleAllSymbolVM.IsScaleAllSymbolVM)
        {
            foreach (var symbol in _symbols)
            {
                if (symbol is BlockSymbolVM blockSymbolVM)
                {
                    blockSymbolVM.Height = height;
                }
            }
        }
        else
        {
            ScalingBlockSymbol.Height = height;
        }

        ScalingBlockSymbol.TextField.Cursor = _cursorWhenScaling;
        canvasSymbolsVM.Cursor = _cursorWhenScaling;
    }
}