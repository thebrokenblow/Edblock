using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols.CompletedLine;

public class CompletedLine
{
    private readonly List<LineSymbolModel> linesSymbolModel;
    private readonly PositionConnectionPoint outgoingPosition;
    private readonly PositionConnectionPoint incomingPosition;
    private readonly (int x, int y) _finalCoordinate;

    public CompletedLine(DrawnLineSymbolModel drawnLineSymbolModel, (int x, int y) finalCoordinate)
    {
        linesSymbolModel = drawnLineSymbolModel.LinesSymbolModel;
        outgoingPosition = drawnLineSymbolModel.OutgoingPosition;
        incomingPosition = drawnLineSymbolModel.IncomingPosition;
        _finalCoordinate = finalCoordinate;
    }

    public List<LineSymbolModel> GetCompleteLines()
    {
        if (linesSymbolModel.Count % 2 == 1)
        {
            if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    var lastLine = linesSymbolModel[^1];
                    ILineCoordinateDecorator coordinateLineDecorator = new LineCoordinateDecorator((lastLine.X2, lastLine.Y2), _finalCoordinate);
                    FinishLinesParallelSides(_finalCoordinate, coordinateLineDecorator);
                }
                else
                {
                    var lastLine = linesSymbolModel[^1];
                    ILineCoordinateDecorator coordinateLineDecorator = new LineCoordinateDecorator((lastLine.X2, lastLine.Y2), _finalCoordinate);
                    FinishLinesNotParallelSides(coordinateLineDecorator, _finalCoordinate);
                }
            }
            else
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    var lastLine = linesSymbolModel[^1];
                    var build = new BuildLineCoordinateDecorato().SetSwapFinalCoordinate();
                    ILineCoordinateDecorator coordinateLineDecorator = new LineCoordinateDecorator((lastLine.X2, lastLine.Y2), _finalCoordinate);
                    coordinateLineDecorator = build.Build(coordinateLineDecorator);
                    FinishLinesNotParallelSides(coordinateLineDecorator, _finalCoordinate);
                }
                else
                {
                    var lastLine = linesSymbolModel[^1];
                    var build = new BuildLineCoordinateDecorato().SetSwapFinalCoordinate();
                    ILineCoordinateDecorator coordinateLineDecorator = new LineCoordinateDecorator((lastLine.X2, lastLine.Y2), _finalCoordinate);
                    coordinateLineDecorator = build.Build(coordinateLineDecorator);
                    FinishLinesParallelSides(_finalCoordinate, coordinateLineDecorator);
                }
            }
        }
        else
        {
            if (outgoingPosition == PositionConnectionPoint.Bottom || outgoingPosition == PositionConnectionPoint.Top)
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    FinishDrawingHorizontalToHorizontalLines2(lastLine, finalCoordinate);
                }
                else
                {
                    FinishDrawingHorizontalToVerticalLines2(lastLine, finalCoordinate);
                }
            }
            else
            {
                if (incomingPosition == PositionConnectionPoint.Top || incomingPosition == PositionConnectionPoint.Bottom)
                {
                    FinishDrawingVerticalToHorizontalLines2(lastLine, finalCoordinate);
                }
                else
                {
                    FinishDrawingVerticalToVerticalLines2(lastLine, finalCoordinate);
                }
            }
        }


        return linesSymbolModel;
    }

    private void FinishLinesNotParallelSides(ILineCoordinateDecorator lineCoordinateDecorator, (int x, int y) finalCoordinate)
    {
        var lastLine = linesSymbolModel[^1];

        if (lastLine.Y2 == finalCoordinate.y)
        {
            lastLine.Y2 = finalCoordinate.y;
        }
        else
        {
            lastLine.X2 = finalCoordinate.x;
        }

        var firstLine = new LineSymbolModel
        {
            X1 = lineCoordinateDecorator.LineCoordinateX,
            Y1 = lineCoordinateDecorator.FinalCoordinateY,
            X2 = finalCoordinate.x,
            Y2 = finalCoordinate.y
        };

        linesSymbolModel.Add(firstLine);
    }

    private List<LineSymbolModel> FinishLinesParallelSides((int x, int y) finalCoordinate, ILineCoordinateDecorator lineCoordinateDecorator)
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
                X2 = lineCoordinateDecorator.FinalCoordinateX,
                Y2 = lineCoordinateDecorator.LineCoordinateY,
            };

            var secondLine = new LineSymbolModel
            {
                X1 = lineCoordinateDecorator.FinalCoordinateX,
                Y1 = lineCoordinateDecorator.LineCoordinateY,
                X2 = finalCoordinate.x,
                Y2 = finalCoordinate.y,
            };

            linesSymbolModel.Add(firstLine);
            linesSymbolModel.Add(secondLine);
        }

        return linesSymbolModel;
    }
}