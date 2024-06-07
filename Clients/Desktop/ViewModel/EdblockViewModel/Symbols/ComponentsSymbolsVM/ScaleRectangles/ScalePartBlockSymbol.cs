using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using EdblockViewModel.Components.CanvasSymbols;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class ScalePartBlockSymbol(
    BlockSymbolVM scalingBlockSymbol,
    Cursor cursorWhenScaling,
    Func<ScalePartBlockSymbol, CanvasSymbolsComponentVM, double>? getWidthBlockSymbol,
    Func<ScalePartBlockSymbol, CanvasSymbolsComponentVM, double>? getHeigthBlockSymbol,
    IScaleAllSymbolComponentVM scaleAllSymbolVM,
    ObservableCollection<BlockSymbolVM> blockSymbolsVM)
{
    public BlockSymbolVM ScalingBlockSymbol { get; } = scalingBlockSymbol;
    public double InitialWidthBlockSymbol { get; } = scalingBlockSymbol.Width;
    public double InitialHeigthBlockSymbol { get; } = scalingBlockSymbol.Height;
    public double InitialXCoordinateBlockSymbol { get; } = scalingBlockSymbol.XCoordinate;
    public double InitialYCoordinateBlockSymbol { get; } = scalingBlockSymbol.YCoordinate;

    public void SetWidthBlockSymbol(CanvasSymbolsComponentVM canvasSymbolsVM)
    {
        if (getWidthBlockSymbol == null)
        {
            return;
        }

        double width = getWidthBlockSymbol.Invoke(this, canvasSymbolsVM);

        if (scaleAllSymbolVM.IsScaleAllSymbol)
        {
            foreach (var blockSymbolVM in blockSymbolsVM)
            {
                blockSymbolVM.SetWidth(width);
            }
        }
        else
        {
            ScalingBlockSymbol.SetWidth(width);
        }

        if (ScalingBlockSymbol is IHasTextFieldVM blockSymbolHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Cursor = cursorWhenScaling;
        }

        canvasSymbolsVM.Cursor = cursorWhenScaling;
    }

    public void SetHeightBlockSymbol(CanvasSymbolsComponentVM canvasSymbolsVM)
    {
        if (getHeigthBlockSymbol == null)
        {
            return;
        }

        double height = getHeigthBlockSymbol.Invoke(this, canvasSymbolsVM);

        if (scaleAllSymbolVM.IsScaleAllSymbol)
        {
            foreach (var blockSymbolsVM in blockSymbolsVM)
            {
                blockSymbolsVM.SetHeight(height);
            }
        }
        else
        {
            ScalingBlockSymbol.SetHeight(height);
        }

        if (ScalingBlockSymbol is IHasTextFieldVM blockSymbolHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Cursor = cursorWhenScaling;
        }

        canvasSymbolsVM.Cursor = cursorWhenScaling;
    }
}