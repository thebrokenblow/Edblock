using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class ListLineSymbolModel
{
    public List<LineSymbolModel> LinesSymbols { get; set; } = new();
    private readonly PositionConnectionPoint _positionConnectionPoint;

    public ListLineSymbolModel(PositionConnectionPoint positionConnectionPoint)
    {
        _positionConnectionPoint = positionConnectionPoint;
    }

    public void ChangeCoordinateLine(int currentX, int currentY)
    {
        var lastLineSymbol = LinesSymbols[^1];

        if (_positionConnectionPoint == PositionConnectionPoint.Bottom || _positionConnectionPoint == PositionConnectionPoint.Top)
        {
            CoordinateLineModel.ChangeCoordinatesVerticalLines(LinesSymbols, lastLineSymbol, currentX, currentY);
        }
        else
        {
            CoordinateLineModel.ChangeCoordinatesHorizontalLines(LinesSymbols, lastLineSymbol, currentX, currentY);
        }
    }

    public LineSymbolModel GetNewLine()
    {
        var lastLineSymbol = LinesSymbols[^1];
        var newLineSymbolModel = FactoryLineSymbolModel.CreateNewLine(lastLineSymbol);

        LinesSymbols.Add(newLineSymbolModel);

        return newLineSymbolModel;
    }
}