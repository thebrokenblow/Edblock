using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class ListLineSymbolModel
{
    public List<LineSymbolModel> LinesSymbols { get; set; } = new();

    public void ChangeCoordinateLine(int currentX, int currentY)
    {
        var lastLineSymbol = LinesSymbols[^1];

        if (lastLineSymbol.PositionConnectionPoint == PositionConnectionPoint.Bottom ||
            lastLineSymbol.PositionConnectionPoint == PositionConnectionPoint.Top)
        {
            CoordinateLineModel.ChangeCoordinatesVerticalLines(LinesSymbols, lastLineSymbol, currentX, currentY);
        }
        else
        {
            CoordinateLineModel.ChangeCoordinatesHorizontalLines(LinesSymbols, lastLineSymbol, currentX, currentY);
        }
    }
}