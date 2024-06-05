using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using EdblockViewModel.Components.CanvasSymbols;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class ScalePartBlockSymbol
{
    public BlockSymbolVM ScalingBlockSymbol { get; init; }
    public double InitialWidthBlockSymbol { get; init; }
    public double InitialHeigthBlockSymbol { get; init; }
    public double InitialXCoordinateBlockSymbol { get; init; }
    public double InitialYCoordinateBlockSymbol { get; init; }

    private readonly IScaleAllSymbolComponentVM _scaleAllSymbolVM;
    private readonly Cursor _cursorWhenScaling;
    private readonly ObservableCollection<BlockSymbolVM> _symbols;
    private readonly Func<ScalePartBlockSymbol, CanvasSymbolsComponentVM, double>? _getWidthBlockSymbol;
    private readonly Func<ScalePartBlockSymbol, CanvasSymbolsComponentVM, double>? _getHeigthBlockSymbol;

    public ScalePartBlockSymbol(
        BlockSymbolVM scalingBlockSymbol,
        Cursor cursorWhenScaling,
        Func<ScalePartBlockSymbol, CanvasSymbolsComponentVM, double>? getWidthBlockSymbol,
        Func<ScalePartBlockSymbol, CanvasSymbolsComponentVM, double>? getHeigthBlockSymbol,
        IScaleAllSymbolComponentVM scaleAllSymbolVM,
        ObservableCollection<BlockSymbolVM> symbolsVM)
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

    public void SetWidthBlockSymbol(CanvasSymbolsComponentVM canvasSymbolsVM)
    {

        if (_getWidthBlockSymbol == null)
        {
            return;
        }

        double width = _getWidthBlockSymbol.Invoke(this, canvasSymbolsVM);

        if (_scaleAllSymbolVM.IsScaleAllSymbol)
        {
            foreach (var symbol in _symbols)
            {
                if (symbol is BlockSymbolVM blockSymbolVM)
                {
                    blockSymbolVM.SetWidth(width);
                }
            }

           
        }
        else
        {
            ScalingBlockSymbol.SetWidth(width);
        }

        if (ScalingBlockSymbol is IHasTextFieldVM blockSymbolHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Cursor = _cursorWhenScaling;
        }

        canvasSymbolsVM.Cursor = _cursorWhenScaling;
    }

    public void SetHeightBlockSymbol(CanvasSymbolsComponentVM canvasSymbolsVM)
    {
       

        if (_getHeigthBlockSymbol == null)
        {
            return;
        }

        double height = _getHeigthBlockSymbol.Invoke(this, canvasSymbolsVM);

        if (_scaleAllSymbolVM.IsScaleAllSymbol)
        {
            foreach (var symbol in _symbols)
            {
                if (symbol is BlockSymbolVM blockSymbolVM)
                {
                    blockSymbolVM.SetHeight(height);
                }
            }


        }
        else
        {
            ScalingBlockSymbol.SetHeight(height);
        }

        if (ScalingBlockSymbol is IHasTextFieldVM blockSymbolHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Cursor = _cursorWhenScaling;
        }

        canvasSymbolsVM.Cursor = _cursorWhenScaling;
    }
}