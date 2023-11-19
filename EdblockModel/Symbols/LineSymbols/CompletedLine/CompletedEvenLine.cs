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
        var lastLine = linesSymbolModel[^1];

        if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                lastLine.X2 = _finalCoordinate.x;
                var coordinateLastLine = (lastLine.X2, lastLine.Y2);
                FinishLastLine(coordinateLastLine);
            }
            else
            {
                var penultimateLine = linesSymbolModel[^2];
                penultimateLine.Y2 = _finalCoordinate.y;

                var coordinatePenultimateLine = (penultimateLine.X2, penultimateLine.Y2);
                FinishLastLine(coordinatePenultimateLine, lastLine);
            }
        }
        else
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                var penultimateLine = linesSymbolModel[^2];
                penultimateLine.X2 = _finalCoordinate.x;

                var coordinatePenultimateLine = (penultimateLine.X2, penultimateLine.Y2);
                FinishLastLine(coordinatePenultimateLine, lastLine);
            }
            else
            {
                lastLine.Y2 = _finalCoordinate.y;
                var coordinateLastLine = (lastLine.X2, lastLine.Y2);
                FinishLastLine(coordinateLastLine);
            }
        }

        return linesSymbolModel;
    }

    private void FinishLastLine((int x, int y) penultimateLineCoordinate, LineSymbolModel? lineSymbolModel = null)
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