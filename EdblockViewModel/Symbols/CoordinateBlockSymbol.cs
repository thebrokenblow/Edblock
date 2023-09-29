using EdblockModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal class CoordinateBlockSymbol
{
    public static void SetXCoordinate(BlockSymbol? blockSymbol, int currentX, int previousX)
    {
        if (blockSymbol != null)
        {
            var currentXCoordinate = CanvasSymbols.GetCoordinateSymbol(blockSymbol.XCoordinate, currentX, previousX, blockSymbol.Width);
            blockSymbol.XCoordinate = currentXCoordinate;
            blockSymbol.BlockSymbolModel.X = currentXCoordinate;
        }
    }

    public static void SetYCoordinate(BlockSymbol? blockSymbol, int currentY, int previousY)
    {
        if (blockSymbol != null)
        {
            var currentYCoordinate = CanvasSymbols.GetCoordinateSymbol(blockSymbol.YCoordinate, currentY, previousY, blockSymbol.Height);
            blockSymbol.YCoordinate = currentYCoordinate;
            blockSymbol.BlockSymbolModel.Y = currentYCoordinate;
        }
    }
}
