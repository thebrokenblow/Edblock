using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class CompletedLineModel
{
    private readonly List<LineSymbolModel> linesSymbolModel;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedLineModel(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;
        _finalCoordinate = finalCoordinate;
    }

    public List<LineSymbolModel> GetCompleteLines()
    {
        if (outgoingPosition == PositionConnectionPoint.Bottom || incomingPosition == PositionConnectionPoint.Top)
        {
            if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
            {
                CompleteParallel(linesSymbolModel, _finalCoordinate);
            }
        }

        return linesSymbolModel;
    }

    private List<LineSymbolModel> CompleteParallel(List<LineSymbolModel> linesSymbolModel, (int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        if (lastLine.X2 == finalCoordinate.x)
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
                X2 = finalCoordinate.x,
                Y2 = lastLine.Y2
            };

            var secondLine = new LineSymbolModel
            {
                X1 = finalCoordinate.x,
                Y1 = lastLine.Y2,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            linesSymbolModel.Add(firstLine);
            linesSymbolModel.Add(secondLine);
        }

        return linesSymbolModel;
    }

    //private void FinishDrawingParallelBorders(LineSymbolModel lastLineSymbolModel, ICoordinateDecorator coordinateLine, ICoordinateDecorator coordinateBlockSymbol)
    //{
    //    if (coordinateLine.X == coordinateBlockSymbol.X)
    //    {
    //        lastLineSymbolModel.X2 = coordinateLine.X;
    //        lastLineSymbolModel.Y2 = coordinateLine.Y;
    //    }
    //    else
    //    {
    //        var firstLine = new LineSymbolModel
    //        {
    //            X1 = lastLineSymbolModel.X2,
    //            Y1 = lastLineSymbolModel.Y2,
    //            X2 = coordinateBlockSymbol.X,
    //            Y2 = coordinateBlockSymbol.Y
    //        };

    //        var secondLine = new LineSymbolModel
    //        {
    //            X1 = coordinateBlockSymbol.X,
    //            Y1 = lastLineSymbolModel.Y2,
    //            X2 = coordinateBlockSymbol.X,
    //            Y2 = coordinateBlockSymbol.Y,
    //        };

    //        _drawnLineSymbolModel.LinesSymbolModel.Add(firstLine);
    //        _drawnLineSymbolModel.LinesSymbolModel.Add(secondLine);
    //    }
    //}

    private void FinishDrawingVerticalToVerticalLines(LineSymbolModel lastLine, (int x, int y) finalCoordinate)
    {
        if (lastLine.Y2 == finalCoordinate.y)
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
                X2 = lastLine.X2,
                Y2 = finalCoordinate.y
            };

            var secondLine = new LineSymbolModel
            {
                X1 = lastLine.X2,
                Y1 = finalCoordinate.y,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };
        }
    }
}