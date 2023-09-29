using EdblockModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal class CoordinateBlockSymbol
{
    public static void SetXCoordinate(BlockSymbol? blockSymbol, int currentX, int previousX)
    {
        if (blockSymbol != null)
        {
            var currentXCoordinate = CanvasSymbols.GetCoordinateSymbol(blockSymbol.Coordinate.X, currentX, previousX, blockSymbol.Width);
            blockSymbol.Coordinate.X = currentXCoordinate;
            blockSymbol.BlockSymbolModel.X = currentXCoordinate;
        }
    }

    public static void SetYCoordinate(BlockSymbol? blockSymbol, int currentY, int previousY)
    {
        if (blockSymbol != null)
        {
            var currentYCoordinate = CanvasSymbols.GetCoordinateSymbol(blockSymbol.Coordinate.Y, currentY, previousY, blockSymbol.Height);
            blockSymbol.Coordinate = currentYCoordinate;
            blockSymbol.BlockSymbolModel.Y = currentYCoordinate;
        }
    }
}
