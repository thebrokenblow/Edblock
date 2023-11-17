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
                FinishDrawingHorizontalToHorizontalLines2(_finalCoordinate);
            }
            else
            {
                FinishLinesNotParallelSides2(_finalCoordinate);
            }
        }
        else
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                FinishLinesNotParallelSides1(_finalCoordinate);
            }
            else
            {
                FinishDrawingVerticalToVerticalLines2(_finalCoordinate);
            }
        }

        return linesSymbolModel;
    }
    private void FinishDrawingHorizontalToHorizontalLines2((int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        lastLine.X2 = finalCoordinate.x;

        var firstLine = new LineSymbolModel
        {
            X1 = finalCoordinate.x,
            Y1 = lastLine.Y2,
            Y2 = finalCoordinate.y,
            X2 = finalCoordinate.x
        };
    }

    private void FinishDrawingVerticalToVerticalLines2((int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];
        lastLine.Y2 = finalCoordinate.y;

        var firstLine = new LineSymbolModel
        {
            X1 = lastLine.X2,
            Y1 = lastLine.Y2,
            X2 = finalCoordinate.x,
            Y2 = lastLine.Y2
        };
    }   

    private void FinishLinesNotParallelSides1((int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        if (finalCoordinate.x == lastLine.X2)
        {
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            var secondLine = linesSymbolModel[^2];

            secondLine.X2 = finalCoordinate.x;

            lastLine.X1 = finalCoordinate.x;
            lastLine.X2 = finalCoordinate.x;
            lastLine.Y2 = finalCoordinate.y;
        }
    }

    private void FinishLinesNotParallelSides2((int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        if (finalCoordinate.y == lastLine.Y2)
        {
            lastLine.X2 = finalCoordinate.x;
        }
        else
        {
            var secondLine = linesSymbolModel[^2];

            secondLine.Y2 = finalCoordinate.y;

            lastLine.Y1 = finalCoordinate.y;
            lastLine.Y2 = finalCoordinate.y;
            lastLine.X2 = finalCoordinate.x;
        }
    }
}
