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
        if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                var lastLine = linesSymbolModel[^1];
                lastLine.X2 = _finalCoordinate.x;
                FinishLinesParallelSides(_finalCoordinate);
            }
            else
            {
                var penultimate = linesSymbolModel[^2];
                penultimate.Y2 = _finalCoordinate.y;
                FinishLinesNotParallelSides(_finalCoordinate);
            }
        }
        else
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                var penultimate = linesSymbolModel[^2];
                penultimate.X2 = _finalCoordinate.x;
                FinishLinesNotParallelSides(_finalCoordinate);
            }
            else
            {
                var lastLine = linesSymbolModel[^1];
                lastLine.Y2 = _finalCoordinate.y;
                FinishLinesParallelSides(_finalCoordinate);
            }
        }

        return linesSymbolModel;
    }

    private void FinishLinesParallelSides((int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        var firstLine = new LineSymbolModel
        {
            X1 = lastLine.X2,
            Y1 = lastLine.Y2,
            X2 = finalCoordinate.x,
            Y2 = finalCoordinate.y
        };

        linesSymbolModel.Add(firstLine);
    }

    private void FinishLinesNotParallelSides((int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        if (finalCoordinate.x == lastLine.X2 || finalCoordinate.y == lastLine.Y2)
        {
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var secondLine = linesSymbolModel[^2];

            lastLine.X1 = secondLine.X2;
            lastLine.Y1 = secondLine.Y2;

            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
    }
}