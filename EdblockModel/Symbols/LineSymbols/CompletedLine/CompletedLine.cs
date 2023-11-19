using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols.CompletedLine;

public class CompletedLine
{
    private readonly List<LineSymbolModel> linesSymbolModel;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly (int x, int y) _finalCoordinate;
    private readonly CompletedEvenLine completedEvenLine;

    public CompletedLine(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        completedEvenLine = new(drawnLineSymbolModel, finalCoordinate);
        linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;
        _finalCoordinate = finalCoordinate;
    }

    private void FinishDrawingHorizontalToHorizontalLines((int x, int y) cordinatePenultimateLine, (int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        if (lastLine.X2 == finalCoordinate.x || lastLine.Y2 == finalCoordinate.y)
        {
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var firstLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = lastLine.Y2,
                X2 = cordinatePenultimateLine.x,
                Y2 = cordinatePenultimateLine.y
            };
            linesSymbolModel.Add(firstLine);

            var coordinatelineSymbolModel = (firstLine.X2, firstLine.Y2);
            FinishDrawLastLine(coordinatelineSymbolModel);
        }
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


    public List<LineSymbolModel> GetCompleteLines()
    {
        var lastLine = linesSymbolModel[^1];

        if (linesSymbolModel.Count % 2 == 1)
        {
            if (outgoingPosition == PositionConnectionPoint.Bottom ||
                outgoingPosition == PositionConnectionPoint.Top)
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    var coordinatePenultimateLine = (_finalCoordinate.x, lastLine.Y2);
                    FinishDrawingHorizontalToHorizontalLines(coordinatePenultimateLine, _finalCoordinate);
                }
                else
                {
                    lastLine.Y2 = _finalCoordinate.y;
                    var coordinateLineSymbolModel = (lastLine.X2, lastLine.Y2);
                    FinishDrawLastLine(coordinateLineSymbolModel);
                }
            }
            else
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    lastLine.X2 = _finalCoordinate.x;
                    var coordinateLineSymbolModel = (lastLine.X2, lastLine.Y2);
                    FinishDrawLastLine(coordinateLineSymbolModel);
                }
                else
                {
                    var coordinatePenultimateLine = (lastLine.X2, _finalCoordinate.y);
                    FinishDrawingHorizontalToHorizontalLines(coordinatePenultimateLine, _finalCoordinate);
                }
            }
        }
        else
        {
            return completedEvenLine.Completed();
        }


        return linesSymbolModel;
    }
}