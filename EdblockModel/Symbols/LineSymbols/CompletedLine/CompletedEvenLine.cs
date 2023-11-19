using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols.CompletedLine;

internal class CompletedEvenLine
{
    private readonly List<LineSymbolModel> linesSymbolModel;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedEvenLine(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;
        _finalCoordinate = finalCoordinate;
    }

    public List<LineSymbolModel> Completed()
    {
        var lastLineModel = linesSymbolModel[^1];
        var penultimateLineModel = linesSymbolModel[^2];

        if (incomingPosition == PositionConnectionPoint.Bottom || incomingPosition == PositionConnectionPoint.Top)
        {
            penultimateLineModel.X2 = _finalCoordinate.x;
        }
        else
        {
            penultimateLineModel.Y2 = _finalCoordinate.y;
        }

        var coordinatelineSymbolModel = (penultimateLineModel.X2, penultimateLineModel.Y2);
        FinishDrawLastLine(coordinatelineSymbolModel, lastLineModel);

        return linesSymbolModel;
    }

    private void FinishDrawLastLine((int x, int y) penultimateLineCoordinate, LineSymbolModel? lineSymbolModel = null)
    {
        if (lineSymbolModel == null)
        {
            lineSymbolModel = new LineSymbolModel();
            linesSymbolModel.Add(lineSymbolModel);
        }

        lineSymbolModel.X1 = penultimateLineCoordinate.x;
        lineSymbolModel.Y1 = penultimateLineCoordinate.y;
        lineSymbolModel.X2 = _finalCoordinate.x;
        lineSymbolModel.Y2 = _finalCoordinate.y;
    }
}