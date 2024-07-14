using System.Windows.Input;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

public class ScalePartBlockSymbol(IScaleAllSymbolComponentVM scaleAllSymbolVM)
{
    public required double InitialWidthBlockSymbol { get; init; }
    public required double InitialHeigthBlockSymbol { get; init; }
    public required double InitialXCoordinateBlockSymbol { get; init; }
    public required double InitialYCoordinateBlockSymbol { get; init; }
    public required Cursor CursorWhenScaling { get; init; }
    public required ScalableBlockSymbolVM ScalingBlockSymbol { get; init; }
    public required PositionScaleRectangle PositionScaleRectangle { get; init; }

    public void SetSize(ICanvasSymbolsComponentVM canvasSymbolsComponentVM)
    {
        if (scaleAllSymbolVM.IsScaleAllSymbol)
        {
            foreach (var scalableBlockSymbols in canvasSymbolsComponentVM.ListCanvasSymbolsComponentVM.ScalableBlockSymbols)
            {
                scalableBlockSymbols.SetSize(this);
            }
        }
        else
        {
            ScalingBlockSymbol.SetSize(this);
        }

        if (ScalingBlockSymbol is IHasTextFieldVM blockSymbolHasTextField)
        {
            blockSymbolHasTextField.TextFieldSymbolVM.Cursor = CursorWhenScaling;
        }

        canvasSymbolsComponentVM.Cursor = CursorWhenScaling;
    }
}