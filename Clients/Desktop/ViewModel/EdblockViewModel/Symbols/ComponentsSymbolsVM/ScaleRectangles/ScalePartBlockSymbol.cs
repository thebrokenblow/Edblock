using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class ScalePartBlockSymbol(
    BlockSymbolVM scalingBlockSymbol,
    Cursor cursorWhenScaling,
    Func<ScalePartBlockSymbol, ICanvasSymbolsComponentVM, double>? getWidthBlockSymbol,
    Func<ScalePartBlockSymbol, ICanvasSymbolsComponentVM, double>? getHeigthBlockSymbol,
    IScaleAllSymbolComponentVM scaleAllSymbolVM,
    ObservableCollection<BlockSymbolVM> blockSymbolsVM)
{
    public BlockSymbolVM ScalingBlockSymbol { get; } = scalingBlockSymbol;
    public double InitialWidthBlockSymbol { get; } = scalingBlockSymbol.Width;
    public double InitialHeigthBlockSymbol { get; } = scalingBlockSymbol.Height;
    public double InitialXCoordinateBlockSymbol { get; } = scalingBlockSymbol.XCoordinate;
    public double InitialYCoordinateBlockSymbol { get; } = scalingBlockSymbol.YCoordinate;


    public void SetSize(ICanvasSymbolsComponentVM canvasSymbolsComponentVM)
    {
        if (getWidthBlockSymbol != null)
        {
            double width = getWidthBlockSymbol.Invoke(this, canvasSymbolsComponentVM);

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
        }

        if (getHeigthBlockSymbol != null)
        {
            double height = getHeigthBlockSymbol.Invoke(this, canvasSymbolsComponentVM);

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
        }

        if (ScalingBlockSymbol is IHasTextFieldVM blockSymbolHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Cursor = cursorWhenScaling;
        }

        canvasSymbolsComponentVM.Cursor = cursorWhenScaling;
    }
}