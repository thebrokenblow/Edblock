using System.Windows.Input;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class ScalePartBlockSymbol(
    BlockSymbolVM scalingBlockSymbol,
    Cursor cursorWhenScaling,
    IScaleAllSymbolComponentVM scaleAllSymbolVM,
    PositionScaleRectangle positionScaleRectangle)
{
    public double InitialWidthBlockSymbol { get; } = scalingBlockSymbol.Width;
    public double InitialHeigthBlockSymbol { get; } = scalingBlockSymbol.Height;
    public double InitialXCoordinateBlockSymbol { get; } = scalingBlockSymbol.XCoordinate;
    public double InitialYCoordinateBlockSymbol { get; } = scalingBlockSymbol.YCoordinate;
    public PositionScaleRectangle PositionScaleRectangle { get; } = positionScaleRectangle;

    public void SetSize(ICanvasSymbolsComponentVM canvasSymbolsComponentVM)
    {
        if (scaleAllSymbolVM.IsScaleAllSymbol)
        {
            foreach (var blockSymbolVM in canvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.BlockSymbolsVM)
            {
                blockSymbolVM.SetSize(this);
            }
        }
        else
        {
            scalingBlockSymbol.SetSize(this);
        }

        if (scalingBlockSymbol is IHasTextFieldVM blockSymbolHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Cursor = cursorWhenScaling;
        }

        canvasSymbolsComponentVM.Cursor = cursorWhenScaling;
    }
}